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
    
    public partial class vwba_Archivo_Transferencia_Det
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public string Id_Item { get; set; }
        public Nullable<int> IdEmpresa_OP { get; set; }
        public Nullable<decimal> IdOrdenPago { get; set; }
        public Nullable<int> Secuencia_OP { get; set; }
        public bool Estado { get; set; }
        public double Valor { get; set; }
        public decimal Secuencial_reg_x_proceso { get; set; }
        public Nullable<bool> Contabilizado { get; set; }
        public Nullable<System.DateTime> Fecha_proceso { get; set; }
        public string IdTipoCta_acreditacion_cat { get; set; }
        public string num_cta_acreditacion { get; set; }
        public Nullable<int> IdBanco_acreditacion { get; set; }
        public string pr_direccion { get; set; }
        public string pr_correo { get; set; }
        public string IdTipoDocumento { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
    }
}
