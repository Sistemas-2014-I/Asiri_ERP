using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Reportes
{
    public class RepBuscPacDA
    {
        public List<RepBuscPacBE> ReporteBusquedaPaciente(string dato)
        {
            SqlConnection oSqlConnection;
            List<RepBuscPacBE> olist = new List<RepBuscPacBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_PACIENTE_BUSCARCITAPACIENTE", oSqlConnection);
                oSqlCommand.Parameters.Add("@num1", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepBuscPacBE oCliente = new RepBuscPacBE();
                    oCliente.idCita = (Int64)oSqlDataReader["idCita"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    oCliente.Paciente = (String)oSqlDataReader["Paciente"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}