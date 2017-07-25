using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common.Reportes;

namespace Service.Reportes
{
    public class RepBuscDocDA
    {
        public List<RepBuscDocBE> ReporteBusquedaDoctor(string dato)
        {
            SqlConnection oSqlConnection;
            List<RepBuscDocBE> olist = new List<RepBuscDocBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_RHU_EMPLEADO_BUSCARDOCTOR", oSqlConnection);
                oSqlCommand.Parameters.Add("@num", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepBuscDocBE oCliente = new RepBuscDocBE();
                    oCliente.idCita = (Int64)oSqlDataReader["idCita"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    oCliente.Doctor = (String)oSqlDataReader["Doctor"];
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