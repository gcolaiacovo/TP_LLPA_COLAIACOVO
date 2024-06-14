using System;

public partial class CardProducto : System.Web.UI.UserControl
{
    public string IdProducto
    {
        get { return linkProducto.PostBackUrl; }
        set { linkProducto.PostBackUrl = "ProductoDetail.aspx?IdProducto=" + value ; }
    }

    public string UrlImagen
    {
        get { return imgProducto.ImageUrl; }
        set { imgProducto.ImageUrl = value; }
    }

    public string Nombre
    {
        get { return txtNombre.Text; }
        set { txtNombre.Text = value; }
    }

    public decimal Precio
    {
        get { return Convert.ToDecimal(txtPrecio.Text); }
        set { txtPrecio.Text = value.ToString("0.00"); txtPrecioSinOferta.Text = (value * (decimal)1.2).ToString("0.00"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}