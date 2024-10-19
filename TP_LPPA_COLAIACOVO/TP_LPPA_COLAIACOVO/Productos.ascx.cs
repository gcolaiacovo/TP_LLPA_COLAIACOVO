using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class Productos : System.Web.UI.UserControl
{
    private readonly BLLProducto bLLProducto;

    public Productos()
    {
        bLLProducto = new BLLProducto();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var productos = bLLProducto.GetProductos();
            if (productos.Any())
            {
                repeaterProductos.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Producto).ToList();
                repeaterProductos.DataBind();

                repeaterServicios.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Servicio).ToList();
                repeaterServicios.DataBind();
            }
        }
    }
}