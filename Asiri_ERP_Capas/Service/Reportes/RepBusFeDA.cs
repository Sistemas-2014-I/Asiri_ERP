using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Reportes
{
    public class RepBusFeDA
    {
        public List<RepBusFecBE> ReporteFechaPac(string Dia, string Mes, string Año)
        {
            SqlConnection oSqlConnection;
            List<RepBusFecBE> olist = new List<RepBusFecBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_CLI_CITA_BUSCARFECHACITADIA", oSqlConnection);
                oSqlCommand.Parameters.Add("@Dia1", SqlDbType.VarChar).Value = Dia;
                oSqlCommand.Parameters.Add("@Mes1", SqlDbType.VarChar).Value = Mes;
                oSqlCommand.Parameters.Add("@Año1", SqlDbType.VarChar).Value = Año;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    RepBusFecBE oCliente = new RepBusFecBE();
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
        public List<RepBusFecBE> ReporteFechaPac1(string Mes, string Año)
        {
            SqlConnection oSqlConnection;
            List<RepBusFecBE> olist1 = new List<RepBusFecBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand1 = new SqlCommand("SP_CLI_CITA_BUSCARFECHACITAMES", oSqlConnection);
                oSqlCommand1.Parameters.Add("@Mes", SqlDbType.VarChar).Value = Mes;
                oSqlCommand1.Parameters.Add("@Año", SqlDbType.VarChar).Value = Año;
                oSqlCommand1.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader1 = oSqlCommand1.ExecuteReader();
                while (oSqlDataReader1.Read())
                {
                    RepBusFecBE oCliente1 = new RepBusFecBE();
                    oCliente1.idCita = (Int64)oSqlDataReader1["idCita"];
                    oCliente1.fecCita = (DateTime)oSqlDataReader1["fecCita"];
                    oCliente1.fecRegistro = (DateTime)oSqlDataReader1["fecRegistro"];
                    oCliente1.horaInicio = (String)oSqlDataReader1["horaInicio"];
                    oCliente1.mtoTotal = (Decimal)oSqlDataReader1["mtoTotal"];
                    oCliente1.descEstadoCita = (String)oSqlDataReader1["descEstadoCita"];
                    oCliente1.Paciente = (String)oSqlDataReader1["Paciente"];
                    olist1.Add(oCliente1);
                }
                oSqlDataReader1.Close();
                oSqlConnection.Close();
                return olist1;
            }
        }
        public List<RepBusFecBE> ReporteFechaPac2(string Año)
        {
            SqlConnection oSqlConnection;
            List<RepBusFecBE> olist2 = new List<RepBusFecBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand1 = new SqlCommand("SP_CLI_CITA_BUSCARFECHACITAAÑO", oSqlConnection);
                oSqlCommand1.Parameters.Add("@Año2", SqlDbType.VarChar).Value = Año;
                oSqlCommand1.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader1 = oSqlCommand1.ExecuteReader();
                while (oSqlDataReader1.Read())
                {
                    RepBusFecBE oCliente1 = new RepBusFecBE();
                    oCliente1.idCita = (Int64)oSqlDataReader1["idCita"];
                    oCliente1.fecCita = (DateTime)oSqlDataReader1["fecCita"];
                    oCliente1.fecRegistro = (DateTime)oSqlDataReader1["fecRegistro"];
                    oCliente1.horaInicio = (String)oSqlDataReader1["horaInicio"];
                    oCliente1.mtoTotal = (Decimal)oSqlDataReader1["mtoTotal"];
                    oCliente1.descEstadoCita = (String)oSqlDataReader1["descEstadoCita"];
                    oCliente1.Paciente = (String)oSqlDataReader1["Paciente"];
                    olist2.Add(oCliente1);
                }
                oSqlDataReader1.Close();
                oSqlConnection.Close();
                return olist2;
            }
        }
    }
}