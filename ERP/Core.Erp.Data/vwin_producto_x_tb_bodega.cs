//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwin_producto_x_tb_bodega
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public double Stock_minimo { get; set; }
        public string pr_descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public string Su_Descripcion { get; set; }
        public string ca_Categoria { get; set; }
        public string pr_codigo { get; set; }
        public string IdCtaCble_Costo { get; set; }
        public string pc_Cuenta { get; set; }
        public string IdCtaCble_Inven { get; set; }
        public string pc_Cuenta_Inv { get; set; }
    }
}
