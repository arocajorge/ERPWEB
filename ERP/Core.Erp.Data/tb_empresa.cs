//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_empresa
    {
        public tb_empresa()
        {
            this.tb_sis_Documento_Tipo_x_Empresa = new HashSet<tb_sis_Documento_Tipo_x_Empresa>();
            this.tb_sis_reporte_x_seg_usuario = new HashSet<tb_sis_reporte_x_seg_usuario>();
            this.tb_sucursal = new HashSet<tb_sucursal>();
            this.tb_sis_reporte_x_tb_empresa = new HashSet<tb_sis_reporte_x_tb_empresa>();
        }
    
        public int IdEmpresa { get; set; }
        public string codigo { get; set; }
        public string em_nombre { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string ContribuyenteEspecial { get; set; }
        public string em_ruc { get; set; }
        public string em_gerente { get; set; }
        public string em_contador { get; set; }
        public string em_rucContador { get; set; }
        public string em_telefonos { get; set; }
        public string em_direccion { get; set; }
        public byte[] em_logo { get; set; }
        public System.DateTime em_fechaInicioContable { get; set; }
        public string Estado { get; set; }
        public System.DateTime em_fechaInicioActividad { get; set; }
        public string cod_entidad_dinardap { get; set; }
        public string em_Email { get; set; }
    
        public virtual ICollection<tb_sis_Documento_Tipo_x_Empresa> tb_sis_Documento_Tipo_x_Empresa { get; set; }
        public virtual ICollection<tb_sis_reporte_x_seg_usuario> tb_sis_reporte_x_seg_usuario { get; set; }
        public virtual ICollection<tb_sucursal> tb_sucursal { get; set; }
        public virtual ICollection<tb_sis_reporte_x_tb_empresa> tb_sis_reporte_x_tb_empresa { get; set; }
    }
}
