using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVFService
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/2 11:27:27
    /// </summary>
    public class DataBaseConnection
    {
        public static DbConnection GetSqlserverConnection()
        {
            string connectionStr = @"Data Source=192.168.10.177\SQL2008; Initial Catalog=YYQ_MVF;User Id=sa; password=yyq123";
            var connection = new SqlConnection(connectionStr);
            connection.Open();
            return connection;
        }
    }
}
