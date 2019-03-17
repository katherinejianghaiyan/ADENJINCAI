using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Luoyi.Entity
{
    public class SurveyDetails
    {
        public string headGuid { get; set; }
        public string siteGuid { get; set; }
        public string lineGuid { get; set; }
        public string itemName { get; set; }
        public string itemAnswer { get; set; }
        public string sort { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public string userName { get; set; }
        public string userDept { get; set; }
        public int allowNull { get; set; }
        public string itemStyle { get; set; }
        public string userComplaint { get; set; }
        public List<object> details { get; set; }
        public string action { get; set; }
    }
}