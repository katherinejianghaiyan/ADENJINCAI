using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    public class NutritionList
    {
        public string startDate { get; set; }       
        public string endDate { get; set; }
        public string dataType { get; set; }
        public string img { get; set; }
        public List<NutritionWeekDay> lstNutritionWeekDay { get; set; }

        public static implicit operator NutritionList(MealDateList v)
        {
            throw new NotImplementedException();
        }
    }
}
