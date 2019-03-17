using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Survey
    {
        private readonly static DAL.Survey dal = new DAL.Survey();

        public static List<SurveyInfo> SurveyMaster(string buGuid, string siteGuid, string type, string language)
        {
            return dal.surveyMaster(buGuid, siteGuid, type,language);
        }

        //更新数据
        public static int Update(SurveyDetails info)
        {
            return dal.UpdateResponse(info);
            
        }
    }
}