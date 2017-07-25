using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Asiri_ERP_Capas.DA
{
    public class AspNetUserRolesDA
    {
        public List<AspNetUserRolesBE> obtenerPermisos(string userId)
        {
            List<AspNetUserRolesBE> permisosList = new List<AspNetUserRolesBE>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
            {
                con.Open(); 
                string query = "select * from AspNetUserRoles where UserId = '" + userId + "'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        AspNetUserRolesBE p;
                        while (reader.Read())
                        {
                            p = new AspNetUserRolesBE();
                            p.UserId = (string)reader["UserId"];
                            p.RoleId = (string)reader["RoleId"];
                            permisosList.Add(p);
                        }
                    }
                }


                con.Close();
            }
            return permisosList;
        }

    }
}