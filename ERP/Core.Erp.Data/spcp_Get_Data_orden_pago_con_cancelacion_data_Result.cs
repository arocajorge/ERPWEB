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
    
    public partial class spcp_Get_Data_orden_pago_con_cancelacion_data_Result
    {
        public long IdRow { get; set; }
        public string IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string IdTipo_op { get; set; }
        public string Referencia { get; set; }
        public string Referencia2 { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int Secuencia_OP { get; set; }
        public string IdTipoPersona { get; set; }
        public decimal IdPersona { get; set; }
        public Nullable<decimal> IdEntidad { get; set; }
        public System.DateTime Fecha_OP { get; set; }
        public System.DateTime Fecha_Fa_Prov { get; set; }
        public System.DateTime Fecha_Venc_Fac_Prov { get; set; }
        public string Observacion { get; set; }
        public string Nom_Beneficiario { get; set; }
        public string Girar_Cheque_a { get; set; }
        public double Valor_a_pagar { get; set; }
        public double Valor_estimado_a_pagar_OP { get; set; }
        public double Total_cancelado_OP { get; set; }
        public double Saldo_x_Pagar_OP { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public string IdFormaPago { get; set; }
        public Nullable<System.DateTime> Fecha_Pago { get; set; }
        public string IdCtaCble { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdSubCentro_Costo { get; set; }
        public Nullable<decimal> Cbte_cxp { get; set; }
        public string Estado { get; set; }
        public string Nom_Beneficiario_2 { get; set; }
        public Nullable<int> IdEmpresa_cxp { get; set; }
        public Nullable<int> IdTipoCbte_cxp { get; set; }
        public Nullable<decimal> IdCbteCble_cxp { get; set; }
        public Nullable<int> IdBanco { get; set; }
    }
}
