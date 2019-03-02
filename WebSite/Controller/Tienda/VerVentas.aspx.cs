using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Logica;

public partial class Controller_Tienda_VerVentas : System.Web.UI.Page
{
    DataTable sdata, suser_id;
    int flag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        sdata = Session["data"] as DataTable;
        suser_id = Session["user_id"] as DataTable;
    }

    protected void B_Ir_Click(object sender, EventArgs e)
    {
        MisVentas filtrar = new MisVentas(DL_Filtrar.SelectedValue, Convert.ToString(TB_Fecha1.Text), Convert.ToString(TB_Fecha2.Text), sdata, suser_id);
        this.ponerIn(filtrar.Get_Estado());
        this.llenarGV_Ventas();
        B_Ir.Enabled = filtrar.Get_Estado2();
    }

    public void ponerIn(bool est)
    {
        Label6.Visible = est;
        Label7.Visible = est;
        TB_Fecha1.Visible = est;
        TB_Fecha2.Visible = est;
        B_Buscar.Visible = est;
    }

    void llenarGV_Ventas()
    {
        MisVentas a = new MisVentas();
        DataTable data = a.Get_GV_Ventas();
        GV_Ventas.DataSource = data;
        GV_Ventas.DataBind();
    }

    protected void B_Buscar_Click(object sender, EventArgs e)
    {
        MisVentas venta = new MisVentas(DL_Filtrar.SelectedValue, Convert.ToString(TB_Fecha1.Text), Convert.ToString(TB_Fecha2.Text), sdata, suser_id);
        string a = venta.traerMensaje();
        B_Ir.Enabled = venta.Get_Estado2();
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + a + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
    }

    protected void GV_Ventas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Ventas.PageIndex = e.NewPageIndex;
        this.llenarGV_Ventas();
    }

    
}