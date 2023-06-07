using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace consiola_consume_api_ocr
{
    internal class Program
    {
        static string url = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/";
        static string key = "d1c3a31c3e5243e8af964c1c95a01cb4";
        static string par = "?language=es&detectOrientation=true&model-version=latest";

        static void Main(string[] args)
        {
            //var ocr = ConsumeOCR_Url("https://i.pinimg.com/originals/39/8a/66/398a663074313bb6429c0bd3c8c646aa.jpg");
            //var ocr = ConsumeOCR_archivlo_local("d:\\foto.jpg");

            //var descr = DescribeImage_Url("https://i.pinimg.com/originals/41/b7/54/41b75468c8d05512ce1f2254583a3d75.jpg");
            var descr = DescribeImage_archivo_local("d:\\paisaje.jpg");
            Console.WriteLine(descr.description.captions[0].text);
            Console.ReadKey();
        }

        private static DescribeImageResponse DescribeImage_archivo_local(string nombre_archivo)
        {
            par = "?maxCandidates=3&language=es&model-version=latest";
            var api = new WebClient();
            api.Headers.Add("Content-Type", "application/octect-stream");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);

            var bytes = api.UploadFile(url + "describe" + par, "POST", nombre_archivo);
            var json = Encoding.UTF8.GetString(bytes);
            var descr = Newtonsoft.Json.JsonConvert.DeserializeObject<DescribeImageResponse>(json);
            return descr;
        }

        private static DescribeImageResponse DescribeImage_Url(string url_foto)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject( new { 
                url = url_foto
            });
            par = "?maxCandidates=3&language=es&model-version=latest";

            var api = new WebClient();
            api.Headers.Add("Content-Type", "application/json");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);

            json = api.UploadString(url + "describe" + par, "POST", json);
            var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<DescribeImageResponse>(json);
            return resp;
        }

        private static OCRresponse ConsumeOCR_archivlo_local(string nombre_archivo)
        {
            var api = new WebClient();

            api.Headers.Add("Content-Type", "application/octet-stream");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);
            
            var bytes = api.UploadFile(url +"ocr" + par, "POST", nombre_archivo);
            var json = Encoding.UTF8.GetString(bytes);
            var ocr = Newtonsoft.Json.JsonConvert.DeserializeObject<OCRresponse>(json);
            return ocr;

        }

        private static OCRresponse ConsumeOCR_Url(string url_foto)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Url = url_foto
            }
            );

            var api = new WebClient();

            api.Headers.Add("Content-Type", "application/json");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);
            json = api.UploadString(url + "ocr" + par, "POST", json);

            var ocr = Newtonsoft.Json.JsonConvert.DeserializeObject<OCRresponse>(json);
            return ocr;
        }

        private static string GetOcrText(OCRresponse ocr)
        {
            var txt = "";
            foreach (var region in ocr.regions)
            {
                foreach (var line in region.lines)
                {
                    foreach (var word in line.words)
                    {
                        //byte[] encodedBytes = Encoding.UTF8.GetBytes(word.text);
                        //txt += Encoding.Unicode.GetString(encodedBytes) + " ";
                        txt += word.text + " ";
                    }
                    txt += " ";
                }
                txt += "\r\n";
            }
            return txt;
        }
    }
}
