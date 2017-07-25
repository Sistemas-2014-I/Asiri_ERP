using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Asiri_ERP_Capas.ViewModels;

namespace Asiri_ERP_Capas.DA
{
    public class RolDA
    {
        private AsiriContext db = new AsiriContext();
        public bool RegistrarRolPermisos(List<RolPermisoViewModel> RolPermisos)
        {
            bool respuesta = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();

                    foreach (var rp in RolPermisos)
                    {
                        var query = new SqlCommand("INSERT INTO AspNetRolesPermisos(RoleId, PermisoId) VALUES (@p0, @p1)", con);

                        query.Parameters.AddWithValue("@p0", rp.RoleId);
                        query.Parameters.AddWithValue("@p1", rp.PermisoId);
                        query.ExecuteNonQuery();

                    }
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return respuesta;
        }


        public bool ActualizarRolPermisos(List<RolPermisoViewModel> RolPermisos)
        {
            bool respuesta = false;

            //Permisos estoy enviando
            int permisos = RolPermisos.Count();

            //Permisos que tiene en la BD
            int permisosActuales = obtenerPermisos(RolPermisos[0].RoleId).Count();

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();

                    if (permisos == permisosActuales)
                    {

                        foreach (var rp in RolPermisos)
                        {
                            var query = new SqlCommand("UPDATE AspNetRolesPermisos set PermisoId = @p1  WHERE RoleId = @p0", con);
                            query.Parameters.AddWithValue("@p0", rp.RoleId);
                            query.Parameters.AddWithValue("@p1", rp.PermisoId);
                            query.ExecuteNonQuery();
                        }
                        respuesta = true;
                    }
                    else if (permisos < permisosActuales)
                    {
                        int a = 0;
                        for (int i = 0; i == permisosActuales - permisos - 1; i++)
                        {
                            var query = new SqlCommand("DELETE FROM AspNetRolesPermisos WHERE RoleId = @p0 and PermisoId = @p1", con);
                            query.Parameters.AddWithValue("@p0", RolPermisos[i].RoleId);
                            query.Parameters.AddWithValue("@p1", RolPermisos[i].PermisoId);
                            query.ExecuteNonQuery();
                            a++;
                        }
                        for (int j = 0; j == permisos; j++)
                        {
                            var query = new SqlCommand("UPDATE AspNetRolesPermisos set PermisoId = @p1  WHERE RoleId = @p0", con);
                            query.Parameters.AddWithValue("@p0", RolPermisos[j].RoleId);
                            query.Parameters.AddWithValue("@p1", RolPermisos[j].PermisoId);
                            query.ExecuteNonQuery();
                        }
                    }
                    else if (permisos > permisosActuales)
                    {
                        int a = 0;
                        for (int i = 0; i == permisosActuales - 1; i++)
                        {
                            var query = new SqlCommand("UPDATE AspNetRolesPermisos set PermisoId = @p1  WHERE RoleId = @p0", con);
                            query.Parameters.AddWithValue("@p0", RolPermisos[i].RoleId);
                            query.Parameters.AddWithValue("@p1", RolPermisos[i].PermisoId);
                            query.ExecuteNonQuery();
                            a++;
                        }
                        for (int j = a; j == permisos; j++)
                        {
                            var query = new SqlCommand("INSERT INTO AspNetRolesPermisos(RoleId, PermisoId) VALUES (@p0, @p1)", con);
                            query.Parameters.AddWithValue("@p0", RolPermisos[j].RoleId);
                            query.Parameters.AddWithValue("@p1", RolPermisos[j].PermisoId);
                            query.ExecuteNonQuery();
                        }
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return respuesta;
        }


        public bool EliminarRolPermisos(string id)
        {
            bool respuesta = false;
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
                {
                    con.Open();
                    var query = new SqlCommand("DELETE FROM AspNetRolesPermisos WHERE RoleId = @p0", con);
                    query.Parameters.AddWithValue("@p0", id);
                    query.ExecuteNonQuery();
                    con.Close();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public List<AspNetPermisos> obtenerPermisos(string rolId)
        {
            List<AspNetPermisos> permisosList = new List<AspNetPermisos>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
            {
                con.Open();
                string query = "select p.Id, p.Descripcion from AspNetRolesPermisos rp inner join AspNetPermisos p on rp.PermisoId = p.Id where rp.RoleId = @p";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@p", SqlDbType.NVarChar);
                cmd.Parameters["@p"].Value = rolId;
                SqlDataReader reader = cmd.ExecuteReader();
                AspNetPermisos p;
                while (reader.Read())
                {
                    p = new AspNetPermisos();
                    p.Id = (int)reader["Id"];
                    p.Descripcion = (string)reader["Descripcion"];
                    permisosList.Add(p);
                }
                con.Close();
            }
            return permisosList;
        }


        public bool tienePermiso(string RoleId, int id)
        {
            List<AspNetPermisos> permisosList = new List<AspNetPermisos>();
            bool flag = false;
            permisosList = obtenerPermisos(RoleId);
            if (permisosList.Where(x => x.Id.Equals(id)).Any())
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            return flag;
        }


        public List<AspNetPermisos> obtenerPermisosTotal(List<AspNetRoles> roles)
        {
            List<AspNetPermisos> permisosList = new List<AspNetPermisos>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
            {
                con.Open();
                foreach (var item in roles)
                {
                    string query = "select rp.PermisoId, p.Descripcion from AspNetRolesPermisos rp inner join AspNetPermisos p on rp.PermisoId = p.Id where rp.RoleId = @p";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@p", SqlDbType.NVarChar);
                    cmd.Parameters["@p"].Value = item.Id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    AspNetPermisos p;
                    while (reader.Read())
                    {
                        p = new AspNetPermisos();
                        p.Id = (int)reader["Id"];
                        p.Descripcion = (string)reader["Name"];
                        permisosList.Add(p);
                    }
                }
                con.Close();
            }
            return permisosList;
        }


        public List<AspNetRoles> obtenerRoles(string userId)
        {
            List<AspNetRoles> rolesList = new List<AspNetRoles>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AsiriContext"].ToString()))
            {
                con.Open();
                string query = "select r.Id, r.Name from AspnetUserRoles ur inner join AspNetRoles r on r.Id = ur.RoleId where ur.UserId = @p";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@p", SqlDbType.NVarChar);
                cmd.Parameters["@p"].Value = userId;
                SqlDataReader reader = cmd.ExecuteReader();
                AspNetRoles r;
                while (reader.Read())
                {
                    r = new AspNetRoles();
                    r.Id = (string)reader["Id"];
                    r.Name = (string)reader["Name"];
                    rolesList.Add(r);
                }
                con.Close();
            }
            return rolesList;
        }
    }
}