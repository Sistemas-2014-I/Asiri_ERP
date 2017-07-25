using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Reportes
{
    public class RepEvoPacDA
    {
        public List<RepEvoPacBE> ReporteEvolucionPaciente(string dato)
        {
            SqlConnection oSqlConnection;
            List<RepEvoPacBE> olist = new List<RepEvoPacBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_CLI_EVOLUCION_PACIENTE", oSqlConnection);
                oSqlCommand.Parameters.Add("@num", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepEvoPacBE oCliente = new RepEvoPacBE();
                    oCliente.idAtencion = (Int64)oSqlDataReader["idAtencion"];
                    oCliente.descEvolucion = (String)oSqlDataReader["descEvolucion"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    oCliente.Paciente = (String)oSqlDataReader["Paciente"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.descDiagnostico = (String)oSqlDataReader["descDiagnostico"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}