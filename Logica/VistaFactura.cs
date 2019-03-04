using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DataSet;

namespace Logica
{
    public class VistaFactura
    {
        DataTable idVentaS;
        String mensaje;

        public VistaFactura(DataTable idVentaS)
        {
            this.idVentaS = idVentaS;
        }


        public String pageLoad()
        {
            DAOUsuario dAO = new DAOUsuario();
            DataTable idd = new DataTable();
            idd = dAO.verUltimoId3();
            if (idd.Rows.Count > 0)
            {
                foreach (DataRow row in idd.Rows)
                {
                    if (idVentaS == null)
                    {
                        idVentaS = row["f_verultimoid3"] as DataTable;
                    }
                }
            }
            else
            {
                mensaje = "No hay ventas registradas.";
            }
            
            return mensaje;
        }

        public string devuelveMensaje()
        {
            return mensaje;
        }

        public string obtenerInforme()
        {
            DataRow fila;
            DataTable facturaInformacion = new DataTable();
            DS_Factura datos = new DS_Factura();

            facturaInformacion = datos.Tables["Venta"];
            DAOUsuario dao = new DAOUsuario();
            if (idVentaS == null)
            {
                mensaje = "No hay id en la variable de sesion.";
            }
            else
            {
                DataTable intermedio = dao.verFactura(Convert.ToInt32(idVentaS));
                DataTable data = dao.verDescripcionVenta(Convert.ToInt32(idVentaS));

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    fila = facturaInformacion.NewRow();
                    if (i == 0)
                    {
                        fila["nombre_c"] = intermedio.Rows[i]["nombre_c"].ToString();
                        fila["apellido_c"] = intermedio.Rows[i]["apellido_c"].ToString();
                        fila["id_cliente"] = Convert.ToInt32(intermedio.Rows[i]["id_cliente"].ToString());
                        fila["nombre_v"] = intermedio.Rows[i]["nombre_v"].ToString();
                        fila["fecha"] = DateTime.Parse(intermedio.Rows[i]["fecha"].ToString());
                        fila["valor_total"] = Convert.ToDouble(intermedio.Rows[i]["precio"].ToString());
                    }
                    fila["referencia"] = data.Rows[i]["ReferenciaProducto"].ToString();
                    fila["talla"] = Convert.ToDouble(data.Rows[i]["Talla"].ToString());
                    fila["cantidad"] = Convert.ToInt32(data.Rows[i]["Cantidad"].ToString());
                    fila["valor_uni"] = Convert.ToDouble(data.Rows[i]["Precio"].ToString());
                    fila["valor_uni_uni"] = Convert.ToDouble(data.Rows[i]["ValorTotal"].ToString());

                    facturaInformacion.Rows.Add(fila);
                }
            }
            return mensaje;
        }

    }
}
