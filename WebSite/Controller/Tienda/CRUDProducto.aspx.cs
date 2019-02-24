    using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;

public partial class View_Tienda_CRUDProducto : System.Web.UI.Page
{
    String compara
    {
        get { return Session["compara"] as String ; }
        set { Session["compara"] = value;  }
    }
    String id
    {
        get { return Session["idproducto"] as String; }
        set { Session["idproducto"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["idproducto"] = null;
    }

    protected void B_AgregarProducto_Click(object sender, EventArgs e)
    {
        ValidacionesCRUDProducto val = new ValidacionesCRUDProducto();
        val.AgregarProducto(TB_ReferenciaProducto.Text, TB_Precio.Text, TB_Cantidad.Text, DL_Tallas.SelectedValue);
        Response.Write("<script>window.alert('" + val.devuelvemensaje() + "';</script>");
        GV_Productos.DataBind();
        this.reiniciar();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_Productos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ValidacionesCRUDProducto validaciones = new ValidacionesCRUDProducto();
        validaciones.RowCommand(e.CommandName, e.CommandArgument.ToString(), Convert.ToInt32(e.CommandArgument));
        Session["idproducto"] = Convert.ToString(e.CommandArgument);
        Seleccionar_Producto(validaciones.GetProducto());
    }

    void Seleccionar_Producto(Producto producto)
    {        
        Session["compara"] = Convert.ToString(producto.Cantidad);
        TB_EditarReferencia.Text = producto.Referencia;
        TB_EditarCantidad.Text = Convert.ToString(producto.Cantidad);
        TB_EditarPrecio.Text = Convert.ToString(producto.Precio);
        DL_EditarTallas.SelectedValue = Convert.ToString(producto.Talla);
        B_EditarProducto.Enabled = true;
        B_Cancelar.Enabled = true;
    }

    protected void B_EditarProducto_Click(object sender, EventArgs e)
    {
        ValidacionesCRUDProducto val = new ValidacionesCRUDProducto();
        val.EditarProducto(TB_EditarReferencia.Text, TB_EditarPrecio.Text, TB_EditarCantidad.Text, DL_EditarTallas.SelectedValue, Convert.ToInt32(Session["idproducto"]), Convert.ToString(Session["compara"]));
        Response.Write("<script>window.alert('" + val.devuelvemensaje() + "';</script>");
        GV_Productos.DataBind();
        TB_EditarReferencia.Text = "";
        TB_EditarCantidad.Text = "";
        TB_EditarPrecio.Text = "";
        DL_EditarTallas.SelectedIndex = 0;
        B_EditarProducto.Enabled = false;
        B_Cancelar.Enabled = false;
        Session["compara"] = null;
    }

    protected void B_Cancelar_Click(object sender, EventArgs e)
    {
        TB_EditarReferencia.Text = "";
        TB_EditarReferencia.Text = "";
        TB_EditarPrecio.Text = "";
        DL_EditarTallas.SelectedIndex = 0;
        B_EditarProducto.Enabled = false;
        B_Cancelar.Enabled = false;
    }

    protected void DL_ReferenciaProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        B_EditarProducto.Enabled = false;
        B_Cancelar.Enabled = false;
    }

    void reiniciar()
    {
        TB_ReferenciaProducto.Text = "";
        TB_Precio.Text = "";
        TB_Cantidad.Text = "";
        DL_Tallas.SelectedIndex = 0;
    }

    protected void TB_EditarPrecio_TextChanged(object sender, EventArgs e)
    {

    }
}