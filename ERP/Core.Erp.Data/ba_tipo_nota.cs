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
    
    public partial class ba_tipo_nota
    {
        public ba_tipo_nota()
        {
            this.ba_Cbte_Ban = new HashSet<ba_Cbte_Ban>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdTipoNota { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string IdCtaCble { get; set; }
        public string IdCentroCosto { get; set; }
        public string Estado { get; set; }
    
        public virtual ICollection<ba_Cbte_Ban> ba_Cbte_Ban { get; set; }
    }
}
