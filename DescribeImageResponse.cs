using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consiola_consume_api_ocr
{
    internal class DescribeImageResponse
    {
        public string requestId {get; set;}
        public DateTime modelVersion { get; set;}
        public Description description { get; set;}
    }

    public class Description {
        public string[] tags { get; set; }
        public Caption[] captions { get; set; }
    }

    public class Caption { 
        public string text { get; set; }
        public decimal confidence { get; set; }
    }


}
