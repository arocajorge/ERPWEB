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
    
    public partial class in_Ing_Egr_Inven_det
    {
        public in_Ing_Egr_Inven_det()
        {
            this.in_devolucion_inven_det = new HashSet<in_devolucion_inven_det>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public decimal IdNumMovi { get; set; }
        public int Secuencia { get; set; }
        public int IdBodega { get; set; }
        public decimal IdProducto { get; set; }
        public double dm_cantidad { get; set; }
        public string dm_observacion { get; set; }
        public double mv_costo { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCentroCosto_sub_centro_costo { get; set; }
        public string IdEstadoAproba { get; set; }
        public string IdUnidadMedida { get; set; }
        public Nullable<int> IdEmpresa_oc { get; set; }
        public Nullable<int> IdSucursal_oc { get; set; }
        public Nullable<decimal> IdOrdenCompra { get; set; }
        public Nullable<int> Secuencia_oc { get; set; }
        public Nullable<int> IdPunto_cargo_grupo { get; set; }
        public Nullable<int> IdPunto_cargo { get; set; }
        public Nullable<int> IdEmpresa_inv { get; set; }
        public Nullable<int> IdSucursal_inv { get; set; }
        public Nullable<int> IdBodega_inv { get; set; }
        public Nullable<int> IdMovi_inven_tipo_inv { get; set; }
        public Nullable<decimal> IdNumMovi_inv { get; set; }
        public Nullable<int> secuencia_inv { get; set; }
        public string Motivo_Aprobacion { get; set; }
        public double dm_cantidad_sinConversion { get; set; }
        public string IdUnidadMedida_sinConversion { get; set; }
        public Nullable<double> mv_costo_sinConversion { get; set; }
        public Nullable<int> IdMotivo_Inv { get; set; }
    
        public virtual ICollection<in_devolucion_inven_det> in_devolucion_inven_det { get; set; }
        public virtual in_UnidadMedida in_UnidadMedida { get; set; }
        public virtual in_UnidadMedida in_UnidadMedida1 { get; set; }
        public virtual in_Producto in_Producto { get; set; }
        public virtual in_movi_inve_detalle in_movi_inve_detalle { get; set; }
        public virtual in_Ing_Egr_Inven in_Ing_Egr_Inven { get; set; }
    }
}
