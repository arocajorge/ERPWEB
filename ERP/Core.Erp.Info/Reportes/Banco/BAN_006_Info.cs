﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Reportes.Banco
{
    public class BAN_006_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public string cb_giradoA { get; set; }
        public string ValorEnLetras { get; set; }
        public string Descripcion_Ciudad { get; set; }
        public double cb_Valor { get; set; }
        public System.DateTime cb_Fecha { get; set; }
        public string cb_Observacion { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string cb_Cheque { get; set; }
        public Nullable<decimal> cb_Cheque_numero { get; set; }
        public int IdBanco { get; set; }
    }
}
