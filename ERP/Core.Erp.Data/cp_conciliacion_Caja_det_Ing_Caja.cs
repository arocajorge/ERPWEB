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
    
    public partial class cp_conciliacion_Caja_det_Ing_Caja
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion_Caja { get; set; }
        public int secuencia { get; set; }
        public int IdEmpresa_movcaj { get; set; }
        public decimal IdCbteCble_movcaj { get; set; }
        public int IdTipocbte_movcaj { get; set; }
        public double valor_aplicado { get; set; }
        public double valor_disponible { get; set; }
    
        public virtual cp_conciliacion_Caja cp_conciliacion_Caja { get; set; }
        public virtual caj_Caja_Movimiento caj_Caja_Movimiento { get; set; }
    }
}
