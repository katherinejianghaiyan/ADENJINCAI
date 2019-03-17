using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    public class NutritionWeekDay
    {
        public string day { get; set; }
        public List<NutritionInfo> lstNutritionInfo { get; set; }
    }
}
