using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service.Reportes
{
    public class EspecialidadDA
    {
        public List<EspecialidadBE> ReporteEspecialidad(string dato)
        {
            SqlConnection oSqlConnection;
            List<EspecialidadBE> olist = new List<EspecialidadBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_EMPLEADO_TOPESPECIALIDAD", oSqlConnection);
                oSqlCommand.Parameters.Add("@nume", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    EspecialidadBE oCliente = new EspecialidadBE();
                    oCliente.codEmpleado = (string)oSqlDataReader["CodEmpleado"];
                    oCliente.descTipoDeEmpleado = (String)oSqlDataReader["descTipoDeEmpleado"];
                    oCliente.nombrePersona = (String)oSqlDataReader["nombrePersona"];
                    oCliente.apellidoPaterno = (String)oSqlDataReader["apellidoPaterno"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    oCliente.apellidoMaterno = (String)oSqlDataReader["apellidoMaterno"].ToString();
                    oCliente.numDocIdentidad = (string)oSqlDataReader["numDocIdentidad"];
                    oCliente.nombreEspecialidad = (string)oSqlDataReader["nombreEspecialidad"];
                    oCliente.CantidadCita = (int)oSqlDataReader["CantidadCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}