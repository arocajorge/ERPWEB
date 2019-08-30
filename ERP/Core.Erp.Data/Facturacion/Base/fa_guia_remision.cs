//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Erp.Data.Facturacion.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class fa_guia_remision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fa_guia_remision()
        {
            this.fa_factura_x_fa_guia_remision = new HashSet<fa_factura_x_fa_guia_remision>();
            this.fa_guia_remision_det = new HashSet<fa_guia_remision_det>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdGuiaRemision { get; set; }
        public int IdPuntoVta { get; set; }
        public string CodGuiaRemision { get; set; }
        public string CodDocumentoTipo { get; set; }
        public string Serie1 { get; set; }
        public string Serie2 { get; set; }
        public string NumGuia_Preimpresa { get; set; }
        public string NUAutorizacion { get; set; }
        public Nullable<System.DateTime> Fecha_Autorizacion { get; set; }
        public decimal IdCliente { get; set; }
        public Nullable<int> IdContacto { get; set; }
        public decimal IdTransportista { get; set; }
        public System.DateTime gi_fecha { get; set; }
        public Nullable<decimal> gi_plazo { get; set; }
        public Nullable<System.DateTime> gi_fech_venc { get; set; }
        public string gi_Observacion { get; set; }
        public System.DateTime gi_FechaFinTraslado { get; set; }
        public System.DateTime gi_FechaInicioTraslado { get; set; }
        public string placa { get; set; }
        public string Direccion_Origen { get; set; }
        public string Direccion_Destino { get; set; }
        public bool Estado { get; set; }
        public bool aprobada_enviar_sri { get; set; }
        public Nullable<bool> Generado { get; set; }
        public int IdMotivoTraslado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<decimal> IdCbteVta { get; set; }
    
        public virtual fa_cliente fa_cliente { get; set; }
        public virtual fa_cliente_contactos fa_cliente_contactos { get; set; }
        public virtual fa_factura fa_factura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fa_factura_x_fa_guia_remision> fa_factura_x_fa_guia_remision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fa_guia_remision_det> fa_guia_remision_det { get; set; }
        public virtual fa_MotivoTraslado fa_MotivoTraslado { get; set; }
        public virtual fa_PuntoVta fa_PuntoVta { get; set; }
    }
}
