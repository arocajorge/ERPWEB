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
    
    public partial class in_parametro
    {
        public int IdEmpresa { get; set; }
        public int IdMovi_inven_tipo_egresoBodegaOrigen { get; set; }
        public int IdMovi_inven_tipo_ingresoBodegaDestino { get; set; }
        public int IdMovi_Inven_tipo_x_Dev_Inv_x_Ing { get; set; }
        public int IdMovi_Inven_tipo_x_Dev_Inv_x_Erg { get; set; }
        public string P_IdCtaCble_transitoria_transf_inven { get; set; }
        public int P_IdMovi_inven_tipo_default_ing { get; set; }
        public int P_IdMovi_inven_tipo_default_egr { get; set; }
        public int P_IdMovi_inven_tipo_ingreso_x_compra { get; set; }
        public int P_Dias_menores_alerta_desde_fecha_actual_rojo { get; set; }
        public int P_Dias_menores_alerta_desde_fecha_actual_amarillo { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public int IdMovi_inven_tipo_Cambio { get; set; }
        public int IdMovi_inven_tipo_Consignacion { get; set; }
        public int IdMovi_inven_tipo_elaboracion_egr { get; set; }
        public int IdMovi_inven_tipo_elaboracion_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_elaboracion_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_elaboracion_egr { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ajuste_ing { get; set; }
        public Nullable<int> IdMovi_inven_tipo_ajuste_egr { get; set; }
        public string IdCatalogoEstadoAjuste { get; set; }
        public Nullable<int> IdMotivo_Inv_ajuste_ing { get; set; }
        public Nullable<int> IdMotivo_Inv_ajuste_egr { get; set; }
    
        public virtual in_Catalogo in_Catalogo { get; set; }
        public virtual in_Catalogo in_Catalogo1 { get; set; }
        public virtual in_Motivo_Inven in_Motivo_Inven { get; set; }
        public virtual in_Motivo_Inven in_Motivo_Inven1 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo1 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo2 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo3 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo4 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo5 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo6 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo7 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo8 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo9 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo10 { get; set; }
        public virtual in_movi_inven_tipo in_movi_inven_tipo11 { get; set; }
    }
}
