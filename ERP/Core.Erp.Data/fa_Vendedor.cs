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
    
    public partial class fa_Vendedor
    {
        public fa_Vendedor()
        {
            this.fa_cliente_x_fa_Vendedor_x_sucursal = new HashSet<fa_cliente_x_fa_Vendedor_x_sucursal>();
            this.fa_factura = new HashSet<fa_factura>();
            this.fa_proforma = new HashSet<fa_proforma>();
            this.fa_notaCreDeb = new HashSet<fa_notaCreDeb>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdVendedor { get; set; }
        public string Codigo { get; set; }
        public string Ve_Vendedor { get; set; }
        public string NomInterno { get; set; }
        public string ve_cedula { get; set; }
        public double PorComision { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string Estado { get; set; }
        public string MotivoAnula { get; set; }
    
        public virtual ICollection<fa_cliente_x_fa_Vendedor_x_sucursal> fa_cliente_x_fa_Vendedor_x_sucursal { get; set; }
        public virtual ICollection<fa_factura> fa_factura { get; set; }
        public virtual ICollection<fa_proforma> fa_proforma { get; set; }
        public virtual ICollection<fa_notaCreDeb> fa_notaCreDeb { get; set; }
    }
}
