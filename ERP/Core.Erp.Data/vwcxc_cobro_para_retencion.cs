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
    
    public partial class vwcxc_cobro_para_retencion
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public decimal vt_Subtotal { get; set; }
        public decimal vt_iva { get; set; }
        public decimal vt_total { get; set; }
        public string Nombres { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public string vt_Observacion { get; set; }
        public string vt_NumFactura { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal IdCliente { get; set; }
        public Nullable<bool> TieneRetencion { get; set; }
        public bool cr_EsElectronico { get; set; }
        public Nullable<double> Saldo { get; set; }
    }
}
