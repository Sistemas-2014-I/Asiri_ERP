using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using Common.Reportes;

namespace Service.Reportes
{
    public class EmpleadoDA
    {
        public List<EmpleadoBE> ReporteMedico(string dato)
        {
            SqlConnection oSqlConnection;
            List<EmpleadoBE> olist = new List<EmpleadoBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHUt01_EMPLEADO_TOPMEDICOS", oSqlConnection);
                oSqlCommand.Parameters.Add("@num", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    EmpleadoBE oCliente = new EmpleadoBE();
                    oCliente.codEmpleado = (string)oSqlDataReader["CodEmpleado"];
                    oCliente.descTipoDeEmpleado= (String)oSqlDataReader["descTipoDeEmpleado"];
                    oCliente.nombrePersona = (String)oSqlDataReader["nombrePersona"];
                    oCliente.apellidoPaterno= (String)oSqlDataReader["apellidoPaterno"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    oCliente.apellidoMaterno = (String)oSqlDataReader["apellidoMaterno"].ToString();
                    oCliente.numDocIdentidad = (string)oSqlDataReader["numDocIdentidad"];
                    oCliente.NumeroCita = (int)oSqlDataReader["NumeroCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}