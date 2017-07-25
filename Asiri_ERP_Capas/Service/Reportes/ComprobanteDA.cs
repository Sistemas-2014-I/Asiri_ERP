using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Reportes
{
    public class ComprobanteDA
    {
        public List<ComprobanteBE> ReporteComprobante(string dato)
        {
            SqlConnection oSqlConnection;
            List<ComprobanteBE> olist = new List<ComprobanteBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_CNS_COMPROBANTE ", oSqlConnection);
                oSqlCommand.Parameters.Add("@idComprobanteEmitido", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    ComprobanteBE oCliente = new ComprobanteBE();

                    //oCliente.idComprobanteEmitido = (int)oSqlDataReader["idComprobanteEmitido"];
                    oCliente.numComp = (String)oSqlDataReader["numComp"].ToString();
                    oCliente.descTipoComprobante = (String)oSqlDataReader["descTipoComprobante"].ToString();
                    oCliente.fecEmision = (DateTime)oSqlDataReader["fecEmision"];
                    //oCliente.idAtencion = (int)oSqlDataReader["idAtencion"];
                    oCliente.fecVencimiento = (DateTime)oSqlDataReader["fecVencimiento"];
                    oCliente.fecAnulacion = (DateTime)oSqlDataReader["fecAnulacion"];
                    oCliente.fecCancelacion = (DateTime)oSqlDataReader["fecCancelacion"];
                    oCliente.obsvComprobanteEmitido = (String)oSqlDataReader["obsvComprobanteEmitido"];
                    oCliente.descSucursal = (String)oSqlDataReader["descSucursal"];
                    oCliente.direccionSucursal = (String)oSqlDataReader["direccionSucursal"];
                    oCliente.telefono01Sucural = (String)oSqlDataReader["telefono01Sucural"];
                    //oCliente.idFuncionVital = ((int)oSqlDataReader["idFuncionVital"]);
                    oCliente.telefono02Sucural = (String)oSqlDataReader["telefono02Sucural"];
                    oCliente.nombrePaciente = (String)oSqlDataReader["nombrePaciente"];
                    oCliente.razonSocial = (String)oSqlDataReader["razonSocial"];
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.direccion01Paciente = (String)oSqlDataReader["direccion01Paciente"];
                    oCliente.direccion02Paciente = (String)oSqlDataReader["direccion02Paciente"];
                    oCliente.docIdentidad = (String)oSqlDataReader["docIdentidad"];
                    oCliente.mtoSubtotal = (decimal)oSqlDataReader["mtoSubtotal"];
                    //oCliente.idTratamiento= (int)oSqlDataReader["idTratamiento"];
                    oCliente.mtoImpto = (decimal)oSqlDataReader["mtoImpto"];
                    oCliente.mtoDescuento = (decimal)oSqlDataReader["mtoDescuento"];
                    oCliente.mtoTotal = (decimal)oSqlDataReader["mtoTotal"];
                    oCliente.porcImpto = (decimal)oSqlDataReader["porcImpto"];
                    //oCliente.pathArchivo = Encoding.UTF8.GetString((byte[])oSqlDataReader["pathArchivo"]).ToString(); 
                    //oCliente.direccion01 = (String)oSqlDataReader["codEmpleado"];
                    oCliente.porcDescuento = (decimal)oSqlDataReader["porcDescuento"];
                    oCliente.abrvImpto = (String)oSqlDataReader["abrvImpto"];
                    oCliente.nombreServicio = (String)oSqlDataReader["nombreServicio"];
                    oCliente.precio = (decimal)oSqlDataReader["precio"];
                    oCliente.cantidad = (decimal)oSqlDataReader["cantidad"];
                    oCliente.importe = (decimal)oSqlDataReader["importe"];
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
