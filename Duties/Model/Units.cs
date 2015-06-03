using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duties.Model
{
    class Units : Base
    {
        public Units()
        {
            table = "con_units";
            stm = "SELECT unit_id AS id, unit_pl_name AS name FROM con_units ORDER BY unit_pl_name";
        }
    }
}
