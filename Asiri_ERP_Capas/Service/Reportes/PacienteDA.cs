using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common.Reportes;

namespace Service.Reportes
{
    public class PacienteDA
    {
        //Rep Estado paciente activo o inactivo
        public List<PacienteBE> ReporteEstadoPac(string dato)
        {
            SqlConnection oSqlConnection;
            List<PacienteBE> olist = new List<PacienteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_PACIENTE_LIST_INACTIVO", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    PacienteBE oCliente = new PacienteBE();

                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.descEstadoCivil = (String)oSqlDataReader["descEstadoCivil"];
                    oCliente.nombreDistrito = (String)oSqlDataReader["nombreDistrito"];
                    oCliente.nombreProvincia = (String)oSqlDataReader["nombreProvincia"];
                    oCliente.nombreRegion = (String)oSqlDataReader["nombreRegion"];
                    oCliente.descZona = (String)oSqlDataReader["descZona"];
                    oCliente.nombreCompleto = (String)oSqlDataReader["NombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    oCliente.fecRegistro = (DateTime)oSqlDataReader["fecRegistro"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }

        //top n de pacientes con mayor numero de citas
        public List<PacienteBE> RepTopN(string dato)
        {
            SqlConnection oSqlConnection;
            List<PacienteBE> olist = new List<PacienteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_PACIENTE_TOPN", oSqlConnection);
                oSqlCommand.Parameters.Add("@num", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    PacienteBE oCliente = new PacienteBE();

                    oCliente.nombreCompleto = (String)oSqlDataReader["NombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.nombreDistrito = (String)oSqlDataReader["nombreDistrito"];
                    oCliente.direccion = (String)oSqlDataReader["direccion"];
                    oCliente.NumeroCitas = (int)oSqlDataReader["NumeroCitas"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }

        //Reporte de paciente en particular 
        public List<PacienteBE> RepPacPartic(int dato,string dato1)
        {
            SqlConnection oSqlConnection;
            List<PacienteBE> olist = new List<PacienteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_PACIENTE_TIPO_NUM_DOC", oSqlConnection);
                oSqlCommand.Parameters.Add("@ident", SqlDbType.VarChar).Value = dato;
                oSqlCommand.Parameters.Add("@ident2",SqlDbType.Int).Value=dato1;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    PacienteBE oCliente = new PacienteBE();

                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.descEstadoCivil = (String)oSqlDataReader["descEstadoCivil"];
                    oCliente.nombreDistrito = (String)oSqlDataReader["nombreDistrito"];
                    oCliente.nombreProvincia = (String)oSqlDataReader["nombreProvincia"];
                    oCliente.nombreRegion = (String)oSqlDataReader["nombreRegion"];
                    oCliente.descZona = (String)oSqlDataReader["descZona"];
                    oCliente.nombreCompleto = (String)oSqlDataReader["NombreCompleto"];
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