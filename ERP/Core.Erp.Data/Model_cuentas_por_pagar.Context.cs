﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class Entities_cuentas_por_pagar : DbContext
    {
        public Entities_cuentas_por_pagar()
            : base("name=Entities_cuentas_por_pagar")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<cp_codigo_SRI> cp_codigo_SRI { get; set; }
        public DbSet<cp_codigo_SRI_tipo> cp_codigo_SRI_tipo { get; set; }
        public DbSet<cp_codigo_SRI_x_CtaCble> cp_codigo_SRI_x_CtaCble { get; set; }
        public DbSet<cp_cuotas_x_doc> cp_cuotas_x_doc { get; set; }
        public DbSet<cp_cuotas_x_doc_det> cp_cuotas_x_doc_det { get; set; }
        public DbSet<cp_orden_giro_pagos_sri> cp_orden_giro_pagos_sri { get; set; }
        public DbSet<cp_orden_pago_cancelaciones> cp_orden_pago_cancelaciones { get; set; }
        public DbSet<cp_orden_pago_estado_aprob> cp_orden_pago_estado_aprob { get; set; }
        public DbSet<cp_orden_pago_formapago> cp_orden_pago_formapago { get; set; }
        public DbSet<cp_orden_pago_tipo> cp_orden_pago_tipo { get; set; }
        public DbSet<cp_pagos_sri> cp_pagos_sri { get; set; }
        public DbSet<cp_pais_sri> cp_pais_sri { get; set; }
        public DbSet<cp_retencion_det> cp_retencion_det { get; set; }
        public DbSet<cp_retencion_x_ct_cbtecble> cp_retencion_x_ct_cbtecble { get; set; }
        public DbSet<cp_TipoDocumento> cp_TipoDocumento { get; set; }
        public DbSet<vwcp_codigo_SRI> vwcp_codigo_SRI { get; set; }
        public DbSet<vwcp_proveedor_consulta> vwcp_proveedor_consulta { get; set; }
        public DbSet<cp_proveedor_clase> cp_proveedor_clase { get; set; }
        public DbSet<cp_orden_giro_x_in_Ing_Egr_Inven> cp_orden_giro_x_in_Ing_Egr_Inven { get; set; }
        public DbSet<cp_parametros> cp_parametros { get; set; }
        public DbSet<vwcp_retencion_det> vwcp_retencion_det { get; set; }
        public DbSet<cp_orden_giro_det> cp_orden_giro_det { get; set; }
        public DbSet<vwcp_orden_giro_det> vwcp_orden_giro_det { get; set; }
        public DbSet<cp_SolicitudPago> cp_SolicitudPago { get; set; }
        public DbSet<cp_orden_pago_det> cp_orden_pago_det { get; set; }
        public DbSet<cp_SolicitudPagoDet> cp_SolicitudPagoDet { get; set; }
        public DbSet<vwcp_SolicitudPago> vwcp_SolicitudPago { get; set; }
        public DbSet<cp_orden_pago> cp_orden_pago { get; set; }
        public DbSet<vwcp_orden_giro_x_pagar> vwcp_orden_giro_x_pagar { get; set; }
        public DbSet<vwcp_orden_pago> vwcp_orden_pago { get; set; }
        public DbSet<vwcp_orden_giro> vwcp_orden_giro { get; set; }
        public DbSet<vwcp_orden_giro_det_ing_x_oc_x_cruzar> vwcp_orden_giro_det_ing_x_oc_x_cruzar { get; set; }
        public DbSet<cp_orden_giro_det_ing_x_oc> cp_orden_giro_det_ing_x_oc { get; set; }
        public DbSet<vwcp_orden_giro_det_ing_x_oc> vwcp_orden_giro_det_ing_x_oc { get; set; }
        public DbSet<cp_orden_pago_tipo_x_empresa> cp_orden_pago_tipo_x_empresa { get; set; }
        public DbSet<vwcp_nota_DebCre> vwcp_nota_DebCre { get; set; }
        public DbSet<cp_proveedor> cp_proveedor { get; set; }
        public DbSet<cp_retencion> cp_retencion { get; set; }
        public DbSet<vwcp_retencion> vwcp_retencion { get; set; }
        public DbSet<cp_nota_DebCre> cp_nota_DebCre { get; set; }
        public DbSet<cp_orden_giro> cp_orden_giro { get; set; }
        public DbSet<cp_orden_giro_det_ing_x_os> cp_orden_giro_det_ing_x_os { get; set; }
        public DbSet<vwcp_orden_giro_det_ing_x_os_x_cruzar> vwcp_orden_giro_det_ing_x_os_x_cruzar { get; set; }
        public DbSet<vwcp_orden_giro_det_ing_x_os> vwcp_orden_giro_det_ing_x_os { get; set; }
        public DbSet<vwcp_orden_giro_SinRetencion> vwcp_orden_giro_SinRetencion { get; set; }
        public DbSet<cp_ConciliacionAnticipo> cp_ConciliacionAnticipo { get; set; }
        public DbSet<cp_ConciliacionAnticipoDetAnt> cp_ConciliacionAnticipoDetAnt { get; set; }
        public DbSet<cp_ConciliacionAnticipoDetCXP> cp_ConciliacionAnticipoDetCXP { get; set; }
        public DbSet<vwcp_ConciliacionAnticipo> vwcp_ConciliacionAnticipo { get; set; }
        public DbSet<vwcp_ConciliacionAnticipoDetAnt_x_cruzar> vwcp_ConciliacionAnticipoDetAnt_x_cruzar { get; set; }
    
        public virtual ObjectResult<spcp_Get_Data_orden_pago_con_cancelacion_x_pago_Result> spcp_Get_Data_orden_pago_con_cancelacion_x_pago(Nullable<int> idEmpresa_pago, Nullable<int> idTipoCbte_pago, Nullable<decimal> idCbteCble_pago, string idUsuario)
        {
            var idEmpresa_pagoParameter = idEmpresa_pago.HasValue ?
                new ObjectParameter("IdEmpresa_pago", idEmpresa_pago) :
                new ObjectParameter("IdEmpresa_pago", typeof(int));
    
            var idTipoCbte_pagoParameter = idTipoCbte_pago.HasValue ?
                new ObjectParameter("IdTipoCbte_pago", idTipoCbte_pago) :
                new ObjectParameter("IdTipoCbte_pago", typeof(int));
    
            var idCbteCble_pagoParameter = idCbteCble_pago.HasValue ?
                new ObjectParameter("IdCbteCble_pago", idCbteCble_pago) :
                new ObjectParameter("IdCbteCble_pago", typeof(decimal));
    
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spcp_Get_Data_orden_pago_con_cancelacion_x_pago_Result>("spcp_Get_Data_orden_pago_con_cancelacion_x_pago", idEmpresa_pagoParameter, idTipoCbte_pagoParameter, idCbteCble_pagoParameter, idUsuarioParameter);
        }
    
        public virtual ObjectResult<spcp_Get_Data_orden_pago_con_cancelacion_data_Result> spcp_Get_Data_orden_pago_con_cancelacion_data(Nullable<int> idEmpresa, Nullable<decimal> idPersona_ini, Nullable<decimal> idPersona_fin, string idTipoPersona, Nullable<decimal> idEntidad_ini, Nullable<decimal> idEntidad_fin, string idEstado_Aprobacion, string idUsuario, Nullable<int> idSucursal, Nullable<bool> mostrar_saldo_0, Nullable<bool> validarCuentaBancaria)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var idPersona_iniParameter = idPersona_ini.HasValue ?
                new ObjectParameter("IdPersona_ini", idPersona_ini) :
                new ObjectParameter("IdPersona_ini", typeof(decimal));
    
            var idPersona_finParameter = idPersona_fin.HasValue ?
                new ObjectParameter("IdPersona_fin", idPersona_fin) :
                new ObjectParameter("IdPersona_fin", typeof(decimal));
    
            var idTipoPersonaParameter = idTipoPersona != null ?
                new ObjectParameter("IdTipoPersona", idTipoPersona) :
                new ObjectParameter("IdTipoPersona", typeof(string));
    
            var idEntidad_iniParameter = idEntidad_ini.HasValue ?
                new ObjectParameter("IdEntidad_ini", idEntidad_ini) :
                new ObjectParameter("IdEntidad_ini", typeof(decimal));
    
            var idEntidad_finParameter = idEntidad_fin.HasValue ?
                new ObjectParameter("IdEntidad_fin", idEntidad_fin) :
                new ObjectParameter("IdEntidad_fin", typeof(decimal));
    
            var idEstado_AprobacionParameter = idEstado_Aprobacion != null ?
                new ObjectParameter("IdEstado_Aprobacion", idEstado_Aprobacion) :
                new ObjectParameter("IdEstado_Aprobacion", typeof(string));
    
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));
    
            var idSucursalParameter = idSucursal.HasValue ?
                new ObjectParameter("IdSucursal", idSucursal) :
                new ObjectParameter("IdSucursal", typeof(int));
    
            var mostrar_saldo_0Parameter = mostrar_saldo_0.HasValue ?
                new ObjectParameter("mostrar_saldo_0", mostrar_saldo_0) :
                new ObjectParameter("mostrar_saldo_0", typeof(bool));
    
            var validarCuentaBancariaParameter = validarCuentaBancaria.HasValue ?
                new ObjectParameter("ValidarCuentaBancaria", validarCuentaBancaria) :
                new ObjectParameter("ValidarCuentaBancaria", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spcp_Get_Data_orden_pago_con_cancelacion_data_Result>("spcp_Get_Data_orden_pago_con_cancelacion_data", idEmpresaParameter, idPersona_iniParameter, idPersona_finParameter, idTipoPersonaParameter, idEntidad_iniParameter, idEntidad_finParameter, idEstado_AprobacionParameter, idUsuarioParameter, idSucursalParameter, mostrar_saldo_0Parameter, validarCuentaBancariaParameter);
        }
    }
}
