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
    
    public partial class VWCOMP_001
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdOrdenCompra { get; set; }
        public int Secuencia { get; set; }
        public string Tipo { get; set; }
        public int SecuenciaTipo { get; set; }
        public decimal IdProducto { get; set; }
        public string Su_Descripcion { get; set; }
        public System.DateTime oc_fecha { get; set; }
        public string oc_observacion { get; set; }
        public string Estado { get; set; }
        public string NombreTerminoPago { get; set; }
        public string oc_plazo { get; set; }
        public decimal IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string TelefonosProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public string RucProveedor { get; set; }
        public string NombreComprador { get; set; }
        public string NombreProducto { get; set; }
        public string NomUnidadMedida { get; set; }
        public double do_Cantidad { get; set; }
        public double do_precioCompra { get; set; }
        public double do_porc_des { get; set; }
        public double do_descuento { get; set; }
        public double do_precioFinal { get; set; }
        public double do_subtotal { get; set; }
        public double do_iva { get; set; }
        public double do_total { get; set; }
        public double Por_Iva { get; set; }
        public double SubtotalIVA { get; set; }
        public double Subtotal0 { get; set; }
        public double DescuentoTotal { get; set; }
    }
}
