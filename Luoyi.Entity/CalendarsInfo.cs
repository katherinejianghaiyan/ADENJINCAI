using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    [Serializable]
	public class CalendarsInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BUGUID { get; set; }
        public string SiteGUID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Working { get; set; }
    }
}
