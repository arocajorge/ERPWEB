﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities_compras : DbContext
    {
        public Entities_compras()
            : base("name=Entities_compras")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<com_catalogo> com_catalogo { get; set; }
        public DbSet<com_catalogo_tipo> com_catalogo_tipo { get; set; }
        public DbSet<com_estado_cierre> com_estado_cierre { get; set; }
        public DbSet<com_departamento> com_departamento { get; set; }
        public DbSet<com_TerminoPago> com_TerminoPago { get; set; }
        public DbSet<com_Motivo_Orden_Compra> com_Motivo_Orden_Compra { get; set; }
        public DbSet<com_comprador> com_comprador { get; set; }
        public DbSet<com_ordencompra_local_det> com_ordencompra_local_det { get; set; }
        public DbSet<com_ordencompra_local> com_ordencompra_local { get; set; }
        public DbSet<com_parametro> com_parametro { get; set; }
        public DbSet<vwcom_ordencompra_local_det> vwcom_ordencompra_local_det { get; set; }
    }
}
