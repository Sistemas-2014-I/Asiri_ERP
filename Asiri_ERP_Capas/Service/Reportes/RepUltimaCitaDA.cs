using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Service.Reportes
{
    public class RepUltimaCitaDA
    {
        public List<RepUltimaCitaBE> ReporteUltimaCita(int Dias)
        {
            SqlConnection oSqlConnection;
            List<RepUltimaCitaBE> olist = new List<RepUltimaCitaBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("TotalDiasSinVenir", oSqlConnection);
                oSqlCommand.Parameters.Add("@Dias", SqlDbType.VarChar).Value = Dias;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepUltimaCitaBE oCliente = new RepUltimaCitaBE();
                    oCliente.Paciente = (String)oSqlDataReader["Paciente"].ToString();
                    oCliente.UltimaCita = (DateTime)oSqlDataReader["UltimaCita"];
                    oCliente.direccion01 = (String)oSqlDataReader["direccion01"].ToString();
                    oCliente.numTelefonico01 = (String)oSqlDataReader["numTelefonico01"].ToString();
                    oCliente.direccion02 = (String)oSqlDataReader["direccion02"].ToString();
                    oCliente.numTelefonico02 = (String)oSqlDataReader["numTelefonico02"].ToString();
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}