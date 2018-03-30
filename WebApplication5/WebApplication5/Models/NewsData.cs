using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newsapi.Models
{
    public class NewsData
    {
        public string[] InstrumentIDs { get; set; }
        public string[] CompanyNames { get; set; }
        public string TimeStamp { get; set; }
        public string Headline { get; set; }
        public string NewsText { get; set; }
    }
}