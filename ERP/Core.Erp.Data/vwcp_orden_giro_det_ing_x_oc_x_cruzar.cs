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
    
    public partial class vwcp_orden_giro_det_ing_x_oc_x_cruzar
    {
        public int inv_IdEmpresa { get; set; }
        public int inv_IdSucursal { get; set; }
        public int inv_IdMovi_inven_tipo { get; set; }
        public decimal inv_IdNumMovi { get; set; }
        public int inv_Secuencia { get; set; }
        public Nullable<int> oc_IdSucursal { get; set; }
        public Nullable<decimal> oc_IdOrdenCompra { get; set; }
        public Nullable<int> oc_Secuencia { get; set; }
        public string pr_descripcion { get; set; }
        public string IdCtaCtble_Inve { get; set; }
        public double dm_cantidad_sinConversion { get; set; }
        public double do_precioCompra { get; set; }
        public double do_porc_des { get; set; }
        public double do_descuento { get; set; }
        public double do_precioFinal { get; set; }
        public double do_subtotal { get; set; }
        public double do_iva { get; set; }
        public double do_total { get; set; }
        public string IdUnidadMedida { get; set; }
        public double Por_Iva { get; set; }
        public string IdCod_Impuesto { get; set; }
        public string NomUnidadMedida { get; set; }
        public decimal IdProveedor { get; set; }
        public decimal IdProducto { get; set; }
        public string pc_Cuenta { get; set; }
        public int SecuenciaTipo { get; set; }
    }
}
