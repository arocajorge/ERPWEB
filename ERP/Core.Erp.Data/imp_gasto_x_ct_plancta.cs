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
    
    public partial class imp_gasto_x_ct_plancta
    {
        public int IdGasto_tipo { get; set; }
        public int IdEmpresa { get; set; }
        public string IdCtaCble { get; set; }
    
        public virtual imp_gasto imp_gasto { get; set; }
    }
}
