using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Entity;
using Luoyi.Common;
using System.Data;

namespace Luoyi.DAL
{
    public class ItemCustomClass
    {

        public ItemCustomClassInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblItemCustomClass WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemCustomClassInfo>();
            }
        }

        public List<ItemCustomClassInfo> GetList()
        {
            string sql = "SELECT * FROM tblItemCustomClass WHERE IsStop = 0 ORDER BY Sort DESC";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemCustomClassInfo>();
            }
        }

    }
}