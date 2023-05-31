﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consiola_consume_api_ocr
{
    public class Word
    { 
        public string boundingBox { get; set; }
        public string text { get; set; }
    }

    public class Line
    {
        public string boundingBox { get; set; }
        public Word[] words { get; set; }

    }

    public class Region { 
        public string boundingBox { get; set; }
        public Line[] lines { get; set; }
    }

    public class OCRresponse { 
        public string language { get; set; }
        public decimal textAngle { get; set; }
        public string orientation { get; set; }
        public string modelVersion { get; set; }
        public Region[] regions { get; set; }

    }
}
