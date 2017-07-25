using Common.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service.Reportes
{
    public class ProductoDA
    {
        public List<ProductoBE> ReporteProducto()
        {
            SqlConnection oSqlConnection;
            List<ProductoBE> olist = new List<ProductoBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                SqlCommand oSqlCommand = new SqlCommand("SP_PRO_ReporteProducto", oSqlConnection);
                //oSqlCommand.Parameters.Add("@num", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    ProductoBE oCliente = new ProductoBE();
                    oCliente.codProducto = (string)oSqlDataReader["CodProducto"];
                    oCliente.codBarra = (String)oSqlDataReader["codBarra"];
                    oCliente.nombreProductoComercial = (String)oSqlDataReader["nombreProductoComercial"];
                    oCliente.nombreCategoria = (String)oSqlDataReader["nombreCategoria"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }
    }
}