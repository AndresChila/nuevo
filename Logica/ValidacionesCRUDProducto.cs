using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ValidacionesCRUDProducto
    {
        public ValidacionesCRUDProducto()
        {
        }
        string mensaje = "Datos correctos. Puede ingresar";
        bool validarLlenoAgregar(string refp, string precio, string cantidad)
        {
            if (refp == "" || precio == "" || cantidad == "")
            {
                mensaje = "Por favor llene todos los campos";
                return false;
            }
            else
            {
                return true;
            }
        }
        bool validarLlenoEditar(string refp, string precio, string cantidad)
        {
            if (refp == "" || precio == "" || cantidad == "")
            {
                return false;
            }
            else
            {
                mensaje = "Por favor llene todos los campos.";
                return true;
            }
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
                mensaje = "Ingrese un dato númerico válido";
                return false;
            }
        }

        public void AgregarProducto(string refp, string precio, string cantidad, string talla)
        {
            if (validarLlenoAgregar(refp, precio, cantidad) == true)
            {
                if (validarNumeros(cantidad) == true)
                {
                    if (validarNumeros(precio) == true)
                    {
                        DAOUsuario dAO = new DAOUsuario();
                        Producto producto = new Producto();
                        Producto producto2 = new Producto();
                        producto.Referencia = refp;
                        producto.Cantidad = Convert.ToInt64(cantidad);
                        producto.Precio = Convert.ToDouble(precio);
                        producto.Talla = Convert.ToDouble(talla);
                        if (producto.Precio <= 0 || producto.Cantidad <= 0)
                        {
                            mensaje = "Ingrese un valor mayor a 0.";
                            return;
                        }
                        producto2.Referencia = refp;
                        producto2.Precio = Convert.ToDouble(precio);
                        producto2.Talla = Convert.ToDouble(talla);
                        List<string> referencias = dAO.ReferenciasProducto();
                        List<Producto> referencias2 = new List<Producto>();
                        referencias2 = dAO.pruebaaa();
                        if (referencias2.Contains(producto2))
                        {
                            mensaje = "Este producto ya esta registrado. Si desea añadir mas elementos de este producto, dirijase a la seccion de actualizar un producto.";
                        }
                        else
                        {                           
                            dAO.crearProducto(producto);
                            mensaje = "Producto ingresado exitosamente";
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }
        }

        public string devuelvemensaje()
        {
            return mensaje;
        }
        Producto producto = new Producto();
        public void RowCommand(string name, string argument, int r)
        {
            DAOUsuario dAO = new DAOUsuario();                        
            if(name.Equals("Delete"))
            {
                int id = Convert.ToInt32(argument);
                dAO.eliminarProducto(id);
                this.SetProducto(0);
            }
            if (name.Equals("Editar"))
            {
                this.SetProducto(Convert.ToInt32(argument));
            }
        }

        public void SetProducto(int r)
        {
            DAOUsuario dAO = new DAOUsuario();            
            int refe = r;
            DataTable productos = dAO.verProductosEditar(refe);
            if (productos != null && r > 0)
            {
                foreach (DataRow row in productos.Rows)
                {
                    producto.Referencia = Convert.ToString(row["referenciaproducto"]);
                    producto.Cantidad = Convert.ToInt64(row["cantidad"]);
                    producto.Talla = Convert.ToDouble(row["talla"]);
                    producto.Precio = Convert.ToDouble(row["precio"]);
                }
            }
            else
            {
                producto.Referencia = "";
                producto.Cantidad = 0;
                producto.Talla = 0;
                producto.Precio = 0;
            }              
        }

        public Producto GetProducto()
        {
            return producto;
        }

        public void EditarProducto(string refp, string precio, string cantidad, string talla, int id, string com)
        {
            if (validarLlenoAgregar(refp, precio, cantidad) == true)
            {
                if (validarNumeros(cantidad) == true)
                {
                    if (validarNumeros(precio) == true)
                    {
                        DAOUsuario dAO = new DAOUsuario();
                        Producto producto = new Producto();
                        Producto producto2 = new Producto();
                        producto.Referencia = refp;
                        producto.Cantidad = Convert.ToInt64(cantidad);
                        producto.Precio = Convert.ToDouble(precio);
                        producto.Talla = Convert.ToDouble(talla);
                        producto.Idproducto = id;
                        if (producto.Precio <= 0 || producto.Cantidad <= 0)
                        {
                            mensaje = "Ingrese un valor mayor a cero";
                            return;
                        }
                        string comp = com;
                        
                        if (Convert.ToInt32(producto.Cantidad) < Convert.ToInt32(comp))
                        {
                            mensaje = "El numero de elementos de esta referencia debe ser mayor o igual a los ya existente.";
                        }
                        else
                        {
                            dAO.editarProducto(producto);
                            mensaje = "Producto editado exitosamente.";
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }            
        }
    }
}
///////////////////arreglado
