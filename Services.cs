using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data;
using System.Data;
using Dapper;
using System.IO;

namespace sqlShop
{
    public static class Services
    {
        public static FbConnectionStringBuilder connection_string = new FbConnectionStringBuilder
        {
            Charset = "win1251",
            UserID = "sysdba",
            Password = "masterkey",
            Database = @"LOCALHOST:" + Directory.GetCurrentDirectory() + @"\TOVARI.FDB",

            ServerType = FbServerType.Default
        };
        static FbConnection fb;

        public static FbConnection GetFbConnection() => fb;

        public static void openConn()
        {
            try
            {
                fb = new FbConnection(connection_string.ToString());
                fb.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        internal static DataTable GetTable_Tovary()
        {
            FbDataAdapter adapter = new FbDataAdapter("select * from tovary", GetFbConnection());
            var table = new DataTable();

            adapter.Fill(table);

            return table;
        }
        internal static DataTable GetTable_Clients()
        {
            FbDataAdapter adapter = new FbDataAdapter("select * from pokupat order by npok", GetFbConnection());
            var table = new DataTable();

            adapter.Fill(table);

            return table;
        }
        internal static DataTable GetTable_Rashod()
        {
            FbDataAdapter adapter = new FbDataAdapter("select * from rashod order by nras", GetFbConnection());
            var table = new DataTable();

            adapter.Fill(table);

            return table;
        }
        /// <summary>
        /// Возвращает таблицу прихода
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetTable_Prihod()
        {
            FbDataAdapter adapter = new FbDataAdapter("select * from prihod order by npr", GetFbConnection());
            var table = new DataTable();

            adapter.Fill(table);

            return table;
        }
        /// <summary>
        /// Возвращает массив всеизвестных товаров с таблицы tovary
        /// </summary>
        /// <returns></returns>
        internal static string[] GetArrayOfProducts()
        {
            return Services.GetFbConnection().Query<String>("select tovar from tovary").ToArray();
        }
    }
}
