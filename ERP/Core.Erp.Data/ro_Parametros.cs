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
    
    public partial class ro_Parametros
    {
        public int IdEmpresa { get; set; }
        public Nullable<int> IdTipoCbte_AsientoSueldoXPagar { get; set; }
        public Nullable<bool> GeneraOP_PagoPrestamos { get; set; }
        public string IdTipoOP_PagoPrestamos { get; set; }
        public string IdFormaOP_PagoPrestamos { get; set; }
        public Nullable<bool> GeneraOP_LiquidacionVacaciones { get; set; }
        public string IdTipoOP_LiquidacionVacaciones { get; set; }
        public Nullable<int> IdTipoFlujoOP_LiquidacionVacaciones { get; set; }
        public string IdFormaOP_LiquidacionVacaciones { get; set; }
        public Nullable<bool> DescuentaIESS_LiquidacionVacaciones { get; set; }
        public string cta_contable_IESS_Vacaciones { get; set; }
        public Nullable<bool> GeneraOP_ActaFiniquito { get; set; }
        public string IdTipoOP_ActaFiniquito { get; set; }
        public string IdFormaPagoOP_ActaFiniquito { get; set; }
        public double Sueldo_basico { get; set; }
        public double Porcentaje_aporte_pers { get; set; }
        public double Porcentaje_aporte_patr { get; set; }
        public string IdRubro_acta_finiquito { get; set; }
        public bool genera_op_x_pago { get; set; }
        public bool Genera_op_x_pago_x_empleao { get; set; }
    }
}
