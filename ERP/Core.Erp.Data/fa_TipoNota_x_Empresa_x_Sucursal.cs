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
    using System.Collections.Generic;
    
    public partial class fa_TipoNota_x_Empresa_x_Sucursal
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTipoNota { get; set; }
        public string IdCtaCble { get; set; }
    
        public virtual fa_TipoNota fa_TipoNota { get; set; }
    }
}
