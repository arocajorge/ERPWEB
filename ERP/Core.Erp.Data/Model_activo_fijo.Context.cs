﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities_activo_fijo : DbContext
    {
        public Entities_activo_fijo()
            : base("name=Entities_activo_fijo")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Af_Activo_fijo_Categoria> Af_Activo_fijo_Categoria { get; set; }
        public virtual DbSet<Af_CatalogoTipo> Af_CatalogoTipo { get; set; }
        public virtual DbSet<Af_Parametros> Af_Parametros { get; set; }
        public virtual DbSet<Af_Catalogo> Af_Catalogo { get; set; }
        public virtual DbSet<Af_Activo_fijo> Af_Activo_fijo { get; set; }
        public virtual DbSet<Af_Activo_fijo_tipo> Af_Activo_fijo_tipo { get; set; }
        public virtual DbSet<Af_Venta_Activo> Af_Venta_Activo { get; set; }
        public virtual DbSet<Af_Retiro_Activo> Af_Retiro_Activo { get; set; }
        public virtual DbSet<Af_Mej_Baj_Activo> Af_Mej_Baj_Activo { get; set; }
        public virtual DbSet<Af_Depreciacion> Af_Depreciacion { get; set; }
        public virtual DbSet<Af_Depreciacion_Det> Af_Depreciacion_Det { get; set; }
    
        public virtual ObjectResult<spACTF_activos_a_depreciar_Result> spACTF_activos_a_depreciar(Nullable<int> idEmpresa, Nullable<System.DateTime> fecha_ini, Nullable<System.DateTime> fecha_fin, string idUsuario)
        {
            var idEmpresaParameter = idEmpresa.HasValue ?
                new ObjectParameter("IdEmpresa", idEmpresa) :
                new ObjectParameter("IdEmpresa", typeof(int));
    
            var fecha_iniParameter = fecha_ini.HasValue ?
                new ObjectParameter("Fecha_ini", fecha_ini) :
                new ObjectParameter("Fecha_ini", typeof(System.DateTime));
    
            var fecha_finParameter = fecha_fin.HasValue ?
                new ObjectParameter("Fecha_fin", fecha_fin) :
                new ObjectParameter("Fecha_fin", typeof(System.DateTime));
    
            var idUsuarioParameter = idUsuario != null ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spACTF_activos_a_depreciar_Result>("spACTF_activos_a_depreciar", idEmpresaParameter, fecha_iniParameter, fecha_finParameter, idUsuarioParameter);
        }
    }
}
