﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Contabilidad;
namespace Core.Erp.Info.CuentasPorPagar
{
  public  class cp_retencion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdProveedor { get; set; }
        public decimal IdRetencion { get; set; }
        public string CodDocumentoTipo { get; set; }
        [StringLength(3, MinimumLength = 1, ErrorMessage = "el campo serie1 debe tener 3 caracteres")]
        public string serie1 { get; set; }
        [StringLength(3, MinimumLength = 1, ErrorMessage = "el campo serie2 debe tener 3 caracteres")]
        public string serie2 { get; set; }
        [StringLength(20, MinimumLength = 1, ErrorMessage = "el campo número de retención debe tener mínimo 1 caracter y máximo 20")]

        public string NumRetencion { get; set; }
        public string NAutorizacion { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime fecha { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo Observación debe tener mínimo 1 caracter y máximo 500")]
        public string observacion { get; set; }
        public string re_Tiene_RTiva { get; set; }
        public string re_Tiene_RFuente { get; set; }
        public Nullable<int> IdEmpresa_Ogiro { get; set; }
        public Nullable<decimal> IdCbteCble_Ogiro { get; set; }
        public Nullable<int> IdTipoCbte_Ogiro { get; set; }
        public Nullable<int> ct_IdEmpresa_Anu { get; set; }
        public Nullable<int> ct_IdTipoCbte_Anu { get; set; }
        public Nullable<decimal> ct_IdCbteCble_Anu { get; set; }
        public string re_EstaImpresa { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string IdUsuario { get; set; }
        public System.DateTime Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool aprobada_enviar_sri { get; set; }




        public double co_subtotal_iva { get; set; }
        public double co_subtotal_siniva { get; set; }
        public double co_baseImponible { get; set; }
        public double co_valoriva { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_razonSocial { get; set; }
        public int? ct_IdTipoCbte { get; set; }
        public decimal? ct_IdCbteCble { get; set; }
        public string co_serie { get; set; }
        public string co_factura { get; set; }
        public string Descripcion { get; set; }

        public List<cp_retencion_det_Info> detalle { get; set; }
        public ct_cbtecble_Info info_comprobante { get; set; }
        public string pe_nombreCompleto { get; set; }

        public cp_retencion_Info()
        {
            detalle = new List<cp_retencion_det_Info>();
            info_comprobante = new ct_cbtecble_Info();

        }

    }
}
