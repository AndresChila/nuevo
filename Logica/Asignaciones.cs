using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;

namespace Logica
{
    public class Asignaciones
    {
        public Asignaciones()
        {

        }

        string mensaje;

        public string CantidadPendiente()
        {
            DAOUsuario dAO = new DAOUsuario();
            DataTable cantidadPendiente = new DataTable();
            cantidadPendiente = dAO.verPedido();
            return cantidadPendiente.Rows.Count.ToString();
        }

        public void AgregarProductos(string refp, double talla, string cant, string sede)
        {
            DAOUsuario d = new DAOUsuario();
            Asignacion asignacion = new Asignacion();
            Producto producto = new Producto();
            Pedido pedido = new Pedido();
            int cont = 0;
            int cantBodega = 0;
            if (validarNumeros(cant) == true)
            {
                if (cant == "")
                {
                    asignacion.Cantidad = 0;
                }
                else
                {
                    asignacion.Cantidad = Convert.ToInt32(cant);
                }
                if (asignacion.Cantidad > 0)
                {
                    cont++;
                    DataTable r = d.validarAsignacion(asignacion.Referencia, asignacion.Talla);
                    if (r.Rows.Count == 1)
                    {
                        foreach (DataRow row in r.Rows)
                        {
                            cantBodega = Convert.ToInt32(row["cantidad"]);
                            producto.Entregado = Convert.ToInt32(row["entregado"]);
                            producto.Idproducto = Convert.ToInt32(row["idproducto"]);
                            cantBodega = cantBodega - producto.Entregado;
                        }
                        if (asignacion.Cantidad < cantBodega)
                        {
                            //Response.Write("esto da" + (cantBodega - asignacion.Cantidad));
                            if ((cantBodega - asignacion.Cantidad) >= 5)
                            {
                                DateTime fechaHoy = DateTime.Now;
                                asignacion.Fecha = fechaHoy.ToString("d");
                                asignacion.Estado = false;
                                asignacion.Sede = sede;
                                if (cont == 1)
                                {
                                    d.crearAsignacion(asignacion);
                                }

                                DataTable id = new DataTable();
                                id = d.verUltimoId2();
                                if (id.Rows.Count > 0)
                                {
                                    foreach (DataRow ff in id.Rows)
                                    {
                                        pedido.Idpedido = Convert.ToInt32(ff["f_verultimoid2"]);
                                    }
                                    d.crearAsignaciones(asignacion, pedido.Idpedido);
                                    d.editarCantidad(producto.Idproducto, (asignacion.Cantidad + producto.Entregado));                                                                                                         
                                    mensaje = "Asignación completada";
                                }
                            }
                            else
                            {
                                mensaje = "En la sede principal deben quedar al menos 5 productos";
                                return;
                            }
                        }
                        else
                        {
                            mensaje = "La cantidad de productos a asignar debe ser menor a la que esta e bodega";
                            return;
                        }
                    }
                    else
                    {
                       mensaje = "No hay productos con esta descripción en la bodega validar.";
                       return;
                    }
                }
            }
            else
            {
                mensaje = "Solo se pueden ingresar numeros.";
                return;
            }
        }

        string sede;

        public DataTable Row_Command(string name, string argument)
        {
            DataTable detalle = new DataTable();
            if (name.Equals("Select"))
            {
                DAOUsuario dAO = new DAOUsuario();                
                DataTable ped = new DataTable();
                ped = dAO.verPedido(Convert.ToInt32(argument));

                detalle = dAO.verPedidos(Convert.ToInt32(argument));
                sede = Convert.ToString(ped.Rows[0]["sede"]);
                return detalle;
            }
            else
            {
                return detalle;
            }

        }

        public void Asignar(string refp, double talla, int cantidad, int idPed, int count)
        {
            /*DAOUsuario d = new DAOUsuario();
            Producto producto = new Producto();
            Pedido pedido = new Pedido();
            
            int cantBodega = 0;
            int idPedi = idPed;
            int cont = 0;
            if (count > 0)
            {
                foreach (GridViewRow row in GV_Pedidos.Rows)
                {
                    Asignacion asignacion = new Asignacion();
                    cont++;
                    asignacion.Referencia = refp;
                    asignacion.Talla = talla;
                    asignacion.Cantidad = cantidad;

                    DataTable r = d.validarAsignacion(asignacion.Referencia, asignacion.Talla);

                    if (r.Rows.Count == 1)
                    {
                        foreach (DataRow ro in r.Rows)
                        {
                            cantBodega = Convert.ToInt32(ro["cantidad"]);
                            producto.Entregado = Convert.ToInt32(ro["entregado"]);
                            Session["entregado"] = producto.Entregado;
                            producto.Idproducto = Convert.ToInt32(ro["idproducto"]);
                            Session["idproducto"] = producto.Idproducto;
                            cantBodega = cantBodega - producto.Entregado;
                        }
                        if (asignacion.Cantidad < cantBodega)
                        {
                            Response.Write("esto da" + (cantBodega - asignacion.Cantidad));
                            if ((cantBodega - asignacion.Cantidad) >= 5)
                            {
                                DateTime fechaHoy = DateTime.Now;
                                asignacion.Fecha = fechaHoy.ToString("d");
                                asignacion.Estado = false;
                                asignacion.Sede = Convert.ToString(Session["sedePedido"]);

                                if (Session["asignacion2"] == null)
                                {
                                    listaAsignacion2 = new List<Asignacion>();
                                    listaAsignacion2.Add(asignacion);
                                    Session["asignacion2"] = listaAsignacion2;
                                }
                                else
                                {
                                    listaAsignacion2 = (Session["asignacion2"] as List<Asignacion>);
                                    listaAsignacion2.Add(asignacion);
                                    Session["asignacion2"] = listaAsignacion2;
                                }
                            }
                            else
                            {
#pragma warning disable CS0618 // Type or member is obsolete

                                RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('En la sede principal deben quedar al menos 5 productos. Revise el producto Referencia:" + asignacion.Referencia + " y talla " + asignacion.Talla + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
                                return;
                            }
                        }
                        else
                        {
#pragma warning disable CS0618 // Type or member is obsolete
                            RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('La cantidad de productos a asignar debe ser menor a la que esta en bodega. ');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
                            return;
                        }
                    }

                    else
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('No hay productos con esta descripción en la bodega validar. ');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
                        return;
                    }
                }
#pragma warning disable CS0618 // Type or member is obsolete
                RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('El pedido esta listo para ser enviado.');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
            }
            if (Session["asignacion2"].Equals(null) == false)
            {
                Button2.Enabled = true;
                Button3.Enabled = false;
            }

            GV_Pedido.DataBind();
            GV_Pedidos.DataBind();
            GV_ProductosBodega.DataBind();*/
        }

        public string Sede()
        {
            return sede;
        }

        public string Devuelve_Mensaje()
        {
            return mensaje;
        }

        public bool validarNumeros(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
