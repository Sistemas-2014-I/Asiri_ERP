using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service.Reportes
{
    public class EmpleadoReporteDA
    {
        public EmpleadoReporteBE EmpleadoReporte { get; set; }
        public List<EmpleadoReporteBE> ReportePersonal(string dato)
        {
            SqlConnection oSqlConnection;
            List<EmpleadoReporteBE> olist = new List<EmpleadoReporteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_Empleado_Reporte", oSqlConnection);
                oSqlCommand.Parameters.Add("@DocIdentidad", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    EmpleadoReporteBE oCliente = new EmpleadoReporteBE();
                    oCliente.descTipoDeEmpleado = (String)oSqlDataReader["descTipoDeEmpleado"];
                    oCliente.codEmpleado = (string)oSqlDataReader["CodEmpleado"];
                    oCliente.nombrePersona = (String)oSqlDataReader["nombrePersona"];
                    oCliente.apellidoPaterno = (String)oSqlDataReader["apellidoPaterno"];
                    oCliente.apellidoMaterno = (String)oSqlDataReader["apellidoMaterno"].ToString();
                    oCliente.numDocIdentidad = (string)oSqlDataReader["numDocIdentidad"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}