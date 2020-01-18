using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forex.Models
{
    public class CurrenciesForList
    {
        public string Symbol { get; set; }
        public List<Currency> Currencies { get; set; }
        public string Period { get; set; }
        public TechnicalAnalysisTools MovingAverages {get;set;}
    }
}