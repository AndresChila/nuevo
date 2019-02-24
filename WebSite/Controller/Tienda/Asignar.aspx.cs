using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;

public partial class View_Tienda_Asignar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Asignaciones pen = new Asignaciones();
        L_CantidadPendientes.Text = Convert.ToString(pen.CantidadPendiente());
    }

    String compara
    {
        get { return Session["compara"] as String; }
        set { Session["compara"] = value; }
    }
    String idPed
    {
        get { return Session["idPed"] as String; }
        set { Session["idPed"] = value; }
    }
    List<Asignacion> listaAsignacion
    {
        get { return Session["asignacionl"] as List<Asignacion>; }
        set { Session["asignacionl"] = value; }
    }
    List<Asignacion> listaAsignacion2
    {
        get { return Session["asignacion2"] as List<Asignacion>; }
        set { Session["asignacion2"] = value; }
    }

    protected void B_Asignar_Click(object sender, EventArgs e)
    {
        Asignaciones agregar = new Asignaciones();
        foreach (GridViewRow fila in GV_AsignarSinPedido.Rows)
        {
            agregar.AgregarProductos(Convert.ToString(((Label)fila.Cells[0].FindControl("L_Referencia")).Text), Convert.ToDouble(((Label)fila.Cells[1].FindControl("L_Talla")).Text), ((TextBox)fila.Cells[2].FindControl("TB_Cantidad")).Text, DL_Sedes.SelectedValue);
            Response.Write("<script>window.alert('" + agregar.Devuelve_Mensaje() + "';</script>");
        }
    }

    protected void GV_Asignaciones_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_Pedido_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_Pedido_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Asignaciones row = new Asignaciones();
        Session["idPed"] = Convert.ToString(e.CommandArgument);
        Session["sedePedido"] = row.Sede();
        GV_Pedidos.DataSource = row.Row_Command(e.CommandName.ToString(), e.CommandArgument.ToString());
        GV_Pedidos.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Asignaciones validar = new Asignaciones();
        foreach (GridViewRow row in GV_Pedidos.Rows)
        {
            validar.Asignar(Convert.ToString(((Label)row.Cells[0].FindControl("L_Referencia")).Text), Convert.ToDouble(((Label)row.Cells[1].FindControl("L_Talla")).Text), Convert.ToInt32(((Label)row.Cells[2].FindControl("L_Cantidad")).Text), GV_Pedidos.Rows.Count, (Session["asignacion2"] as List<Asignacion>), Convert.ToString(Session["sedePedido"]));
            Session["entregado"] = validar.GetEntregado();
            Session["asignacion2"] = validar.GetPedidos();
            Session["idproducto"] = validar.GetId();
#pragma warning disable CS0618 // Type or member is obsolete
            RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + validar.Devuelve_Mensaje() + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        }
        Button2.Enabled = validar.GeT_Estado();
        Button3.Enabled = !validar.GeT_Estado();
        GV_Pedido.DataBind();
        GV_Pedidos.DataBind();
        GV_ProductosBodega.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        Asignaciones ingresar = new Asignaciones();
        ingresar.ingresarBD(Session["asignacion2"] as List<Asignacion>, Convert.ToInt32(Session["idproducto"]), Convert.ToInt32(Session["entregado"]));
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + ingresar.Devuelve_Mensaje() + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete    
        GV_Pedido.DataBind();
        GV_Pedidos.DataBind();
        GV_ProductosBodega.DataBind();
    }
}