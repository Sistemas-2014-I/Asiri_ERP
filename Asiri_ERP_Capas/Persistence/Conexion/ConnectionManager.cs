using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Conexion
{
    public class ConnectionManager
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AsiriContext"].ConnectionString;
        }
    }
}
