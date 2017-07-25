using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service.Reportes
{
    public class ServicioDA
    {
        public List<ServicioBE> ReporteServicio()
        {
            SqlConnection oSqlConnection;
            List<ServicioBE> olist = new List<ServicioBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_PRO_ReporteServicio", oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    ServicioBE oCliente = new ServicioBE();
                    oCliente.codServicio = (string)oSqlDataReader["codServicio"];
                    oCliente.nombreServicio = (String)oSqlDataReader["nombreServicio"];
                    oCliente.descTipoDeServicio = (String)oSqlDataReader["descTipoDeServicio"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}