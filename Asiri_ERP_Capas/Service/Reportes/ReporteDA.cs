using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common.Reportes;
namespace Service.Reportes
{
    public class ReporteDA
    {
        // Reporte por estado de cuenta de cliente en particular
        public List<ReporteBE> ReporteEstCuenta(string dato)
        {
            SqlConnection oSqlConnection;
            List<ReporteBE> olist = new List<ReporteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_ESTADOCUENTA_CLIENTE", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    ReporteBE oCliente = new ReporteBE();
                    oCliente.nombreCompleto = (String)oSqlDataReader["nombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    oCliente.fecCita = (DateTime)oSqlDataReader["fecCita"];
                    oCliente.mtoSubtotal = (Decimal)oSqlDataReader["mtoSubtotal"];
                    oCliente.debe = (Decimal)oSqlDataReader["debe"];
                    oCliente.haber = (Decimal)oSqlDataReader["haber"];
                    oCliente.saldo = (Decimal)oSqlDataReader["saldo"];
                    //oCliente.idEstadoCita = (int)oSqlDataReader["idEstadoCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }

        //Estado Cuenta en Particular
        public List<ReporteBE> ReporteEstCuePar(int dato)
        {
            SqlConnection oSqlConnection;
            List<ReporteBE> olist = new List<ReporteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_TNS_ESTADODECUENTA_PARTICULAR", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    ReporteBE oCliente = new ReporteBE();
                    oCliente.nombreCompleto = (String)oSqlDataReader["nombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    //oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    //oCliente.fecCita = (DateTime)oSqlDataReader["fecCita"];
                    oCliente.nombreServicio = (String)oSqlDataReader["nombreServicio"];
                    oCliente.precio = (Decimal)oSqlDataReader["precio"];
                    oCliente.mtoSubtotal = (Decimal)oSqlDataReader["mtoSubtotal"];
                    //oCliente.idCuota = (Int32)oSqlDataReader["idCuota"];
                    oCliente.debe = (Decimal)oSqlDataReader["debe"];
                    oCliente.haber = (Decimal)oSqlDataReader["haber"];
                    oCliente.saldo = (Decimal)oSqlDataReader["saldo"];
                    oCliente.mtoDesembolso = (Decimal)oSqlDataReader["mtoInteres"];
                    oCliente.mtoPago = (Decimal)oSqlDataReader["mtoPago"];
                    oCliente.fecDesembolso = (DateTime)oSqlDataReader["fecDesembolso"];
                    oCliente.fecPago = (DateTime)oSqlDataReader["fecPago"];
                    //oCliente.idEstadoCita = (int)oSqlDataReader["idEstadoCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }


        //Comprobante en particular

        public List<ReporteBE> ReporteCompPar(string dato)
        {
            SqlConnection oSqlConnection;
            List<ReporteBE> olist = new List<ReporteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "SELECT * FROM VW_RHUt_PACIENTE_REPFECHAS";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_TNS_comprobanteEmitido_particular ", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    ReporteBE oCliente = new ReporteBE();
                    oCliente.nombreCompleto = (String)oSqlDataReader["nombreCompleto"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    //oCliente.codPaciente = (String)oSqlDataReader["codPaciente"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    //oCliente.direccion01 = (String)oSqlDataReader["direccion01"];
                    //oCliente.fecCita = (DateTime)oSqlDataReader["fecCita"];
                    oCliente.nombreServicio = (String)oSqlDataReader["nombreServicio"];
                    oCliente.descTipoComprobante = (String)oSqlDataReader["descTipoComprobante"];
                    oCliente.precio = (Decimal)oSqlDataReader["precio"];
                    oCliente.cantidad = (Decimal)oSqlDataReader["cantidad"];
                    oCliente.serie = (String)oSqlDataReader["serie"];
                    oCliente.numComprobanteEmitido = (String)oSqlDataReader["numComprobanteEmitido"];
                    oCliente.mtoTotal = (Decimal)oSqlDataReader["mtoTotal"];
                    oCliente.mtoSubtotal = (Decimal)oSqlDataReader["mtoSubTotal"];
                    //oCliente.idCuota = (Int32)oSqlDataReader["idCuota"];
                    oCliente.mtoImpto = (Decimal)oSqlDataReader["mtoImpto"];
                    oCliente.mtoDescuento = (Decimal)oSqlDataReader["mtoDescuento"];
                    oCliente.porcentajeImpto = (Decimal)oSqlDataReader["porcentajeImpto"];
                    oCliente.porcentajeDescuento = (Decimal)oSqlDataReader["porcentajeDescuento"];
                    //oCliente.fecEmision = (DateTime)oSqlDataReader["feEmision"];
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
