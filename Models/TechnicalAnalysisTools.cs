using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forex.Models
{
    public class TechnicalAnalysisTools
    {
        public string Moving_AVG5 { get; set; }
        public string Moving_AVG10 { get; set; }
        public string Moving_AVG20 { get; set; }
        public string Moving_AVG50{ get; set; }
        public string Moving_AVG100{ get; set; }
        public string Moving_AVG200 { get; set; }
        public string Exp_Moving_AVG5 { get; set; }
        public string Exp_Moving_AVG10 { get; set; }
        public string Exp_Moving_AVG20 { get; set; }
        public string Exp_Moving_AVG50 { get; set; }
        public string Exp_Moving_AVG100 { get; set; }
        public string Exp_Moving_AVG200 { get; set; }

        //public string Moving_AVG5_status { get; set; }
        //public string Moving_AVG10_status { get; set; }
        //public string Moving_AVG20_status { get; set; }
        //public string Moving_AVG50_status { get; set; }
        //public string Moving_AVG100_status { get; set; }
        //public string Moving_AVG200_status { get; set; }
        //public string Exp_Moving_AVG5_status { get; set; }
        //public string Exp_Moving_AVG10_status { get; set; }
        //public string Exp_Moving_AVG20_status { get; set; }
        //public string Exp_Moving_AVG50_status { get; set; }
        //public string Exp_Moving_AVG100_status { get; set; }
        //public string Exp_Moving_AVG200_status { get; set; }
        
        public string Summary { get; set; }

    }
}