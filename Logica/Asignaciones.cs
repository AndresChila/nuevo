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
        List<Asignacion> aux = new List<Asignacion>();
        int entregado, idProducto;
        bool estado;
        public void Asignar(string refp, double talla, int cantidad, int count, List<Asignacion> lista, string sede)
        {
            DAOUsuario d = new DAOUsuario();
            Producto producto = new Producto();
            Pedido pedido = new Pedido();
            int cantBodega = 0;

            int cont = 0;
            if (count > 0)
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
                        entregado = producto.Entregado;
                        producto.Idproducto = Convert.ToInt32(ro["idproducto"]);
                        idProducto = producto.Idproducto;
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
                            asignacion.Sede = Convert.ToString(sede);

                            if (lista == null)
                            {
                                lista = new List<Asignacion>();
                                lista.Add(asignacion);
                                aux = lista;
                            }
                            else
                            {
                                lista.Add(asignacion);
                                aux = lista;
                            }
                        }
                        else
                        {
                            mensaje = "En la sede principal deben quedar al menos 5 productos. Revise el producto Referencia:" + asignacion.Referencia + " y talla " + asignacion.Talla + ".";
                            return;
                        }
                    }
                    else
                    {
                        mensaje = "La cantidad de productos a asignar debe ser menor a la que esta en bodega.";
                        return;
                    }
                }
                else
                {
                    mensaje = "No hay productos con esta descripción en la bodega validar";
                    return;
                }
            }
            else
            {
                mensaje = "El pedido esta listo para ser enviado.";
                return;
            }
            if (lista.Equals(null) == false)
            {
                estado = true;
            }
        }
        int idpedido;
        public void ingresarBD(List<Asignacion> lista, int idpro, int entr)
        {
            DAOUsuario d = new DAOUsuario();
            List<Asignacion> listaAsignacion2 = new List<Asignacion>();
            listaAsignacion2 = lista;
            idProducto = idpro;
            entregado = entr;
            int cont = 0;
            if (listaAsignacion2.Count > 0)
            {
                foreach (Asignacion a in listaAsignacion2)
                {
                    cont++;
                    if (cont == 1)
                    {
                        d.crearAsignacion(a);
                    }
                    DataTable id = new DataTable();
                    id = d.verUltimoId2();
                    if (id.Rows.Count > 0)
                    {
                        foreach (DataRow ff in id.Rows)
                        {
                            idpedido = Convert.ToInt32(ff["f_verultimoid2"]);
                        }
                        d.crearAsignaciones(a, Convert.ToInt32(idpedido));
                        d.editarCantidad(Convert.ToInt32(idProducto), (a.Cantidad + Convert.ToInt32(entregado)));
                        d.actualizarPedido(true, Convert.ToInt32(idpedido));
                        mensaje = "Base de Datos actualizada. Asignación completada.";
                        return;
                    }
                }
            }
            else
            {
                mensaje = "No hay una lista llena para enviar. ";
                return;
            }
        }

        public bool GeT_Estado()
        {
            return estado;
        }

        public List<Asignacion> GetPedidos()
        {
            return aux;
        }

        public int GetEntregado()
        {
            return entregado;
        }

        public int GetId()
        {
            return idProducto;
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