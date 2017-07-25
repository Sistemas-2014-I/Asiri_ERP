using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Clinica
{
    public class ConnectionManager
    {
        public static SqlConnection GetConnection()
        {
            string lStrCadConex = ConfigurationManager.ConnectionStrings["Asiri_ERPConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(lStrCadConex);
            return connection;
        }
    }
}
