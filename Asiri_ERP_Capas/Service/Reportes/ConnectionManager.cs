using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Service.Reportes
{
    public class ConnectionManager
    {
        public static SqlConnection GetConnection()
        {
            string lStrCadConex = ConfigurationManager.ConnectionStrings["AsiriContext"].ConnectionString;
            SqlConnection connection = new SqlConnection(lStrCadConex);
            return connection;
        }
    }
}
