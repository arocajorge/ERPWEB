﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
   public class ro_NominasPagosCheques_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdTransaccion { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public Nullable<decimal> IdEmpleado { get; set; }
        public string Observacion { get; set; }
        public double Valor { get; set; }
        public int IdEmpresa_op { get; set; }
        public decimal IdOrdenPago { get; set; }


        #region camp. vista
        public double? ValorCancelado { get; set; }
        public string em_codigo { get; set; }
        public string em_tipoCta { get; set; }
        public string em_NumCta { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string pe_cedulaRuc { get; set; }
        public double Saldo { get; set; }
        public string pe_nombreCompleto { get; set; }


        #endregion

    }
}
