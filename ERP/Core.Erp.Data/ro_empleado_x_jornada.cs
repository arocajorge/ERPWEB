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
    
    public partial class ro_empleado_x_jornada
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public int Secuencia { get; set; }
        public int IdJornada { get; set; }
        public double ValorHora { get; set; }
        public int MaxNumHoras { get; set; }
    
        public virtual ro_jornada ro_jornada { get; set; }
        public virtual ro_empleado ro_empleado { get; set; }
    }
}
