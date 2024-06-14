using System;

public partial class UserReview : System.Web.UI.UserControl
{
    public string Texto
    {
        get { return txtDescripcion.Text; }
        set { txtDescripcion.Text = value; }
    }

    public string UrlImagen
    {
        get { return imagen.ImageUrl; }
        set { imagen.ImageUrl = value; }
    }

    public string Nombre
    {
        get { return txtNombre.Text; }
        set { txtNombre.Text = value; }
    }

    public string CacheBuster { get; set; } // Nuevo atributo para evitar caché


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}