﻿using System;

namespace Core.Erp.Info.Reportes.Facturacion
{
    public class FAC_005_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCliente { get; set; }
        public string NomCliente { get; set; }
        public string TipoDocumento { get; set; }
        public Nullable<bool> EsExportacion { get; set; }
        public string Su_Descripcion { get; set; }
        public string Su_CodigoEstablecimiento { get; set; }
        public Nullable<decimal> SubtotalIVASinDscto { get; set; }
        public Nullable<decimal> SubtotalSinIVASinDscto { get; set; }
        public Nullable<decimal> SubtotalSinDscto { get; set; }
        public Nullable<decimal> Descuento { get; set; }
        public Nullable<decimal> SubtotalIVAConDscto { get; set; }
        public Nullable<decimal> SubtotalSinIVAConDscto { get; set; }
        public Nullable<decimal> SubtotalConDscto { get; set; }
        public Nullable<decimal> ValorIVA { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> Cantidad { get; set; }
    }
}
