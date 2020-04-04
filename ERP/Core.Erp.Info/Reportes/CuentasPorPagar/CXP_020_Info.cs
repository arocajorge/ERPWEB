﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.CuentasPorPagar
{
    public class CXP_020_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public string NomDocumento { get; set; }
        public string co_serie { get; set; }
        public string co_factura { get; set; }
        public string Num_Autorizacion { get; set; }
        public Nullable<System.DateTime> fecha_autorizacion { get; set; }
        public string Su_Descripcion { get; set; }
        public string Su_Direccion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public System.DateTime co_FechaFactura { get; set; }
        public string co_observacion { get; set; }
        public string IdFormaPago { get; set; }
        public string nom_FormaPago { get; set; }
        public double co_subtotal_iva { get; set; }
        public double co_subtotal_siniva { get; set; }
        public double co_subtotal { get; set; }
        public double co_total { get; set; }
        public double co_valoriva { get; set; }
        public string pr_descripcion { get; set; }
        public string pr_codigo { get; set; }
        public double Subtotal { get; set; }
        public double Descuento { get; set; }
        public double ValorIva { get; set; }
        public double TotalDetalle { get; set; }
        public string pr_direccion { get; set; }
        public string pr_correo { get; set; }
        public double Cantidad { get; set; }
        public double CostoUni { get; set; }
    }
}
