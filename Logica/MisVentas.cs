using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Logica
{
    public class MisVentas
    {
        Venta venta = new Venta();
        string mensaje, filtrar;
        string fecha1, fecha2;
        DataTable sdata, suser_id;
        bool estado, estado2 = true;
        DataTable data = new DataTable();

        public MisVentas()
        {
        }

        public MisVentas(string filtrar, string fecha1, string fecha2, DataTable sdata, DataTable suser_id)
        {
            this.filtrar = filtrar;
            this.fecha1 = fecha1;
            this.fecha2 = fecha2;
            this.sdata = sdata;
            this.suser_id = suser_id;
        }

        public void filtrarVentas()
        {
            if (filtrar == "1")
            {
                estado = false;
                DAOUsuario dAO = new DAOUsuario();
                data = dAO.verVentas(Convert.ToInt32(suser_id), 1, "", "");         


            }
            if (filtrar == "2")
            {
                estado = true;
                estado2 = false;
            }
            if (filtrar == "3")
            {
                estado = false;
                DAOUsuario dAO = new DAOUsuario();
                data = dAO.verVentas(Convert.ToInt32(suser_id), 3, "", "");                
            }
        }

        public void buscarVentas()
        {
            if (validarLlenadoFechas() == true)
            {
                DAOUsuario dAO = new DAOUsuario();                
                data = dAO.verVentas(Convert.ToInt32(suser_id), 2, fecha1, fecha2);                
                estado2 = true;
                estado = true;
            }
            else
            {
                mensaje = "Ingrese datos.'";
            }
        } 

        public bool Get_Estado()
        {
            return estado;
        }

        public bool Get_Estado2()
        {
            return estado2;
        }

        public DataTable Get_GV_Ventas()
        {
            return data;
        }
        
        public string traerMensaje()
        {
            return mensaje;
        }

        bool validarLlenadoFechas()
        {
            if (fecha1 == "" || fecha2 == "")
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
