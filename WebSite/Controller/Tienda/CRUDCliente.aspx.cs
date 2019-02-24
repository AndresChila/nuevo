using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;


public partial class View_Tienda_CRUDCliente : System.Web.UI.Page
{
    DAOUsuario dao = new DAOUsuario();
    Cliente cliente = new Cliente();
    DataTable cli = new DataTable();
    string accion;

    protected void Page_Load(object sender, EventArgs e)
    {
        cli = dao.traerClientes();
        GV_Clientes.DataSource = cli;
        GV_Clientes.DataBind();

        if (!IsPostBack)
        {
            
        }
    }

    protected void B_Agregar_Click(object sender, EventArgs e)
    {
        bool resultadoNombre = Regex.IsMatch(TB_Nombre.Text, @"^[a-zA-Z]+$");
        bool resultadoApellido = Regex.IsMatch(TB_Apellido.Text, @"^[a-zA-Z]+$");
        accion = "guardar";
        ValidacionesCrudCliente val = new ValidacionesCrudCliente(TB_Nombre.Text.ToString(), TB_Cedula.Text.ToString(), TB_Apellido.Text.ToString(), TB_Direccion.Text.ToString(),
                                                                  TB_Telefono.Text.ToString(), D_Sexo.SelectedValue.ToString(), TB_Nombre0.ToString(), TB_Cedula0.ToString(),
                                                                  TB_Apellido0.ToString(), TB_Direccion0.ToString(), TB_Telefono0.ToString(),D_Sexo0.SelectedValue.ToString(), 
                                                                 accion, resultadoNombre, resultadoApellido);

#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('Revise los datos." + val.devuelvemensaje() + " ');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        cli = dao.traerClientes();
        GV_Clientes.DataSource = cli;
        GV_Clientes.DataBind();   
       
    }

    void pintarEditar(Cliente cliente)
    {
        TB_Cedula0.Text = cliente.Cedula.ToString();
        TB_Nombre0.Text = cliente.Nombre;
        TB_Apellido0.Text = cliente.Apellido;
        TB_Direccion0.Text = cliente.Direccion;
        TB_Telefono0.Text = cliente.Telefono.ToString();
    }

    protected void B_Actualizar_Click(object sender, EventArgs e)
    {
        accion = "editar";
        bool resultadoNombre = Regex.IsMatch(TB_Nombre0.Text, @"^[a-zA-Z]+$");
        bool resultadoApellido = Regex.IsMatch(TB_Apellido0.Text, @"^[a-zA-Z]+$");
        ValidacionesCrudCliente val = new ValidacionesCrudCliente(TB_Nombre.Text.ToString(), TB_Cedula.Text.ToString(), TB_Apellido.Text.ToString(), TB_Direccion.Text.ToString(),
                                                                  TB_Telefono.Text.ToString(), D_Sexo.SelectedValue.ToString(), TB_Nombre0.Text.ToString(), TB_Cedula0.Text.ToString(),
                                                                  TB_Apellido0.Text.ToString(), TB_Direccion0.Text.ToString(), TB_Telefono0.Text.ToString(), D_Sexo0.SelectedValue.ToString(),
                                                                  accion, resultadoNombre, resultadoApellido);
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + val.devuelvemensaje() + " ');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        cli = dao.traerClientes();
        GV_Clientes.DataSource = cli;
        GV_Clientes.DataBind();
        
    } 
        
    protected void GV_Clientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Clientes.PageIndex = e.NewPageIndex;
        cli = dao.traerClientes();
        GV_Clientes.DataSource = cli;
        GV_Clientes.DataBind();
    }

    protected void GV_Clientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ValidacionesCrudCliente val = new ValidacionesCrudCliente();
        val.RowCommand(e.CommandName.ToString(), e.CommandArgument.ToString());
        pintarEditar(val.Get_Clientico());
        
    }
}