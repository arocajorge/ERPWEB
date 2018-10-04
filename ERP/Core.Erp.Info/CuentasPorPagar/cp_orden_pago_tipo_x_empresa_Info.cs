﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.CuentasPorPagar
{
   public class cp_orden_pago_tipo_x_empresa_Info
    {
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = ("el campo código es obligatorio"))]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "el campo código debe tener mínimo 3 caracter y máximo 20")]

        public string IdTipo_op { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable debito es obligatorio"))]

        public string IdCtaCble { get; set; }
        public string IdCentroCosto { get; set; }
        [Required(ErrorMessage = ("el campotipo comprobante es obligatorio es obligatorio"))]

        public Nullable<int> IdTipoCbte_OP { get; set; }
        [Required(ErrorMessage = ("el campotipo comprobante anulación es obligatorio es obligatorio"))]

        public Nullable<int> IdTipoCbte_OP_anulacion { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public string Buscar_FactxPagar { get; set; }
        [Required(ErrorMessage = ("el campo cuenta contable credito es obligatorio"))]
        public string IdCtaCble_Credito { get; set; }
        public Nullable<bool> Dispara_Alerta { get; set; }
        [Required(ErrorMessage = ("el campo descripción es obligatorio"))]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "el campo descripción debe tener mínimo 5 caracter y máximo 500")]
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string GeneraDiario { get; set; }

    }
}
