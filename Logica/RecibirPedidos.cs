using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class RecibirPedidos
    {
        string mensaje, idAsig;
        DataTable datosAsignacion, compara, compara2, paginar, paginar2, idAsignDT;

        public RecibirPedidos(DataTable datosAsignacion, DataTable paginar, DataTable paginar2, DataTable idAsignDT, string idAsig)
        {
            this.datosAsignacion = datosAsignacion;
            this.paginar = paginar;
            this.paginar2 = paginar2;
            this.idAsignDT = idAsignDT;
            this.idAsig = idAsig;
        }

        public string actualizarAsignaciones()
        {
            DAOUsuario dAO = new DAOUsuario();
            DataTable datosAsignacion = new DataTable();
            paginar = null;
            if (datosAsignacion.Rows.Count == 0)
            {
                mensaje = "No hay productos pendientes para asignar al inventario.";
            }
            else
            {
                if (paginar == null)
                {
                    compara = new DataTable();
                    compara = datosAsignacion;
                    paginar = compara;
                }
            }
            return mensaje;
        }

        public string traerMensaje()
        {
            return mensaje;
        }

        public void seleccionarSede(string comandName, int comandArgument)
        {
            if (comandName == ("Select"))
            {
                DAOUsuario dAO = new DAOUsuario();
                paginar2 = null;
                idAsignDT = null;
                DataTable datosAsignaciones = dAO.verAsignaciones(Convert.ToInt32(comandArgument));
                if (paginar2 == null)
                {
                    compara2 = new DataTable();
                    compara2 = datosAsignaciones;
                    paginar2 = compara2;
                }
                if (idAsignDT == null)
                {
                    idAsig = Convert.ToString(comandArgument);
                    idAsignDT.Equals(idAsig);
                }

            }
        }

        public string agregarInventario()
        {
            /*
            if (FilasGV == 0)
            {
                mensaje = "No hay productos pendientes para asignar al inventario.";
            }
            else
            {
                List<Inventario> listaDevolucion;
                foreach (GridViewRow fila in GV_Asignaciones.Rows)
                {
                    Inventario inventario = new Inventario();
                    bool dev;
                    inventario.Referencia = Convert.ToString(((Label)fila.Cells[1].FindControl("L_Referencia")).Text);
                    inventario.Talla = Convert.ToDouble(((Label)fila.Cells[2].FindControl("L_Talla")).Text);
                    inventario.Cantidad = Convert.ToInt32(((Label)fila.Cells[3].FindControl("L_Cantidad")).Text);
                    dev = Convert.ToBoolean(((CheckBox)fila.Cells[4].FindControl("CB_Recibido")).Checked);
                    if (inventario.Referencia != null)
                    {
                        inventario.Sede = Convert.ToString(Session["sede"]);
                        if (dev == true)
                        {
                            da.crearInventario(inventario);
                        }
                        else if (dev == false)
                        {
                            if (Session["listaDev"] == null)
                            {
                                listaDevolucion = new List<Inventario>();
                                listaDevolucion.Add(inventario);
                                Session["listaDev"] = listaDevolucion;
                            }
                            else
                            {
                                listaDevolucion = (Session["listaDev"] as List<Inventario>);
                                listaDevolucion.Add(inventario);
                                Session["listaDev"] = listaDevolucion;
                            }
                        }
                        mensaje= "Se han añadido los productos al inventario.";
                    }
                    else
                    {
                        mensaje = "No hay productos pendientes para agregar al inventario de la sede.";
                    }

                }
            }*/
            return mensaje;
        }








    }
}
