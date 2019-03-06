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

public partial class View_Tienda_NuevaVenta : System.Web.UI.Page
{

    Cliente cliente = new Cliente();
    DataTable cli = new DataTable();
    Producto producto = new Producto();
    DataTable cli2 = new DataTable();
    DataTable cli3 = new DataTable();
    bool pbNV;



    private List<Producto> listaVenta
    {
        get { return Session["l"] as List<Producto>; }
        set { Session["l"] = value; }
    }

    private string valorVenta
    {
        get { return Session["valorVenta"] as string; }
        set { Session["valorVenta"] = value; }
    }

    private string idCliente
    {
        get { return Session["idCliente"] as string; }
        set { Session["idCliente"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            llenarGridView();
            Session["lis"] = null;
        }


    }
    /////////////////////////////////////////////////////////////////////////////////////////arreglado
    public void llenarGridView()
    {
        LlenarGridViews llenara = new LlenarGridViews();
        
        GV_VentaPedido.DataSource = llenara.llenarGridViewVenta(Session["sede"].ToString(), pbNV); 
        GV_VentaPedido.DataBind();
    }
    

    protected void GV_Productos_SelectedIndexChanged(object sender, GridViewPageEventArgs e)
    {

    }
    

    protected void B_BuscarCliente_Click(object sender, EventArgs e)
    {
        DAOUsuario dao = new DAOUsuario();
        DataTable cliente = new DataTable();
        BuscarCliente clnte = new BuscarCliente();
        Cliente datosCliente = new Cliente();
        
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('"+ clnte.buscarVacio(TB_BuscarCliente.Text) + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete

        cliente = dao.buscarCliente(int.Parse(TB_BuscarCliente.Text));
        datosCliente = clnte.BuscarDatosCliente(TB_BuscarCliente.Text, cliente.Rows.Count);

            
        TB_Nombre.Text = datosCliente.Nombre;
        TB_Apellido.Text = datosCliente.Apellido;
        B_Seleccionar.Enabled = true;
            
        Session["idCliente"] = TB_BuscarCliente.Text;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {


    }



    protected void B_Seleccionar_Click(object sender, EventArgs e)
    {
        //Session["l"] = null;
        AgregarProductos agregar = new AgregarProductos();

        llenarVenta(agregar.AnalizarGridView(TB_Cantida.Text.ToString(), double.Parse(LTalla.Text.ToString()), LRef.Text.ToString(), Session["sede"].ToString(), (List<Producto>)Session["lis"]));
        string msg = agregar.get_mensaje();
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + msg + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        actualizarGV_Venta();
    }

    void llenarVenta(List<Producto> listaVV)
    {
        Session["lis"] = listaVV;
        AgregarProductos tot = new AgregarProductos();
        double precioTotal = 0;
        List<Producto> listaV = new List<Producto>();
        listaV = (Session["lis"] as List<Producto>);
        precioTotal = tot.sumarTotal(listaV);
        Session["valorVenta"] = Convert.ToString(precioTotal);
    }

    void actualizarGV_Venta()
    {
        List<Producto> listaV = new List<Producto>();
        listaV = (Session["lis"] as List<Producto>);
        GV_Venta.DataSource = listaV;
        GV_Venta.DataBind();
        AgregarProductos p = new AgregarProductos();
        try
        {
            ((TextBox)GV_Venta.FooterRow.FindControl("TB_TotalVenta")).Text = p.valorVenta(Session["valorVenta"].ToString());
        }
        catch(Exception e)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('No hay productos para añadir a la grid venta');</script>");
#pragma warning restore CS0618 // Type or member is obsolete 
        }
    }

    void irAFactura()
    {
        Session["lis"] = null;
        Response.Redirect("../Tienda/VistaFactura.aspx");
    }
     void irAFactura2()
    {
        Session["lis"] = null;
        Response.Redirect("../Tienda/VistaAbono.aspx");
    }

    protected void B_Facturar_Click(object sender, EventArgs e)
    {

        Venta venta = new Venta();
        DateTime fechaHoy = DateTime.Now;
        venta.Idcliente = int.Parse(Session["idCliente"].ToString());
        venta.Idvendedor = int.Parse(Session["user_id"].ToString());
        venta.Producto = (Session["lis"] as List<Producto>);
        venta.Fecha = fechaHoy.ToString("d");
        venta.Precio = double.Parse(Session["valorVenta"].ToString());
        venta.Sede = Session["sede"].ToString();
        AgregarProductos fact = new AgregarProductos();
        fact.actualizarVenta(venta);
        actualizarInventario();
        reiniciar();
        this.irAFactura();
    }



    void actualizarInventario()
    {
        AgregarProductos actInvt = new AgregarProductos();
        actInvt.paraInvent(Session["lis"] as List<Producto>, Session["sede"].ToString());
    }

     protected void GV_Productos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_VentaPedido.PageIndex = e.NewPageIndex;
        this.llenarGridView();
    }
    

    protected void GV_Venta_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<Producto> pventa = new List<Producto>();
        pventa = (Session["lis"] as List<Producto>);
        AgregarProductos borr = new AgregarProductos();
        Session["lis"] = borr.BorrarDelGrid(e.CommandName.ToString(), pventa, e.CommandArgument.ToString());
        actualizarGV_Venta();
                   
    }

