using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Reportes
{
    public class RepBusFecCitaDA
    {
        public List<RepBusFecCitaBE> ReporteFechaPac(DateTime Fecha1, DateTime Fecha2)
        {
            SqlConnection oSqlConnection;
            List<RepBusFecCitaBE> olist = new List<RepBusFecCitaBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_CLI_CITA_BUSCARRANGOFECHACITA", oSqlConnection);
                oSqlCommand.Parameters.Add("@Fecha1", SqlDbType.DateTime).Value = Fecha1;
                oSqlCommand.Parameters.Add("@Fecha2", SqlDbType.DateTime).Value = Fecha2;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepBusFecCitaBE oCliente = new RepBusFecCitaBE();
                    oCliente.idCita = (Int64)oSqlDataReader["idCita"];
                    oCliente.fecCita = (DateTime)oSqlDataReader["fecCita"];
                    oCliente.fecRegistro = (DateTime)oSqlDataReader["fecRegistro"];
                    oCliente.horaInicio = (String)oSqlDataReader["horaInicio"];
                    oCliente.mtoTotal = (Decimal)oSqlDataReader["mtoTotal"];
                    oCliente.descEstadoCita = (String)oSqlDataReader["descEstadoCita"];
                    oCliente.Paciente = (String)oSqlDataReader["Paciente"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}