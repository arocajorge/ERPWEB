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
    
    public partial class vwct_CierrePorModuloPorSucursal
    {
        public int IdEmpresa { get; set; }
        public int IdCierre { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public string CodModulo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaIni { get; set; }
        public System.DateTime FechaFin { get; set; }
        public bool Cerrado { get; set; }
    }
}
