using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    [Serializable]
    public class ItemCustomClassInfo
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Columns { get; set; }
        public string TableName { get; set; }
        public string Filter { get; set; }
        public string OrderBy { get; set; }
        public bool IsStop { get; set; }
        public int Sort { get; set; }
    }
}
