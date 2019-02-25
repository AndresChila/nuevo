using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;
using Utilitarios;

public partial class View_Tienda_NuevoAbono : System.Web.UI.Page
{
    DataTable vntaS,refresh;
    string precioDeuda, pagoActual;
    protected void Page_Load(object sender, EventArgs e)
    {
        llenarGV_Abonos();
    }

    public List<Producto> listaVenta
    {
        get { return Session["refresh"] as List<Producto>; }
        set { Session["refresh"] = value; }
    }

    private DataTable datosAbono
    {
        get { return Session["venta"] as DataTable; }
        set { Session["venta"] = value; }
    }

    private string idAbono
    {
        get { return Session["idAbono"] as string; }
        set { Session["idAbono"] = value; }
    }

    protected void GV_AbonosPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        NuevosAbonos select = new NuevosAbonos(null, null, null, null);
        select.seleccionarAbono(e.CommandName, int.Parse(e.CommandArgument.ToString()));
    }

    void llenarGV_Abonos()  
    {
        DAOUsuario dAO = new DAOUsuario();
        DataTable abo = new DataTable();
        abo = dAO.traerAbonos(Convert.ToString(Session["sede"]));
        string aa = Convert.ToString(Session["sede"]);
        Session["venta"] = abo;
        GV_AbonosPendientes.DataSource = abo;
        GV_AbonosPendientes.DataBind();
    }

    void nuevaVenta()
    {
        vntaS = Session["venta"] as DataTable;
        NuevosAbonos newAbono = new NuevosAbonos(vntaS, precioDeuda, pagoActual, idAbono);
        this.actualizarInventario();
        string a = newAbono.traerMensaje();
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + a + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        Response.Redirect("../Tienda/VistaFactura.aspx");
    }

    void actualizarInventario()
    {
        listaVenta = new List<Producto>();
        DAOUsuario dAO = new DAOUsuario();

        listaVenta = (Session["refresh"] as List<Producto>);
        foreach (Producto p in listaVenta)
        {
            dAO.actualizarInventario(p, Convert.ToString(Session["sede"]));
        }


    }

    protected void B_AgregarProducto_Click(object sender, EventArgs e)
    {
        idAbono = Convert.ToString(Session["idAbono"]);
        NuevosAbonos newAbono = new NuevosAbonos(vntaS, precioDeuda, pagoActual, idAbono);
        this.llenarGV_Abonos();
        string a = newAbono.traerMensaje();
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + a + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete

    }

    protected void GV_AbonosPendientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_AbonosPendientes.PageIndex = e.NewPageIndex;
        this.llenarGV_Abonos();
    }
}