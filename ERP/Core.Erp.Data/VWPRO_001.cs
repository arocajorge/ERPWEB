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
    
    public partial class VWPRO_001
    {
        public int IdEmpresa { get; set; }
        public decimal IdFabricacion { get; set; }
        public int Secuencia { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string in_Su_Descripcion { get; set; }
        public string in_bo_Descripcion { get; set; }
        public string eg_Su_Descripcion { get; set; }
        public string eg_bo_Descripcion { get; set; }
        public string in_NombreTipo { get; set; }
        public string eg_NombreTipo { get; set; }
        public Nullable<decimal> egr_IdNumMovi { get; set; }
        public Nullable<decimal> ing_IdNumMovi { get; set; }
        public string Signo { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public bool RealizaMovimiento { get; set; }
        public string pr_descripcion { get; set; }
        public string NombreUnidad { get; set; }
    }
}