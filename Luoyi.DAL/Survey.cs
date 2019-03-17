using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.DAL
{
    public class Survey
    {
        public List<SurveyInfo> surveyMaster(string buGUID, string siteGuid, string type, string language)
        {
            List<SurveyInfo> surveyList = new List<SurveyInfo>();

            #region Sql文

            string strSql = "SELECT A1.SITEGUID,{3}WELCOMETEXT,A1.SURVEYTITLE,A2.LINEGUID,{4}ITEMNAME,{5}ITEMANSWER,A2.SORT, " +
                     "CONVERT(VARCHAR(10),A2.CREATETIME,23) CREATETIME,A2.CREATEUSER,CAST(A2.ALLOWNULL AS INT) ALLOWNULL,A2.ITEMSTYLE " +
                    "FROM tblSurveyHeadDef (nolock) A1, tblSurveyLinesDef (nolock) A2 " +
                    "WHERE A1.HeadGUID=A2.HeadGUID " +
                     "AND (A1.SiteGUID='{0}' or A1.BUGUID='{1}') AND A2.DELETEUSER IS NULL {2} ORDER BY A2.SORT";

            strSql = string.Format(strSql, siteGuid, buGUID, 
                string.IsNullOrEmpty(type)? "" : string.Format( "and a1.surveytype='{0}'" ,type),
                language == "ZH_CN"? "A1." : "ISNULL(A1.WELCOMETEXT_EN,A1.WELCOMETEXT) ",
                language == "ZH_CN" ? "A2." : "ISNULL(A2.ITEMNAME_EN,A2.ITEMNAME) ",
                language == "ZH_CN" ? "A2." : "ISNULL(A2.ITEMANSWER_EN,A2.ITEMANSWER) ");

            #endregion
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden2, CommandType.Text, strSql))
            {
                surveyList = reader.ToEntityList<SurveyInfo>();
            }
            if (surveyList == null || !surveyList.Any())
                return surveyList;

            //如果有按Site定义内容的，则仅取Site
            if (surveyList.Any(q => q.siteGuid == siteGuid))
                surveyList = surveyList.Where(q => q.siteGuid == siteGuid).ToList();

            var lg = (from row in surveyList
                      group row by new
                      {
                          siteGuid = row.siteGuid,
                          welcomeText = row.welcomeText,
                          surveyTitle = row.surveyTitle
                      }).Select(dr => new SurveyInfo()
                      {
                          siteGuid = dr.Key.siteGuid,
                          welcomeText = dr.Key.welcomeText,
                          surveyTitle = dr.Key.surveyTitle,
                          surveyLines = surveyList.Select(gr => new SurveyDetails()
                          {
                              lineGuid = gr.lineGuid,
                              itemName = gr.itemName,
                              itemAnswer = gr.itemAnswer,
                              allowNull = gr.allowNull,
                              itemStyle = gr.itemStyle
                          }).ToList()
                      }).ToList();

            return lg;
        }
        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateResponse(SurveyDetails info)
        {
            string createUser = string.IsNullOrEmpty(info.action) ? info.createUser:"";
            string userName = info.userName;
            string userDept = info.userDept;
            string siteGuid = info.siteGuid;
            string headGuid = Guid.NewGuid().ToString();
            List<object> details = info.details;
            
         
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            StringBuilder sb = new StringBuilder();

            if (details.Count == 0)
                return 0;
            foreach (object item in details)
            {

                string s = (string)item;
                
                int idIndex = s.IndexOf("'id':");
                string lineGuid = s.Substring(idIndex+6,36);
                int ansSTIndex = s.IndexOf("'answer':") + 10;
                int ansEDIndex = s.IndexOf("'}") - ansSTIndex;
                string userAnswer = s.Substring(ansSTIndex, ansEDIndex);

                //  新建记录
                string sql = string.Format("INSERT INTO TBLSURVEYRESPONSEDETAILS (HEADGUID,LINEGUID,USERANSWER,CREATETIME,CREATEUSER,SITEGUID) "
                    + "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", string.IsNullOrEmpty(info.headGuid)?headGuid:info.headGuid, lineGuid, userAnswer, dateTime, createUser, siteGuid);
                sb.Append(sql);
            }
            if (string.IsNullOrEmpty(info.action) && !string.IsNullOrEmpty(info.headGuid))
            {
                string sql = string.Format("UPDATE TBLSURVEYRESPONSEDETAILS SET CREATEUSER = '{0}' WHERE HEADGUID = '{1}'",
                   info.createUser, info.headGuid);
                sb.Append(sql);
            }

            int i = SqlHelper.ExecuteSql(SqlHelper.dbAden2, CommandType.Text, sb.ToString());
            
            return i;
        }



    }
}