using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duties.Model
{
    class Staff : Base
    {
        public Staff()
        {
            table = "con_staff";
            stm = string.Format("SELECT staff_id AS id, CONCAT(staff_first_name, ' ', staff_last_name) AS name FROM {0} ORDER BY staff_last_name", table);
        }

        public DataTable SelectByUnit(int id)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "SELECT con_staff.staff_id AS id, CONCAT(staff_first_name, ' ', staff_last_name) AS name FROM con_staff, con_staff_roles WHERE con_staff.staff_id = con_staff_roles.staff_id AND con_staff_roles.role_id = 1 AND con_staff_roles.unit_id = @unit_id ORDER BY staff_last_name";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@unit_id", id);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }

            }
        }
    }
}
