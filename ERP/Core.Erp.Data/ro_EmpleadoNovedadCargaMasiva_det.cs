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
    
    public partial class ro_EmpleadoNovedadCargaMasiva_det
    {
        public int IdEmpresa { get; set; }
        public decimal IdCarga { get; set; }
        public int Secuencia { get; set; }
        public int IdEmpresa_nov { get; set; }
        public decimal IdNovedad { get; set; }
        public string Observacion { get; set; }
        public decimal IdEmpleado { get; set; }
    
        public virtual ro_EmpleadoNovedadCargaMasiva ro_EmpleadoNovedadCargaMasiva { get; set; }
        public virtual ro_empleado_Novedad ro_empleado_Novedad { get; set; }
        public virtual ro_empleado ro_empleado { get; set; }
    }
}
