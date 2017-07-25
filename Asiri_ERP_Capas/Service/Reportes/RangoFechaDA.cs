using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common.Reportes;

namespace Service.Reportes
{
    public class RangoFechaDA
    {
        public List<RangoFechaBE> ReporteFechaPac(DateTime fecha1, DateTime fecha2)
        {
            SqlConnection oSqlConnection;
            List<RangoFechaBE> olist = new List<RangoFechaBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_PACIENTE_REPFECHAS", oSqlConnection);
                oSqlCommand.Parameters.Add("@fecha1", SqlDbType.DateTime).Value = fecha1;
                oSqlCommand.Parameters.Add("@fecha2", SqlDbType.DateTime).Value = fecha2;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RangoFechaBE oCliente = new RangoFechaBE();
                    oCliente.descEstadoCivil = (String)oSqlDataReader["descEstadoCivil"];
                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.nombreDistrito = (String)oSqlDataReader["nombreDistrito"];
                    oCliente.nombreProvincia = (String)oSqlDataReader["nombreProvincia"];
                    oCliente.nombreRegion = (String)oSqlDataReader["nombreRegion"];
                    oCliente.descZona = (String)oSqlDataReader["descZona"];
                    oCliente.nombreCompleto = (String)oSqlDataReader["nombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.direccion01 = (String)oSqlDataReader["direccion01"].ToString();
                    oCliente.fecRegistro = (DateTime)oSqlDataReader["fecRegistro"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }

        }

    }
}