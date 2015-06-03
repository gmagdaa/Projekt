using Duties.Base;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Duties.Model
{
    class Duty: Base
    {
        public int day;
        public string from;
        public string to;
        int duty_day_int;
        string duty_day_string;
        public Duty()
        {
            table = "con_duties";
            stm = string.Format("SELECT duty_day, duty_time_from, duty_time_to", table);
        }

        public DataTable SelectByStaff(int id)
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "SELECT duty_id as id, IF(duty_day = 1, 'Poniedziałek', IF(duty_day = 2, 'Wtorek', IF(duty_day = 3, 'Środa', IF(duty_day = 4, 'Czwartek', IF(duty_day = 5, 'Piątek', IF(duty_day = 6, 'sobota', IF(duty_day = 7, 'niedziela', duty_day))))))) AS day, duty_time_from AS time_from, duty_time_to AS time_to FROM con_duties  cd where cd.staff_id = @staff_id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@staff_id", id);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;
                }

            }
        }

        
        public object[] SelectDuty(int id)
        {
            object[] duty = new object[3]; 
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "select duty_day, duty_time_from, duty_time_to from con_duties where duty_id = @duty_id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@duty_id", id);
                
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    day = rdr.GetInt32(0);
                    from = rdr.GetString(1);
                    to = rdr.GetString(2);
                }

                duty_day_string = deconvert(day);
                duty[0] = duty_day_string;
                duty[1] = from;
                duty[2] = to;
                
                rdr.Close();

            }
           
            return duty;
        }


        
        public List<string[]> SelectDutyForPrint()
        {
            
            List<string[]> staff_list = new List<string[]>();
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "select 	DE.degree_pl_desc, 	S.staff_first_name, 	S.staff_last_name,	D.duty_day,	D.duty_time_from,	D.duty_time_to from 	con_staff S, 	con_degrees DE,	con_duties D where 	S.degree_id = DE.degree_id 	and S.staff_id = D.staff_id order by S.staff_last_name, S.staff_first_name, D.duty_day";
                cmd.Prepare();


                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string[] staff = new string[6]; 
                    staff[0] = rdr.GetString(0);
                    staff[1] = rdr.GetString(1);
                    staff[2] = rdr.GetString(2);
                    staff[3] = rdr.GetString(3);
                    staff[4] = rdr.GetString(4);
                    staff[5] = rdr.GetString(5);
                    staff_list.Add(staff);
                }

                rdr.Close();

            }

            return staff_list;
        } 

        public void AddDuty(int staff_id, string duty_day, string duty_time_from, string duty_time_to)
        {
            duty_day_int = convert(duty_day);
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "INSERT INTO con_duties(staff_id, duty_day, duty_time_from, duty_time_to) VALUES(@staff_id, @duty_day, @duty_time_from, @duty_time_to)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@staff_id", staff_id);
                cmd.Parameters.AddWithValue("@duty_day", duty_day_int);
                cmd.Parameters.AddWithValue("@duty_time_from", duty_time_from);
                cmd.Parameters.AddWithValue("@duty_time_to", duty_time_to);
                cmd.ExecuteNonQuery();
            } 
        }

        
        public bool message = false;
        public void DeleteDuty(int duty_id)
        {
            

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = Database.Instance.Connection;
                    cmd.CommandText = "Delete from con_duties where duty_id = @duty_id";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@duty_id", duty_id);

                    cmd.ExecuteNonQuery();
                }
            
        }

        public void UpdateDuty(int duty_id, string duty_day, string duty_time_from, string duty_time_to)
        {
            duty_day_int = convert(duty_day);
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Database.Instance.Connection;
                cmd.CommandText = "update con_duties set duty_day = @duty_day, duty_time_from = @duty_time_from, duty_time_to = @duty_time_to where duty_id = @duty_id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@duty_id", duty_id);
                cmd.Parameters.AddWithValue("@duty_day", duty_day_int);
                cmd.Parameters.AddWithValue("@duty_time_from", duty_time_from);
                cmd.Parameters.AddWithValue("@duty_time_to", duty_time_to);
                
                cmd.ExecuteNonQuery();
            }
        }

        public int convert(string duty_day)
        {
            if (duty_day == "Poniedziałek")
                duty_day_int = 1;
            if (duty_day == "Wtorek")
                duty_day_int = 2;
            if (duty_day == "Środa")
                duty_day_int = 3;
            if (duty_day == "Czwartek")
                duty_day_int = 4;
            if (duty_day == "Piątek")
                duty_day_int = 5;
            return duty_day_int;
        }

        public string deconvert(int duty_day)
        {
            if (duty_day == 1)
                duty_day_string = "Poniedziałek";
            if (duty_day == 2)
                duty_day_string = "Wtorek";
            if (duty_day == 3)
                duty_day_string = "Środa";
            if (duty_day == 4)
                duty_day_string = "Czwartek";
            if (duty_day == 5)
                duty_day_string = "Piątek";
            return duty_day_string;
        }
        
    }
}

