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
    
    public partial class vwcp_conciliacion_Caja_det
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_OGiro { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public string Tipo_documento { get; set; }
        public double MontoAplicado { get; set; }
        public decimal IdPersona { get; set; }
        public string pe_nombreCompleto { get; set; }
        public decimal IdProveedor { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public double co_Por_iva { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public string co_observacion { get; set; }
        public double co_baseImponible { get; set; }
        public double co_valoriva { get; set; }
        public double co_valorpagar { get; set; }
        public double SaldoOG { get; set; }
        public decimal Valor_a_aplicar { get; set; }
        public Nullable<int> IdEmpresa_OP { get; set; }
        public Nullable<decimal> IdOrdenPago_OP { get; set; }
        public string co_factura { get; set; }
        public double co_total { get; set; }
    }
}
