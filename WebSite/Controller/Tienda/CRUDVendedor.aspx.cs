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

public partial class View_Tienda_CRUDVendedor : System.Web.UI.Page
{
    DAOUsuario dao = new DAOUsuario();
    Usuario usuario = new Usuario();
    DataTable usu = new DataTable();
    DataTable sedess = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidarMasterAdmin val = new ValidarMasterAdmin();
        //Response.Redirect(val.validarSession(Session["user_id"].ToString(), Session["clave"].ToString(), Session["rol_id"].ToString(), Session["sede"].ToString()));
        usu = dao.traerUsuarios2(Session["sede"].ToString());
        GV_Productos.DataSource = usu;
        GV_Productos.DataBind();
        sedess = dao.traerSedes();

        

        if (!IsPostBack)
        {
            for (int i = 0; i < usu.Rows.Count; i++)
            {
                DropDownList1.Items.Add(usu.Rows[i]["cedula"].ToString());
            }
        }
        TB_Rol.Text = "3";
    }

    protected void B_Agregar_Click(object sender, EventArgs e)
    {
        ValidacionesCRUDVendedor val = new ValidacionesCRUDVendedor(TB_Cedula.Text, TB_Nombre.Text, TB_Clave.Text, TB_Direccion.Text, TB_Telefono.Text, 
                                                                     TB_Correo.Text, D_Sexo.SelectedValue.ToString(), Session["sede"].ToString(), TB_Rol.Text,
                                                                     null, null, null, null, null,
                                                                     null, null, null, null);

        string a =val.hacerTodoAgregar();
        

        usu = dao.traerUsuarios2(Session["sede"].ToString());
        GV_Productos.DataSource = usu;
        GV_Productos.DataBind();
        DropDownList1.Items.Add(TB_Cedula.Text);
        TB_Cedula.Text = "";
        TB_Nombre.Text = "";
        TB_Clave.Text = "";
        TB_Direccion.Text = "";
        TB_Telefono.Text = "";
        TB_Correo.Text = "";

#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('"+a+"');</script>");
#pragma warning restore CS0618 // Type or member is obsolete

    }

    protected void B_Seleccionar_Click(object sender, EventArgs e)
    {
        ValidacionesCRUDVendedor val = new ValidacionesCRUDVendedor(null, null, null, null, null,
                                                                     null, null, null, null, null, null, null, null, null,
                                                                     null, null, null, null);
        usu = dao.traerUsuarios2(Session["sede"].ToString());
        Usuario usuario = val.llenarCampos(usu, DropDownList1.SelectedItem.ToString());

        TB_Cedula0.Text = usuario.Cedula.ToString();
        TB_Nombre0.Text = usuario.Nombre;
        TB_Clave0.Text = usuario.Clave;
        TB_Direccion0.Text = usuario.Direccion;
        TB_Telefono0.Text = usuario.Telefono.ToString();
        TB_Correo0.Text = usuario.Correo;
        TB_Rol0.Text = usuario.RolId.ToString();
            
    }

    protected void B_Actualizar_Click(object sender, EventArgs e)
    {

        ValidacionesCRUDVendedor val = new ValidacionesCRUDVendedor(null, null, null, null, null,
                                                                     null, null, null, null,
                                                                     TB_Cedula0.Text, TB_Nombre0.Text, TB_Clave0.Text, TB_Direccion0.Text, TB_Telefono0.Text,
                                                                     TB_Correo0.Text, D_Sexo0.SelectedValue.ToString(), Session["sede"].ToString(), TB_Rol0.Text);
        string a = val.hacerTodoEditar();
        usu = dao.traerUsuarios2(Session["sede"].ToString());
        GV_Productos.DataSource = usu;
        GV_Productos.DataBind();
        DropDownList1.Items.Add(TB_Cedula.Text);
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('"+a+"');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
    }

    protected void B_Eliminar_Click(object sender, EventArgs e)
    {
        Usuario usuario3 = new Usuario();
        usuario3.Cedula = int.Parse(DropDownList1.SelectedItem.ToString());
        dao.eliminarUsuario(DropDownList1.SelectedItem.ToString());

        usu = dao.traerUsuarios2(Session["sede"].ToString());
        GV_Productos.DataSource = usu;
        GV_Productos.DataBind();
        DropDownList1.Items.Remove(DropDownList1.SelectedItem.ToString());
    }

    

    

    
}