﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Info.General
{
    public class tb_sucursal_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string codigo { get; set; }
        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        [StringLength(120, MinimumLength = 1, ErrorMessage = "El campo descripción debe tener mínimo 1 caracter y máximo 120")]
        public string Su_Descripcion { get; set; }
        [StringLength(30, MinimumLength = 0, ErrorMessage = "El campo establecimiento debe tener mínimo 0 caracter y máximo 30")]
        public string Su_CodigoEstablecimiento { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessage = "El campo ubicación debe tener mínimo 0 caracter y máximo 20")]
        public string Su_Ubicacion { get; set; }
        [StringLength(15, MinimumLength = 0, ErrorMessage = "El campo ruc debe tener mínimo 0 caracter y máximo 15")]
        public string Su_Ruc { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "El campo ruc debe tener mínimo 0 caracter y máximo 100")]
        public string Su_JefeSucursal { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "El campo telefono debe tener mínimo 0 caracter y máximo 50")]
        public string Su_Telefonos { get; set; }
        [StringLength(100, MinimumLength = 0, ErrorMessage = "El campo dirección debe tener mínimo 0 caracter y máximo 100")]
        public string Su_Direccion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string nom_pc { get; set; }
        public string ip { get; set; }
        public string Estado { get; set; }
        public bool EstadoBool { get; set; }
        public string MotiAnula { get; set; }
        public bool Es_establecimiento { get; set; }
    }
}
