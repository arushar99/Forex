using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forex.Models
{
    public class Currency
    { 
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public string Time { get; set; }
    }
}