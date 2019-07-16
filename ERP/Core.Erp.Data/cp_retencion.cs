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
    
    public partial class cp_retencion
    {
        public cp_retencion()
        {
            this.cp_retencion_det = new HashSet<cp_retencion_det>();
            this.cp_retencion_x_ct_cbtecble = new HashSet<cp_retencion_x_ct_cbtecble>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdRetencion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdPuntoVta { get; set; }
        public string CodDocumentoTipo { get; set; }
        public string serie1 { get; set; }
        public string serie2 { get; set; }
        public string NumRetencion { get; set; }
        public string NAutorizacion { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }
        public System.DateTime fecha { get; set; }
        public string observacion { get; set; }
        public Nullable<int> IdEmpresa_Ogiro { get; set; }
        public Nullable<decimal> IdCbteCble_Ogiro { get; set; }
        public Nullable<int> IdTipoCbte_Ogiro { get; set; }
        public string Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool aprobada_enviar_sri { get; set; }
        public Nullable<bool> Generado { get; set; }
    
        public virtual ICollection<cp_retencion_det> cp_retencion_det { get; set; }
        public virtual ICollection<cp_retencion_x_ct_cbtecble> cp_retencion_x_ct_cbtecble { get; set; }
        public virtual cp_orden_giro cp_orden_giro { get; set; }
    }
}