    protected void GV_Venta_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_Venta_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    void reiniciar()
    {
        Session["lis"] = null;
        llenarGridView();
        actualizarGV_Venta();
        TB_Nombre.Text = "";
        TB_Apellido.Text = "";
        L_InfoCliente.Text = "";
        TB_BuscarCliente.Text = "";
    }

    protected void B_Abono_Click(object sender, EventArgs e)
    {
        AgregarProductos abono = new AgregarProductos();
        Abono venta = new Abono();
        DateTime fechaHoy = DateTime.Now;
        venta.Idcliente = Convert.ToInt32(Session["idCliente"]);
        venta.Idvendedor = Convert.ToInt32(Session["user_id"]);
        venta.Producto = (Session["lis"] as List<Producto>);
        venta.Fecha = fechaHoy.ToString("d");
        venta.Precio = Convert.ToDouble(Session["valorVenta"]);
        venta.Sede = Convert.ToString(Session["sede"]);
        string msj2 = abono.crearAbono(Convert.ToInt32(Session["idCliente"]), venta);
#pragma warning disable CS0618 // Type or member is obsolete
        RegisterStartupScript("mensaje", "<script type='text/javascript'>alert('" + msj2 + "');</script>");
#pragma warning restore CS0618 // Type or member is obsolete
        actualizarInventario();
        reiniciar();
        this.irAFactura2();
    }

    protected void B_BuscarProducto_Click(object sender, EventArgs e)
    {
        AgregarProductos boton = new AgregarProductos();        
        GV_VentaPedido.DataSource = boton.actualizar(((TextBox)GV_VentaPedido.HeaderRow.FindControl("TB_BuscarReferencia")).Text, ((TextBox)GV_VentaPedido.HeaderRow.FindControl("TB_BuscarTalla")).Text, Convert.ToString(Session["sede"]), pbNV); ;
        GV_VentaPedido.DataBind();        
    }

    protected void GV_VentaPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void B_Cancelar_Click(object sender, EventArgs e)
    {
        this.reiniciar();
    }

    protected void GV_VentaPedido_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        AgregarProductos ag = new AgregarProductos();
        List<Producto> prod = new List<Producto>();
        string[] argumentos = e.CommandArgument.ToString().Split(new char[] { ',' });
        string referencia = argumentos[0];
        string talla = argumentos[1];

        producto.Referencia = referencia;
        producto.Talla = double.Parse(talla);
        pintarSeleccionado(producto);
        
    }

    protected void TB_Cantidad_TextChanged(object sender, EventArgs e)
    {
        
    }
    public void pintarSeleccionado(Producto p)
    {
        LRef.Text = p.Referencia;
        LTalla.Text = p.Talla.ToString();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}