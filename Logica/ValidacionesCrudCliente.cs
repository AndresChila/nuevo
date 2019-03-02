using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{

    public class ValidacionesCrudCliente
    {
        public ValidacionesCrudCliente()
        {

        }

        DAOUsuario dao = new DAOUsuario();
        Cliente cliente = new Cliente();
        DataTable cli = new DataTable();

        string nombre, cedula, apellido, direccion, telefono, sexo;
        string nombree, cedulae, apellidoe, direccione, telefonoe, sexoe;
        string mensaje;
        string accion;
        bool resultadoNombre, resultadoApellido;
        public ValidacionesCrudCliente(string nombrea, string cedulaa, string apellidoa, string direcciona, string telefonoa, string sexoa,
            string nombree, string cedulae, string apellidoe, string direccione, string telefonoe, string sexoe, string accion, bool resultadoNombre, bool resultadoApellido)
        {
            this.nombre = nombrea;
            this.cedula = cedulaa;
            this.direccion = direcciona;
            this.telefono = telefonoa;
            this.apellido = apellidoa;
            this.sexo = sexoa;
            this.nombree = nombree;
            this.cedulae = cedulae;
            this.direccione = direccione;
            this.telefonoe = telefonoe;
            this.apellidoe = apellidoe;
            this.sexoe = sexoe;
            this.accion = accion;
            this.resultadoNombre = resultadoNombre;
            this.resultadoApellido = resultadoApellido;

            if (accion == "guardar")
            {
                hacertodoagregar();
            }
            if (accion == "editar")
            {
                hacertodoeditar();
            }
        }

        bool validarLlenoEditar()
        {
            if (cedulae == "" || nombree == "" || apellidoe == "" || direccione == "" || telefonoe == "")
            {
                mensaje = "debe llenar campos en la session editar";
                return false;
            }
            else
            {
                return true;
            }
        }

        bool validarLlenoAgregar(string cedula, string nombre, string apellido, string direccion, string telefono)
        {
            if (cedula == "" || nombre == "" || apellido == "" || direccion == "" || telefono == "")
            {
                mensaje = "No puede dejar espacios en blanco";
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
                mensaje = "cedula y telefono solo puede tener numeros";
                return false;
            }
        }

        public string hacertodoagregar()
        {
            if (validarLlenoAgregar(cedula, nombre, apellido, direccion, telefono) == true)
            {
                if (validarExistente(cedula))
                {
                    if (validarNumeros(cedula) == true)
                    {
                        if (resultadoNombre == true)
                        {
                            if (resultadoApellido == true)
                            {
                                if (validarNumeros(telefono) == true)
                                {
                                    cliente.Cedula = Convert.ToInt32(cedula);
                                    cliente.Nombre = nombre;
                                    cliente.Apellido = apellido;
                                    cliente.Direccion = direccion;
                                    cliente.Telefono = Convert.ToInt64(telefono);
                                    cliente.Sexo = sexo;
                                    if (cliente.Cedula <= 0 || cliente.Telefono <= 0)
                                    {
                                        mensaje = "Ingrese los datos del teléfono correctamente.";
                                    }
                                    dao.CrearCliente(cliente);
                                    mensaje = "Cliente registrado exitosamente.";

                                    cedula = "";
                                    nombre = "";
                                    apellido = "";
                                    direccion = "";
                                    telefono = "";

                                }
                                else
                                {
                                    mensaje = "Ingrese el telefono del Cliente correctamente";
                                }
                            }
                            else
                            {
                                mensaje = "Ingrese el apellido del Cliente correctamente.";
                            }
                        }
                        else
                        {
                            mensaje = "Ingrese el nombre del Cliente correctamente.";
                        }
                    }
                    else
                    {
                        mensaje = "Ingrese la cedula del Cliente correctamente.";
                    }
                }
                else
                {
                    mensaje = "Cliente Ya Existeee!.";
                }
            }
            else
            {
                mensaje = "Ingrese todos los datos.";
            }
            return mensaje;
        }

        public string devuelvemensaje()
        {
            return mensaje;
        }

        public string hacertodoeditar()
        {
            if (validarLlenoEditar() == true)
            {
                if (validarNumeros(cedulae) == true)
                {
                    if (resultadoNombre == true)
                    {
                        if (resultadoApellido == true)
                        {
                            if (validarNumeros(telefonoe) == true)
                            {
                                Cliente cliente2 = new Cliente();

                                cliente2.Cedula = int.Parse(cedulae);

                                cliente2.Nombre = nombree;
                                cliente2.Apellido = apellidoe;
                                cliente2.Direccion = direccione;
                                cliente2.Telefono = Convert.ToInt64(telefonoe);
                                cliente2.Sexo = sexoe;
                                if (cliente2.Cedula <= 0 || cliente2.Telefono <= 0)
                                {
                                    mensaje = "Ingrese los datos correctamente.";
                                }
                                dao.actualizarCliente(cliente2);

                                cedulae = "";
                                nombree = "";
                                apellidoe = "";
                                direccione = "";
                                telefonoe = "";
                            }
                            else
                            {
                                mensaje = "Ingrese el telefono del Cliente correctamente.";
                            }
                        }
                        else
                        {
                            mensaje = "Ingrese el apellido del Cliente correctamente.";
                        }
                    }
                    else
                    {
                        mensaje = "Ingrese el nombre del Cliente correctamente.";
                    }
                }
                else
                {
                    mensaje = "Ingrese la cedula del Cliente correctamente.";
                }
            }
            else
            {
                mensaje = "Ingrese todos los datos.";
            }
            return mensaje;
        }

        Cliente clientico = new Cliente();
        public void RowCommand(string name, string argument)
        {
            DAOUsuario dAO = new DAOUsuario();
            if (name.Equals("Eliminar"))
            {
                int id = Convert.ToInt32(argument);
                dao.eliminarCliente(id);
                this.SetCliente(0);
            }
            if (name.Equals("Editar"))
            {
                this.SetCliente(Convert.ToInt32(argument));
            }
        }

        public void SetCliente(int r)
        {
            DAOUsuario dAO = new DAOUsuario();
            int id = r;
            DataTable clientes = dAO.verClientesEditar(id);
            if (clientes != null && r > 0)
            {
                foreach (DataRow row in clientes.Rows)
                {
                    clientico.Cedula = Convert.ToInt32(row["cedula"]);
                    clientico.Nombre = Convert.ToString(row["nombre"]);
                    clientico.Apellido = Convert.ToString(row["apellido"]);
                    clientico.Direccion = Convert.ToString(row["direccion"]);
                    clientico.Telefono = Convert.ToInt64(row["telefono"]);                    
                }
            }
            else
            {
                clientico.Cedula = 0;
                clientico.Nombre = "";
                clientico.Apellido = "";
                clientico.Direccion = "";
                clientico.Telefono = 0;
            }
        }

        public Cliente Get_Clientico()
        {
            return clientico;
        }

        public bool validarExistente(string cedula)
        {
            DataTable cl = new DataTable();
            cl = dao.traerClientes();
            for(int i = 0; i < cl.Rows.Count; i++)
            {
                if(cl.Rows[i]["cedula"].ToString() == cedula){
                    return false;
                }

            }
            return true;
        }
    }    
}
