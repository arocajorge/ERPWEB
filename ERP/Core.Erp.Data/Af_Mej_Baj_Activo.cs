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
    
    public partial class Af_Mej_Baj_Activo
    {
        public int IdEmpresa { get; set; }
        public decimal Id_Mejora_Baja_Activo { get; set; }
        public string Id_Tipo { get; set; }
        public int IdActivoFijo { get; set; }
        public string Cod_Mej_Baj_Activo { get; set; }
        public double ValorActivo { get; set; }
        public double Valor_Tot_Bajas { get; set; }
        public double Valor_Tot_Mejora { get; set; }
        public double Valor_Depre_Acu { get; set; }
        public double Valor_Neto { get; set; }
        public double Valor_Mej_Baj_Activo { get; set; }
        public string Compr_Mej_Baj { get; set; }
        public string DescripcionTecnica { get; set; }
        public string Motivo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnula { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IdEmpresa_ct { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public System.DateTime Fecha_MejBaj { get; set; }
    
        public virtual Af_Activo_fijo Af_Activo_fijo { get; set; }
    }
}
