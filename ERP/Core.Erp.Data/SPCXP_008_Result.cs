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
    
    public partial class SPCXP_008_Result
    {
        public long IdRow { get; set; }
        public int IdEmpresa { get; set; }
        public Nullable<decimal> IdCbteCble_Ogiro { get; set; }
        public Nullable<decimal> IdTipoCbte_Ogiro { get; set; }
        public string IdOrden_giro_Tipo { get; set; }
        public string Documento { get; set; }
        public string nom_tipo_doc { get; set; }
        public string cod_tipo_doc { get; set; }
        public decimal IdProveedor { get; set; }
        public string nom_proveedor { get; set; }
        public double Valor_a_pagar { get; set; }
        public double MontoAplicado { get; set; }
        public double Saldo { get; set; }
        public string Observacion { get; set; }
        public string Ruc_Proveedor { get; set; }
        public string representante_legal { get; set; }
        public string Tipo_cbte { get; set; }
        public int Plazo_fact { get; set; }
        public System.DateTime co_fechaOg { get; set; }
        public System.DateTime co_FechaFactura_vct { get; set; }
        public Nullable<int> Dias_Vcto { get; set; }
        public Nullable<System.DateTime> Fecha_corte { get; set; }
        public double x_Vencer { get; set; }
        public double Vencido { get; set; }
        public double Vencido_1_30 { get; set; }
        public double Vencido_31_60 { get; set; }
        public double Vencido_60_90 { get; set; }
        public double Vencido_mayor_90 { get; set; }
        public int en_conciliacion { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
