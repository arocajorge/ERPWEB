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
    
    public partial class ro_catalogo
    {
        public ro_catalogo()
        {
            this.ro_cargaFamiliar = new HashSet<ro_cargaFamiliar>();
            this.ro_empleado = new HashSet<ro_empleado>();
            this.ro_empleado1 = new HashSet<ro_empleado>();
            this.ro_empleado2 = new HashSet<ro_empleado>();
            this.ro_empleado3 = new HashSet<ro_empleado>();
            this.ro_marcaciones_x_empleado = new HashSet<ro_marcaciones_x_empleado>();
        }
    
        public string CodCatalogo { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTipoCatalogo { get; set; }
        public string ca_descripcion { get; set; }
        public string ca_estado { get; set; }
        public int ca_orden { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual ICollection<ro_cargaFamiliar> ro_cargaFamiliar { get; set; }
        public virtual ro_catalogoTipo ro_catalogoTipo { get; set; }
        public virtual ICollection<ro_empleado> ro_empleado { get; set; }
        public virtual ICollection<ro_empleado> ro_empleado1 { get; set; }
        public virtual ICollection<ro_empleado> ro_empleado2 { get; set; }
        public virtual ICollection<ro_empleado> ro_empleado3 { get; set; }
        public virtual ICollection<ro_marcaciones_x_empleado> ro_marcaciones_x_empleado { get; set; }
    }
}
