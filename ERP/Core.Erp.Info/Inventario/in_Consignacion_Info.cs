﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.Inventario
{
    public class in_Consignacion_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdConsignacion { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal IdProveedor { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public decimal IdNumMovi { get; set; }

        #region Campos de la vista
        public string Su_Descripcion { get; set; }
        public string bo_Descripcion { get; set; }
        public string NombreTipoMovimiento { get; set; }
        public string NombreProveedor { get; set; }
        #endregion
    }
}
