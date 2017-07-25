using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Reportes;
using System.Data.SqlClient;
using System.Data;

namespace Service.Reportes
{
   public class CitaDA
    {
        public List<CitaBE> ReporteEstadoCita(string dato)
        {
            SqlConnection oSqlConnection;
            List<CitaBE> olist = new List<CitaBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_CLl_CITA_ESTADO", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    CitaBE oCliente = new CitaBE();

                    oCliente.nombreCompleto = (String)oSqlDataReader["NombreCompleto"].ToString();
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.nombreDistrito = (String)oSqlDataReader["nombreDistrito"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    oCliente.fecRegistro = (DateTime)oSqlDataReader["fecRegistro"];
                    oCliente.descEstadoCita = (String)oSqlDataReader["descEstadoCita"];
                    //oCliente.idEstadoCita = (int)oSqlDataReader["idEstadoCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}
