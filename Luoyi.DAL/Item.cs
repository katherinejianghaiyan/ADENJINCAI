using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class Item
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItem(");
            builder.Append("ItemID,GUID,ParentGUID,ItemType,ToBuy,ToSell,ItemName,ItemCode,Price,ClassGUID,Image1,Image2,Image3,DishSize,Weight,Container,Cooking,Nutrition,Tips,Loss,ItemSpec,PurUOMGUID,SaleUOMGUID,Sort,Status,CreateTime,Sales,Score,LastUpdate,IsDel)");
            builder.Append("VALUES (");
            builder.Append("@ItemID,@GUID,@ParentGUID,@ItemType,@ToBuy,@ToSell,@ItemName,@ItemCode,@Price,@ClassGUID,@Image1,@Image2,@Image3,@DishSize,@Weight,@Container,@Cooking,@Nutrition,@Tips,@Loss,@ItemSpec,@PurUOMGUID,@SaleUOMGUID,@Sort,@Status,@CreateTime,@Sales,@Score,@LastUpdate,@IsDel)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemID",SqlDbType.Int,4) {Value =  info.ItemID},
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@ParentGUID",SqlDbType.VarChar,32) {Value =  info.ParentGUID},
					 new SqlParameter("@ItemType",SqlDbType.Int,4) {Value =  info.ItemType},
					 new SqlParameter("@ToBuy",SqlDbType.Bit,1) {Value =  info.ToBuy},
					 new SqlParameter("@ToSell",SqlDbType.Bit,1) {Value =  info.ToSell},
					 new SqlParameter("@ItemName",SqlDbType.VarChar,128) {Value =  info.ItemName},
					 new SqlParameter("@ItemCode",SqlDbType.VarChar,32) {Value =  info.ItemCode},
					 new SqlParameter("@Price",SqlDbType.Decimal,9) {Value =  info.Price},
					 new SqlParameter("@ClassGUID",SqlDbType.VarChar,32) {Value =  info.ClassGUID},
					 new SqlParameter("@Image1",SqlDbType.VarChar,64) {Value =  info.Image1},
					 new SqlParameter("@Image2",SqlDbType.VarChar,64) {Value =  info.Image2},
					 new SqlParameter("@Image3",SqlDbType.VarChar,64) {Value =  info.Image3},
					 new SqlParameter("@DishSize",SqlDbType.VarChar,16) {Value =  info.DishSize},
					 new SqlParameter("@Weight",SqlDbType.Int,4) {Value =  info.Weight},
					 new SqlParameter("@Container",SqlDbType.VarChar,16) {Value =  info.Container},
					 new SqlParameter("@Cooking",SqlDbType.VarChar,1024) {Value =  info.Cooking},
					 new SqlParameter("@Nutrition",SqlDbType.VarChar,1024) {Value =  info.Nutrition},
					 new SqlParameter("@Tips",SqlDbType.VarChar,512) {Value =  info.Tips},
					 new SqlParameter("@Loss",SqlDbType.Int,4) {Value =  info.Loss},
					 new SqlParameter("@ItemSpec",SqlDbType.VarChar,32) {Value =  info.ItemSpec},
					 new SqlParameter("@PurUOMGUID",SqlDbType.VarChar,32) {Value =  info.PurUOMGUID},
					 new SqlParameter("@SaleUOMGUID",SqlDbType.VarChar,32) {Value =  info.SaleUOMGUID},
					 new SqlParameter("@Sort",SqlDbType.Int,4) {Value =  info.Sort},
					 new SqlParameter("@Status",SqlDbType.Int,4) {Value =  info.Status},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@Sales",SqlDbType.Int,4) {Value =  info.Sales},
					 new SqlParameter("@Score",SqlDbType.Decimal,9) {Value =  info.Score},
					 new SqlParameter("@LastUpdate",SqlDbType.DateTime) {Value =  info.LastUpdate},
					 new SqlParameter("@IsDel",SqlDbType.Bit,1) {Value =  info.IsDel}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItem SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("ParentGUID=@ParentGUID,");
            builder.Append("ItemType=@ItemType,");
            builder.Append("ToBuy=@ToBuy,");
            builder.Append("ToSell=@ToSell,");
            builder.Append("ItemName=@ItemName,");
            builder.Append("ItemCode=@ItemCode,");
            builder.Append("Price=@Price,");
            builder.Append("ClassGUID=@ClassGUID,");
            builder.Append("Image1=@Image1,");
            builder.Append("Image2=@Image2,");
            builder.Append("Image3=@Image3,");
            builder.Append("DishSize=@DishSize,");
            builder.Append("Container=@Container,");
            builder.Append("Cooking=@Cooking,");
            builder.Append("Nutrition=@Nutrition,");
            builder.Append("Tips=@Tips,");
            builder.Append("Loss=@Loss,");
            builder.Append("ItemSpec=@ItemSpec,");
            builder.Append("PurUOMGUID=@PurUOMGUID,");
            builder.Append("SaleUOMGUID=@SaleUOMGUID,");
            builder.Append("Sort=@Sort,");
            builder.Append("Status=@Status,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Sales=@Sales,");
            builder.Append("Score=@Score,");
            builder.Append("LastUpdate=@LastUpdate ");
            builder.Append("WHERE ItemID=@ItemID ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@ParentGUID",SqlDbType.VarChar,32){Value =  info.ParentGUID},
					 new SqlParameter("@ItemType",SqlDbType.Int,4){Value =  info.ItemType},
					 new SqlParameter("@ToBuy",SqlDbType.Bit,1){Value =  info.ToBuy},
					 new SqlParameter("@ToSell",SqlDbType.Bit,1){Value =  info.ToSell},
					 new SqlParameter("@ItemName",SqlDbType.VarChar,128){Value =  info.ItemName},
					 new SqlParameter("@ItemCode",SqlDbType.VarChar,32){Value =  info.ItemCode},
					 new SqlParameter("@Price",SqlDbType.Decimal,9){Value =  info.Price},
					 new SqlParameter("@ClassGUID",SqlDbType.VarChar,32){Value =  info.ClassGUID},
					 new SqlParameter("@Image1",SqlDbType.VarChar,64){Value =  info.Image1},
					 new SqlParameter("@Image2",SqlDbType.VarChar,64){Value =  info.Image2},
					 new SqlParameter("@Image3",SqlDbType.VarChar,64){Value =  info.Image3},
					 new SqlParameter("@DishSize",SqlDbType.VarChar,16){Value =  info.DishSize},
					 new SqlParameter("@Container",SqlDbType.VarChar,16){Value =  info.Container},
					 new SqlParameter("@Cooking",SqlDbType.VarChar,1024){Value =  info.Cooking},
					 new SqlParameter("@Nutrition",SqlDbType.VarChar,1024){Value =  info.Nutrition},
					 new SqlParameter("@Tips",SqlDbType.VarChar,512){Value =  info.Tips},
					 new SqlParameter("@Loss",SqlDbType.Int,4){Value =  info.Loss},
					 new SqlParameter("@ItemSpec",SqlDbType.VarChar,32){Value =  info.ItemSpec},
					 new SqlParameter("@PurUOMGUID",SqlDbType.VarChar,32) {Value =  info.PurUOMGUID},
					 new SqlParameter("@SaleUOMGUID",SqlDbType.VarChar,32) {Value =  info.SaleUOMGUID},
					 new SqlParameter("@Sort",SqlDbType.Int,4){Value =  info.Sort},
					 new SqlParameter("@Status",SqlDbType.Int,4){Value =  info.Status},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime){Value =  info.CreateTime},
					 new SqlParameter("@Sales",SqlDbType.Int,4){Value =  info.Sales},
					 new SqlParameter("@Score",SqlDbType.Decimal,9){Value =  info.Score},
					 new SqlParameter("@LastUpdate",SqlDbType.DateTime){Value =  info.LastUpdate},
					 new SqlParameter("@ItemID",SqlDbType.Int,4){Value =  info.ItemID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemInfo GetInfo(int itemID,string language)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblItem WHERE ItemID= {0}", itemID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemInfo>(language);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemInfo GetInfo(string guid)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblItem WHERE GUID= '{0}'", guid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemInfo>();
            }
        }

        public List<ItemInfo> GetList(string filter)
        {
            string sql = string.Format("SELECT * FROM tblItem WHERE IsDel = 0 {0} ORDER BY SORT DESC", filter);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemInfo>();
            }
        }

        /// <summary>
        /// 返回当前页码的数据集表
        /// </summary> 
        /// <returns>当前页码的数据集</returns>
        public DataTable GetPage(ItemFilter filter, out int recordCount)
        {            
            var fileds = string.IsNullOrEmpty(filter.Columns) ? 
                "i.*,p.Price AS ItemPrice,ip.sortnbr,ip.dayofweek" //,h.Name as HolidayName
                : filter.Columns;
            var table = string.IsNullOrEmpty(filter.TableName) ?
                "tblItem (nolock) i JOIN tblItemPrice (nolock) p ON p.ItemGUID=i.GUID JOIN tblSite (nolock) s "
                + "ON isnull(case s.itempriceinbu when 1 then p.BUGUID else p.siteguid end,'') = case s.itempriceinbu when 1 then s.BUGUID else s.guid end "//LEFT JOIN tblHldyMeal (nolock) h on i.HLDYMEALGUID=h.GUID "
                + "left join tblitemplan (nolock) ip on i.guid=ip.itemguid and s.guid=ip.siteguid "
                + "left join tblitemclass (nolock) ic on i.classguid=ic.guid "
                
                //+ (string.IsNullOrEmpty(filter.UserType) ? "" : 
                //    string.Format("JOIN tblitemplan (nolock) ip on i.guid=ip.itemGUID and ip.usertype='{0}'"/* and ip.sortnbr in ({1})"*/, filter.UserType))//, filter.ItemSortNbrs)) //每天显示不同的菜品
                : filter.TableName;
            var order = string.IsNullOrEmpty(filter.OrderBy) ? "ic.sort,i.Sort " : filter.OrderBy;
            var builder = new StringBuilder(" i.IsDel = 0 AND i.ToSell = 1");
            builder.AppendFormat(" AND p.StartDate <={0} AND (p.EndDate>={0} OR p.EndDate IS NULL)", DateTime.Now.ToInt());
            builder.Append(string.IsNullOrEmpty(filter.Keyword) ? "" : string.Format(" AND i.ItemName LIKE '%{0}%'", filter.Keyword));
            
            builder.Append(FilterHelper.BuilderFilter<ItemFilter>(filter));
            builder.Append(filter.Filter);
            return SqlHelper.GetPage(SqlHelper.dbAden, table, filter.PageSize, filter.PageIndex, fileds, builder.ToString(), order, out recordCount).Tables[0];
        }
    }
}

