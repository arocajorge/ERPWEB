﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_parametros_Info
    {
        public int IdEmpresa { get; set; }
        public Nullable<int> pa_TipoCbte_OG { get; set; }
        public Nullable<int> pa_TipoCbte_OG_anulacion { get; set; }
        public string pa_ctacble_deudora { get; set; }
        public string pa_ctacble_iva { get; set; }
        public string pa_ctacble_Proveedores_default { get; set; }
        public Nullable<int> pa_IdTipoCbte_x_Retencion { get; set; }
        public Nullable<int> pa_IdTipoCbte_x_Anu_Retencion { get; set; }
        public Nullable<int> IdTipoMoviCaja { get; set; }
        public Nullable<int> pa_TipoEgrMoviCaja_Conciliacion { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public Nullable<int> pa_TipoCbte_NC { get; set; }
        public Nullable<int> pa_TipoCbte_NC_anulacion { get; set; }
        public string pa_obligaOC { get; set; }
        public Nullable<int> pa_TipoCbte_ND { get; set; }
        public Nullable<int> pa_TipoCbte_ND_anulacion { get; set; }
        public byte[] pa_xsd_de_atsSRI { get; set; }
        public byte[] pa_Formulario103_104 { get; set; }
        public Nullable<int> pa_TipoCbte_para_conci_anulacion_x_antcipo { get; set; }
        public Nullable<int> pa_TipoCbte_para_conci_x_antcipo { get; set; }
        public string pa_ruta_descarga_xml_fac_elct { get; set; }
        public string pa_ctacble_x_RetIva_default { get; set; }
        public string pa_ctacble_x_RetFte_default { get; set; }
        public byte[] archivo_diseño_reporte { get; set; }
        public Nullable<int> pa_IdBancoCuenta_default_para_OP { get; set; }
        public Nullable<bool> pa_X_Defecto_la_Retencion_es_cbte_elect { get; set; }

        public string descripcion { get; set; }
    }
}
