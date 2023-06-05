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
        static string url = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/ocr";
        static string key = "d1c3a31c3e5243e8af964c1c95a01cb4";
        static string par = "?language=es&detectOrientation=true&model-version=latest";

        static void Main(string[] args)
        {
            //var ocr = ConsumeOCR_Url("https://i.pinimg.com/originals/39/8a/66/398a663074313bb6429c0bd3c8c646aa.jpg");
            var ocr = ConsumeOCR_archivlo_local("d:\\foto.jpg");
            Console.WriteLine(GetOcrText(ocr));
            Console.ReadKey();
        }

        private static OCRresponse ConsumeOCR_archivlo_local(string nombre_archivo)
        {
            var api = new WebClient();

            api.Headers.Add("Content-Type", "application/octet-stream");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);
            
            var bytes = api.UploadFile(url + par, "POST", nombre_archivo);
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
            json = api.UploadString(url + par, "POST", json);

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
