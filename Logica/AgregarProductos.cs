﻿using Datos;
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
    public class AgregarProductos
    {
        double precioTotal;
        public AgregarProductos()
        {

        }
        

        public List<Producto> AnalizarGridView(string cantidad, double talla, string refPro, string sede, List<Producto> listaVenta)
        {
            
            DAOUsuario dAO = new DAOUsuario();
            int cont = 0;
            Validaciones val = new Validaciones();
            if (val.validarNumeros(cantidad.ToString()) == true)
            {
                Producto producto = new Producto();
                if (cantidad == "")
                {
                    producto.Cantidad = 0;
                }
                else
                {
                    producto.Cantidad = Convert.ToInt64(cantidad);
                }
                producto.Referencia = Convert.ToString(refPro);
                producto.Talla = Convert.ToDouble(talla);

                if (producto.Cantidad > 0)
                {
                    bool vof;
                    cont++;
                    vof = dAO.validarCantidad(producto, sede);
                    if (vof == false)
                    {
                        this.set_mensaje("La cantidad de productos para la referencia " + producto.Referencia + " con talla " + producto.Talla + " es inferior a la de la venta.Escriba otra cantidad");
                    }
                    else
                    {
                        producto.Precio = dAO.traePrecio(producto.Referencia, producto.Talla);
                        producto.ValorTotal = producto.Precio * producto.Cantidad;
                        producto.Idproducto = cont;
                        if (listaVenta == null)
                        {
                            listaVenta = new List<Producto>();
                            listaVenta.Add(producto);
                        }
                        else
                        {
                            if (listaVenta.Contains(producto))
                            {
                                this.set_mensaje("Ya ha agregado este poducto a la venta. Elimine el producto de la venta para añadir mas cantidad.");
                            }
                            else
                            {
                                listaVenta.Add(producto);
                            }
                        }
                    }
                    
                }
                else
                {
                    if (listaVenta == null)
                    {
                        listaVenta = new List<Producto>();
                        this.set_mensaje("No hay productos para añadir a la venta.");
                    }
                    else
                    {   
                        return listaVenta;
                    }

                }
            }
            else
            {
                this.set_mensaje("Ingrese solo numeros.");
            }
            if(cont == 0)
            {
                this.set_mensaje("No hay productos para añadir a la venta.");
            }
            
            return listaVenta;
}


        string msj ;
        public void set_mensaje(string a)
        {
            msj = a;
        }

        public string get_mensaje()
        {
            return msj;
        }

        public string valorVenta(string val)
        {
            if(val != null)
            {
                return val;
            }
            return "No hay productos alv tururuturu";
        }
        public void actualizarVenta(Venta venta)
        {
            DAOUsuario dAO = new DAOUsuario();
            dAO.crearVenta(venta, JsonConvert.SerializeObject(venta.Producto));
        }
        public void actualizarInvent(string sede, Producto p)
        {
            DAOUsuario dAO = new DAOUsuario();

            dAO.actualizarInventario(p, Convert.ToString(sede));           

        }

        public string crearAbono(int idCliente, Abono venta)
        {
            DAOUsuario daop = new DAOUsuario();
            if (daop.validarAbono(idCliente) == 1)
            {
                return "Este cliente ya tiene un abono pendiente.Termine de pagarlo antes de registar uno nuevo.";
            }
            else
            {
                DAOUsuario dAO = new DAOUsuario();

                dAO.crearAbono(venta, JsonConvert.SerializeObject(venta.Producto));
                return "Se ha creado al abono exitosamente";
            }
        }

        public bool botonBuscar(string referencia_, string talla_)
        {
            try
            {
                string referencia = Convert.ToString(referencia_);
                double talla = Convert.ToDouble(talla_);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public List<Producto> actualizar(string a , string b, string c, bool d)
        {
            List<Producto> listaVenta = new List<Producto>();
            if (botonBuscar(a, b) == false)
            {

            }
            else
            {
                Producto compara = new Producto();
                compara.Referencia = a;
                compara.Talla = Convert.ToDouble(b);
                LlenarGridViews llenara = new LlenarGridViews();
                DataTable aja = llenara.llenarGridViewVenta(c, d);                
                foreach (DataRow row in aja.Rows)
                {
                    Producto p = new Producto();
                    p.Referencia = Convert.ToString(row["referencia"]);
                    p.Talla = Convert.ToDouble(row["talla"]);
                    listaVenta.Add(p);
                }
                if (listaVenta.Contains(compara))
                {
                    listaVenta.Clear();
                    listaVenta.Add(compara);
                }
                else
                {

                }
            }
            return listaVenta;
        }

        public void BorrarDelGrid()
        {

        }

        public List<Producto> BorrarDelGrid(string comandoNombre, List<Producto> lista, string comandoArgument)
        {

            if (comandoNombre.Equals("Delete"))
            {
                List<Producto> pventa = new List<Producto>();
                pventa = lista;
                if (pventa.Count.Equals(0) != true)
                {
                    foreach (Producto p in pventa)
                    {
                        if (p.Idproducto == Convert.ToInt32(comandoArgument))
                        {
                            pventa.RemoveAt(Convert.ToInt32(comandoArgument) - 1);
                            lista = pventa;
                            return lista;
                        }
                        else
                        {

                            this.set_mensaje("no se what happen :(");
                        }
                    }
                }

            }
            return lista;
        }
        /*public Producto traerSeleccionado(string comando, string refer, string tallita)
        {
            DAOUsuario consul = new DAOUsuario();
            DataTable pro = new DataTable();
            Producto p = new Producto();
            if(comando == "Select")
            {
                pro = consul.traerPrecio(refer, double.Parse(tallita));
                for(int i =0; i< pro.Rows.Count; i++)
                {
                    
                }
            }
            return p;
        }*/

        public double sumarTotal(List<Producto> listaV)
        {
            foreach (Producto p in listaV)
            {
                precioTotal = precioTotal + p.ValorTotal;
            }
            return precioTotal;
        }

        public void paraInvent(List<Producto> prod, string sede)
        {
            List<Producto> refresh = new List<Producto>();
            refresh = (prod as List<Producto>);
            foreach (Producto p in refresh)
            {
                actualizarInvent(sede.ToString(), p);
            }
        }
    }
}