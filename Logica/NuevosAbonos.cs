using Datos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace Logica
{
    public class NuevosAbonos
    {
        string mensaje, precioDeuda, pagoActual, idAbono;
        DataTable vntaS, datosAbono;
        List<Producto> refresh;
        public NuevosAbonos(DataTable vntaS, string precioDeuda, string pagoActual, string idAbono)
        {
            this.vntaS = vntaS;
            this.precioDeuda = precioDeuda;
            this.pagoActual = pagoActual;
            this.idAbono = idAbono;
        }

        public string nuevaVenta()
        {
            if (vntaS != null)
            {
                Producto individual = new Producto();
                datosAbono = new DataTable();
                datosAbono = (vntaS as DataTable);

                foreach (DataRow row in datosAbono.Rows)
                {
                    Venta venta = new Venta();
                    venta.Idcliente = Convert.ToInt32(row["idcliente"]);
                    venta.Producto = JsonConvert.DeserializeObject<List<Producto>>(Convert.ToString(row["descripcion"]));
                    refresh = venta.Producto as List<Producto>;
                    venta.Idvendedor = Convert.ToInt32(row["idvendedor"]);
                    venta.Fecha = row["fecha"].ToString();
                    venta.Precio = Convert.ToDouble(row["precio"]);
                    venta.Sede = Convert.ToString(row["sede"]);
                    DAOUsuario dAO = new DAOUsuario();
                    dAO.crearVenta(venta, JsonConvert.SerializeObject(venta.Producto));
                    mensaje = "Se ha convertido en venta";
                    //this.actualizarInventario();
                }

                
            }
            return mensaje;
        }

        public string traerMensaje()
        {
            return mensaje;
        }

        public string agregarProducto()
        {
            if (validarLlenoAbono() == true)
            {
                double a, b;
                a = Convert.ToDouble(precioDeuda);
                b = Convert.ToDouble(pagoActual);
                if (idAbono == null)
                {
                    mensaje = "Seleccione un abono para actualzar.";
                }
                else
                {
                    if (a < b)
                    {
                        mensaje = "El precio a pagar supera la deuda actual.";
                    }
                    else if (b <= a)
                    {
                        DAOUsuario dAO = new DAOUsuario();
                        dAO.actualizarAbono(Convert.ToInt32(idAbono), a - b);
                        mensaje = "Abono actualizado. Nueva deuda" + (a - b);
                        //this.llenarGV_Abonos();
                        if ((a - b) == 0)
                        {
                            this.nuevaVenta();
                        }
                        pagoActual=null;
                        precioDeuda=null;
                        idAbono = null;
                    }
                }
            }
            else
            {
                mensaje = "Ingrese datos.";
            }
            return mensaje;
        }

        public void seleccionarAbono(string comandName, int comandArgument)
        {
            if (comandName.Equals("Select"))
            {
                DAOUsuario dAO = new DAOUsuario();
                precioDeuda = Convert.ToString(dAO.traePrecioAbono(Convert.ToInt32(comandArgument)));
               idAbono = Convert.ToString(comandArgument);
            }
        }

        bool validarLlenoAbono()
        {
            if (precioDeuda == "" || pagoActual == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
