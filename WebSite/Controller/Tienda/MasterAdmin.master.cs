﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;

public partial class View_Tienda_MasterAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidarMasterAdmin val = new ValidarMasterAdmin();
        //Response.Redirect(val.validarSession(Session["user_id"].ToString(), Session["clave"].ToString(), Session["rol_id"].ToString(), Session["sede"].ToString()));
        Label_usuario.Text = Session["nombre"].ToString();
        Label_Sede.Text = Session["sede"].ToString();
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {

        Response.Redirect("PedirProductos.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("VerPedidos.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("RecibirPedido.aspx");

    }

    protected void LinkBodega_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bodega.aspx");

    }

    protected void LinkVendedor_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRUDVendedor.aspx");
    }

    protected void B_CerrarSesion_Click(object sender, EventArgs e)
    {
        this.cerrarSesion();
    }

    void cerrarSesion()
    {
        Session["clave"] = null;
        Session["user_id"] = null;
        Session["nombre_rol"] = null;
        Session["nombre"] = null;
        Session["sede"] = null;
        Session["rol_id"] = null;
        Response.Cache.SetNoStore();
        Response.Redirect("../Login-Rec/NuevoLogin.aspx");
    }
}
