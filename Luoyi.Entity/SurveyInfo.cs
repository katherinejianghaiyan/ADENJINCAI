using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Luoyi.Entity
{
    public class SurveyInfo
    {
        public string lineGuid { get; set; }
        public string siteGuid { get; set; }
        public string welcomeText { get; set; }
        public string surveyTitle { get; set; }
        public string itemName { get; set; }
        public string itemAnswer { get; set; }
        public List<SurveyDetails> surveyLines { get; set; }
        public int allowNull { get; set; }
        public string itemStyle { get; set; }
    }
}