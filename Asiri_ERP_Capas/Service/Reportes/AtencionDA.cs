using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using Common.Reportes;
using System.Configuration;
using System.IO;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Service.Reportes
{
    public class AtencionDA
    {

        //public static Image ByteArrayToImagebyMemoryStream(byte[] imageByte)
        //{
        //    MemoryStream ms = new MemoryStream(imageByte);
        //    Image image = Image.FromStream(ms);
        //    return image;
        //}

        //public Image Base64ToImage(string base64String)
        //{
        //    // Convert base 64 string to byte[]
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    // Convert byte[] to Image
        //    using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        //    {
        //        Image image = Image.FromStream(ms, true);
        //        return image;
        //    }
        //}

     

        public List<AtencionBE> ReporteAtencion(string dato)
        {
            SqlConnection oSqlConnection;
            List<AtencionBE> olist = new List<AtencionBE>();
            {
                oSqlConnection = Reportes.ConnectionManager.GetConnection();
                oSqlConnection.Open();
                //String sentencia = "";
                //SqlCommand oSqlCommand = new SqlCommand(sentencia, oSqlConnection);
                //SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                //ReportFechaBE oCliente;
                SqlCommand oSqlCommand = new SqlCommand("SP_CLT_ATENCION_PARTICULAR", oSqlConnection);
                oSqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = dato;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())

                {
                    AtencionBE oCliente = new AtencionBE();
                    oCliente.nombreCompleto = (String)oSqlDataReader["NombreCompleto"].ToString();
                    oCliente.numDocIdentidad = (String)oSqlDataReader["numDocIdentidad"];
                    oCliente.codCita = (String)oSqlDataReader["codCita"];
                    oCliente.nombreServicio = (String)oSqlDataReader["nombreServicio"].ToString();
                    //oCliente.idAtencion = (int)oSqlDataReader["idAtencion"];
                    oCliente.codAtencion = (String)oSqlDataReader["codAtencion"];
                    oCliente.fecRegistro = (DateTime)oSqlDataReader["fecRegistro"];
                    oCliente.descEvolucion = (String)oSqlDataReader["descEvolucion"];
                    oCliente.descDiagnostico = (String)oSqlDataReader["descDiagnostico"];
                    oCliente.descAnamnesis = (String)oSqlDataReader["descAnamnesis"];
                    oCliente.descEstudioCompl = (String)oSqlDataReader["descEstudioCompl"];
                    oCliente.descExamenFisico = (String)oSqlDataReader["descExamenFisico"];
                    oCliente.sistole = (String)oSqlDataReader["sistole"];
                    oCliente.diastole = (String)oSqlDataReader["diastole"];
                    oCliente.pulsacion = (String)oSqlDataReader["pulsacion"];
                    oCliente.ritmoRespiratorio = (String)oSqlDataReader["ritmoRespiratorio"];
                    oCliente.temperatura = (String)oSqlDataReader["temperatura"];
                    oCliente.altura = (String)oSqlDataReader["altura"];
                    oCliente.peso = (String)oSqlDataReader["peso"];
                    oCliente.imc = (String)oSqlDataReader["imc"];
                    //oCliente.idTratamiento= (long)oSqlDataReader["idTratamiento"];
                    oCliente.descTratamiento = (String)oSqlDataReader["descTratamiento"];
                    oCliente.indicacionServicios = (String)oSqlDataReader["indicacionServicios"];
                    oCliente.nombreProducto = (String)oSqlDataReader["nombreProducto"];
                    oCliente.descCie10 = (String)oSqlDataReader["descCie10"];

                    oCliente.pathArchivo = ((byte[])oSqlDataReader["pathArchivo"]);
                    //var a = Convert.ToBase64String(oCliente.pathArchivo);
                    //oCliente.direccion01 = (String)oSqlDataReader["codEmpleado"];
                    oCliente.codEmpleado = (string)oSqlDataReader["codEmpleado"];
                    oCliente.NombreCompleto1 = (String)oSqlDataReader["NombreCompleto1"];
                    oCliente.idEvolucion = (long)oSqlDataReader["idEvolucion"];
                    oCliente.idDiagnostico = (long)oSqlDataReader["idDiagnostico"];
                    oCliente.idEstudioCompl = (long)oSqlDataReader["idEstudioCompl"];
                    oCliente.idFuncionVital = (long)oSqlDataReader["idFuncionVital"];
                    oCliente.idExamenFisico = (long)oSqlDataReader["idExamenFisico"];
                    oCliente.idTratamientoDtl = (long)oSqlDataReader["idTratamientoDtl"];

                    oCliente.Servicios_pro = (String)oSqlDataReader["Servicios_pro"];
                    //oCliente.idEstadoCita = (int)oSqlDataReader["idEstadoCita"];
                    olist.Add(oCliente);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return olist;
            }
        }

        public Image CambiarTamanoImagen(Image pImagen, int pAncho, int pAlto)
        {
            //creamos un bitmap con el nuevo tamaño
            Bitmap vBitmap = new Bitmap(pAncho, pAlto);
            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //Se dibuja la nueva imagen
                vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
            }
            //retornamos la nueva imagen
            return (Image)vBitmap;
        }
        public byte[] ConvertiByteImagen(byte[] path)
        {
            
            using (var ms = new MemoryStream(path, 0, path.Length))
            {
                Image image = Image.FromStream(ms, true);
                image = CambiarTamanoImagen(image, 100, 100);

                var newImg = ImageToBase64(image);
                return newImg;

            }
            
        }
        public byte[] ImageToBase64(Image image)
        {
          
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();
                    var dasde = Convert.ToBase64String(imageBytes);
                    return imageBytes;
                }
            
        }
    }
}