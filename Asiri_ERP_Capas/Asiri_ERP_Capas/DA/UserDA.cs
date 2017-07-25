using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Asiri_ERP_Capas.ViewModels;
using Common;

namespace Asiri_ERP_Capas.DA
{
    public class UserDA
    {
        private AsiriContext db = new AsiriContext();

        public bool RegistrarUsuarioRol(UserRolViewModel UserRol)
        {
            bool respuesta = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@p0, @p1)", con);
                    query.Parameters.AddWithValue("@p0", UserRol.UserId);
                    query.Parameters.AddWithValue("@p1", UserRol.RolId);
                    query.ExecuteNonQuery();
                    respuesta = true;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return respuesta;
        }


        public bool ActualizarRolUser(string user, string rol)
        {
            bool respuesta = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("UPDATE AspNetUserRoles set RoleId = @p0 WHERE UserId = @p1", con);
                    query.Parameters.AddWithValue("@p0", rol);
                    query.Parameters.AddWithValue("@p1", user);
                    query.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return respuesta;
        }


        public bool EliminarUserRol(string id)
        {
            bool respuesta = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("DELETE FROM AspNetUserRoles WHERE UserId = @p0", con);
                    query.Parameters.AddWithValue("@p0", id);
                    query.ExecuteNonQuery();
                    respuesta = true;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public bool tieneRol(string RoleId, string id)
        {
            bool flag = false;
            if (obtenerRoles(id) == RoleId)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        public string obtenerRoles(string userId)
        {
            string rolesList = "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
            {
                con.Open();
                string query =
                    "select r.Id from AspnetUserRoles ur inner join AspNetRoles r on r.Id = ur.RoleId where ur.UserId = @p";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@p", SqlDbType.NVarChar);
                cmd.Parameters["@p"].Value = userId;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rolesList = (string) reader["Id"];
                }
                con.Close();
            }
            return rolesList;
        }
    }
}
