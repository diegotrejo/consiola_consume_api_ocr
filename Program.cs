using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace consiola_consume_api_ocr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = "https://eastus.api.cognitive.microsoft.com/vision/v3.2/ocr";
            var key = "d1c3a31c3e5243e8af964c1c95a01cb4";
            var par = "?language=es&detectOrientation=true&model-version=latest";

            var foto = "https://i.pinimg.com/originals/39/8a/66/398a663074313bb6429c0bd3c8c646aa.jpg";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject( new 
                { 
                    Url = foto
                }
            );
            
            var api = new WebClient();

            api.Headers.Add("Content-Type", "application/json");
            api.Headers.Add("Ocp-Apim-Subscription-Key", key);
            json =  api.UploadString(url + par, "POST", json);

            var ocr = Newtonsoft.Json.JsonConvert.DeserializeObject<OCRresponse>(json);

            Console.WriteLine(GetOcrText(ocr));
            Console.ReadKey();
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
                    txt += "\r\n";
                }
                txt += "\r\n";
            }
            return txt;
        }
    }
}
