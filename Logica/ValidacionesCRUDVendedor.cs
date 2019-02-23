using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logica
{
    public class ValidacionesCRUDVendedor
    {
        DAOUsuario dao = new DAOUsuario();
        Usuario usuario = new Usuario();
        DataTable usu = new DataTable();
        DataTable sedess = new DataTable();
        string cedula, nombre, direccion, telefono, correo, clave, sexo, sede, rol;
        string cedula0, nombre0, direccion0, telefono0, correo0, clave0, sexo0, sede0, rol0;
        string mensaje ="todo ok";
        public ValidacionesCRUDVendedor(string cedula, string nombre, string clave, string direccion, string telefono, string correo,
                                        string sexo, string sede, string rol, string cedula0, string nombre0, string clave0, string direccion0, 
                                        string telefono0, string correo0, string sexo0, string sede0, string rol0)
        {
            //Constructor
            this.cedula = cedula;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correo = correo;
            this.sexo = sexo;
            this.sede = sede;
            this.rol = rol;
            this.clave = clave;
            this.cedula0 = cedula0;
            this.nombre0 = nombre0;
            this.direccion0 = direccion0;
            this.telefono0 = telefono0;
            this.correo0 = correo0;
            this.sexo0 = sexo0;
            this.sede0 = sede0;
            this.rol0 = rol0;
            this.clave0 = clave0;
        }
        bool validarLlenoAgregar()
        {
            if (cedula == "" || nombre == "" || clave == "" || direccion == "" || telefono == "" || correo == "")
            {
                return false;
            }
            else
            {
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
                return false;
            }
        }
        public bool validarCedula()
        {
            DataTable cedulatabla = new DataTable();

            cedulatabla = dao.traerUsuarios();
            for (int i = 0; i < cedulatabla.Rows.Count; i++)
            {
                if (cedulatabla.Rows[i]["cedula"].ToString() == cedula)
                {
                    return false;
                }
            }
            return true;
        }
        public string hacerTodoAgregar()
        {
            bool resultadoNombre = Regex.IsMatch(nombre, @"^[a-zA-Z]+$");
            if (validarIngresadoAgregar())
            {
                if (validarLlenoAgregar() == true)
                {
                    if (resultadoNombre == true)
                    {
                        if (validarNumeros(telefono) == true)
                        {
                            if (validarNumeros(cedula) == true)
                            {
                                if (validarCedula() == true)
                                {

                                    usuario.Cedula = int.Parse(cedula);
                                    if (usuario.Cedula <= 0)
                                    {
                                        mensaje = "Ingrese los datos de la cédula correctamente.";
                                        return mensaje;
                                    }
                                    usuario.Nombre = nombre;
                                    usuario.Clave = clave;
                                    usuario.Direccion = direccion;
                                    usuario.Telefono = Convert.ToInt64(telefono);
                                    if (usuario.Telefono <= 0)
                                    {
                                        mensaje = "Ingrese los datos del teléfono correctamente.";
                                        return mensaje;
                                    }
                                    usuario.Sexo = sexo;

                                    usuario.Sede = sede;
                                    usuario.Correo = correo;
                                    usuario.Estado = 1;
                                    usuario.Session = "hola";
                                    usuario.RolId = int.Parse(rol);
                                    usuario.LastModified = DateTime.Now;

                                    dao.CrearUsuario(usuario);
                                }
                                else
                                {
                                    dao.agregarUsuarioNuevamente(cedula);
                                    usu = dao.traerUsuarios2(sede);
                                    mensaje = "Este usuario ya existe.";
                                    return mensaje;
                                }
                            }
                            else
                            {
                                mensaje = "Ingrese la cedula del Admin correctamente.";
                                return mensaje;
                            }
                        }
                        else
                        {
                            mensaje = "Ingrese el telefono del Admin correctamente.";
                            return mensaje;
                        }
                    }
                    else
                    {
                        mensaje = "Ingrese el nombre del Admin correctamente.";
                        return mensaje;
                    }
                }
                else
                {
                    mensaje = "Ingrese todos los datos.";
                    return mensaje;
                }
            }
            else
            {
                mensaje = "Ya existe";
                return mensaje;
            }
            return mensaje;
        }

        public Usuario llenarCampos(DataTable usu, string cedula)
        {
            for(int i=0; i< usu.Rows.Count; i++)
            {
                if(usu.Rows[i]["cedula"].ToString() == cedula)
                {
                    usuario.Cedula = long.Parse(usu.Rows[i]["cedula"].ToString());
                    usuario.Nombre = usu.Rows[i]["nombre"].ToString();
                    usuario.Clave = usu.Rows[i]["clave"].ToString();
                    usuario.Direccion = usu.Rows[i]["direccion"].ToString();
                    usuario.Telefono = long.Parse(usu.Rows[i]["telefono"].ToString());
                    usuario.Correo = usu.Rows[i]["correo"].ToString();
                    usuario.RolId = int.Parse(usu.Rows[i]["rol_id"].ToString());
                }
            }
            return usuario;
        }
        bool validarLlenoEditar()
        {
            if (cedula0 == "" || nombre0 == "" || clave0 == "" || direccion0 == "" || telefono0 == "" || correo0 == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string hacerTodoEditar()
        {
            bool resultadoNombre = Regex.IsMatch(nombre0, @"^[a-zA-Z]+$");
            if (validarIngresadoEditar())
            {
                if (validarLlenoEditar() == true)
                {
                    if (resultadoNombre == true)
                    {
                        if (validarNumeros(telefono0) == true)
                        {
                            Usuario usuario2 = new Usuario();

                            usuario2.Cedula = int.Parse(cedula0);
                            usuario2.Nombre = nombre0;
                            usuario2.Clave = clave0;
                            usuario2.Direccion = direccion0;
                            usuario2.Estado = 1;
                            usuario2.Telefono = int.Parse(telefono0);
                            usuario2.Sexo = sexo0;
                            usuario2.Sede = sede0;
                            usuario2.Correo = correo0;
                            usuario2.Session = "hola";
                            usuario2.RolId = int.Parse(rol0);
                            usuario2.LastModified = DateTime.Now;

                            dao.actualizarUsuario(usuario2);
                        }
                        else
                        {
                            mensaje = "Ingrese el telefono del Admin correctamente.";
                            return mensaje;
                        }
                    }
                    else
                    {
                        mensaje = "Ingrese el nombre del Admin correctamente.";
                        return mensaje;
                    }
                }
                else
                {
                    mensaje = "Ingrese todos los datos. ";
                    return mensaje;
                }
            }
            else
            {
                mensaje = "Ya existe";
                return mensaje;
            }
            return mensaje;
        }
        public bool validarIngresadoAgregar()
        {
            DataTable cedulatabla = new DataTable();

            cedulatabla = dao.traerUsuarios();
            for (int i = 0; i < cedulatabla.Rows.Count; i++)
            {
                if (cedulatabla.Rows[i]["cedula"].ToString() == cedula && cedulatabla.Rows[i]["estado"].ToString() == "2")
                {
                    return false;
                }
            }
            return true;
        }
        public bool validarIngresadoEditar()
        {
            DataTable cedulatabla = new DataTable();

            cedulatabla = dao.traerUsuarios();
            for (int i = 0; i < cedulatabla.Rows.Count; i++)
            {
                if (cedulatabla.Rows[i]["cedula"].ToString() == cedula && cedulatabla.Rows[i]["estado"].ToString() == "2")
                {
                    return false;
                }
            }
            return true;
        }
    }    
}
