//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class fa_guia_remision_det
    {
        public fa_guia_remision_det()
        {
            this.fa_guia_remision_det_x_factura = new HashSet<fa_guia_remision_det_x_factura>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdGuiaRemision { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public double gi_cantidad { get; set; }
        public string gi_detallexItems { get; set; }
    
        public virtual fa_guia_remision fa_guia_remision { get; set; }
        public virtual ICollection<fa_guia_remision_det_x_factura> fa_guia_remision_det_x_factura { get; set; }
    }
}
