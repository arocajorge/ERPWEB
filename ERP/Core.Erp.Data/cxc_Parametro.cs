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
    
    public partial class cxc_Parametro
    {
        public int IdEmpresa { get; set; }
        public int pa_IdCaja_x_cobros_x_CXC { get; set; }
        public int pa_IdTipoMoviCaja_x_Cobros_x_cliente { get; set; }
        public int pa_IdTipoCbteCble_CxC { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaTransac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string IdCtaCble_ProvisionFuente { get; set; }
        public string IdCtaCble_ProvisionIva { get; set; }
        public Nullable<int> IdPunto_cargo_grupo_Fte { get; set; }
        public Nullable<int> IdPunto_cargo_Fte { get; set; }
        public Nullable<int> IdPunto_cargo_grupo_Iva { get; set; }
        public Nullable<int> IdPunto_cargo_Iva { get; set; }
        public Nullable<int> IdTipoCbte_LiquidacionRet { get; set; }
    }
}
