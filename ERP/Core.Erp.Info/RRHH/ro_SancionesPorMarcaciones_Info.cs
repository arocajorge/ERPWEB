﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.RRHH
{
  public  class ro_SancionesPorMarcaciones_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAjuste { get; set; }
        public int IdNomina_Tipo { get; set; }
        public int IdNomina_TipoLiqui { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FecaFin { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<bool> IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }

    }
}
