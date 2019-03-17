using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    public class MealDateList
    {
        public string startDate { get; set; }       
        public string day { get; set; }
        public List<MealTypeList> MealType { get; set; }
        
    }
}
