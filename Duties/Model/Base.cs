using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duties.Model
{
    abstract class Base
    {
        protected string table = "";
        protected string stm = "";

        public string Table
        {
            get
            {
                return table;
            }
        }

        public Base()
        {
            stm = "SELECT * FROM " + table;
        }

        public DataTable Select()
        {
            using (MySqlDataAdapter da = new MySqlDataAdapter(stm, Database.Instance.Connection))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
