//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    
    public partial class sppro_GetProductoFacturadosPorFecha_Result
    {
        public int IdEmpresa { get; set; }
        public decimal IdProducto { get; set; }
        public string pr_descripcion { get; set; }
        public Nullable<double> vt_cantidad { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
        public string NombreUnidad { get; set; }
        public double stock { get; set; }
        public Nullable<double> CantidadFabricada { get; set; }
    }
}
