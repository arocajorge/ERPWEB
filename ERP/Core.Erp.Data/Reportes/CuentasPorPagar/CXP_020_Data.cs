﻿using Core.Erp.Data.General;
using Core.Erp.Data.Reportes.Base;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_020_Data
    {
        public List<CXP_020_Info> GetList(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                List<CXP_020_Info> Lista = new List<CXP_020_Info>();

                using (Entities_reportes db = new Entities_reportes())
                {
                    var lst = db.VWCXP_020.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte_Ogiro == IdTipoCbte && q.IdCbteCble_Ogiro == IdCbteCble).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXP_020_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdTipoCbte_Ogiro = item.IdTipoCbte_Ogiro,
                            IdCbteCble_Ogiro = item.IdCbteCble_Ogiro,
                            NomDocumento = item.NomDocumento,
                            co_serie = item.co_serie,
                            co_factura = item.co_factura,
                            Num_Autorizacion = item.Num_Autorizacion,
                            fecha_autorizacion = item.fecha_autorizacion,
                            Su_Descripcion = item.Su_Descripcion,
                            Su_Direccion = item.Su_Direccion,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            pe_cedulaRuc = item.pe_cedulaRuc,
                            co_FechaFactura = item.co_FechaFactura,
                            co_observacion = item.co_observacion,
                            IdFormaPago = item.IdFormaPago,
                            nom_FormaPago = item.nom_FormaPago,
                            co_subtotal_iva = item.co_subtotal_iva,
                            co_subtotal_siniva = item.co_subtotal_siniva,
                            co_subtotal = item.co_subtotal,
                            co_total = item.co_total,
                            co_valoriva = item.co_valoriva,
                            pr_descripcion = item.pr_descripcion,
                            pr_codigo = item.pr_codigo,
                            Subtotal = item.Subtotal,
                            Descuento = item.Descuento,
                            TotalDetalle = item.TotalDetalle,
                            ValorIva = item.ValorIva,
                            pr_direccion = item.pr_direccion,
                            pr_correo = item.pr_correo,
                            Cantidad = item.Cantidad,
                            CostoUni = item.CostoUni
                        });
                    }
                }

                if (Lista.Count > 0)
                {
                    var Detalle = Lista[0];
                    if (!string.IsNullOrEmpty(Detalle.co_factura) && (string.IsNullOrEmpty(Detalle.Num_Autorizacion) || Detalle.Num_Autorizacion == "1234567890"))
                    {
                        tb_empresa_Data odataEmpresa = new tb_empresa_Data();
                        tb_sis_Documento_Tipo_Talonario_Data odataTalonario = new tb_sis_Documento_Tipo_Talonario_Data();
                        string[] Array = Detalle.co_serie.Split('-');
                        if (Array.Count() == 3)
                        {
                            string ClaveAcceso = odataTalonario.GeneraClaveAcceso(Detalle.co_FechaFactura, "06", odataEmpresa.get_info(IdEmpresa).em_ruc, Array[0] + Array[1], Detalle.co_factura);
                            Lista.ForEach(q => q.Num_Autorizacion = ClaveAcceso);
                        }
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
