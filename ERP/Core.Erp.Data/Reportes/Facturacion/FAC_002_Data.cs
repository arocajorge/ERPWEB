﻿using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_002_Data
    {
        public List<FAC_002_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCliente, DateTime fechaCorte, bool MostrarSoloCarteraVencida)
        {
            try
            {
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999999 : IdSucursal;

                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 9999999 : IdCliente;

                fechaCorte = fechaCorte.Date;

                List<FAC_002_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPFAC_002(IdEmpresa, IdSucursalIni, IdSucursalFin, IdClienteIni, IdClienteFin, fechaCorte,MostrarSoloCarteraVencida)
                             select new FAC_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCliente = q.IdCliente,
                                 Codigo = q.Codigo,
                                 IdCbteVta = q.IdCbteVta,
                                 CodCbteVta = q.CodCbteVta,
                                 vt_fecha = q.vt_fecha,
                                 vt_fech_venc = q.vt_fech_venc,
                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_Observacion = q.vt_Observacion,
                                 vt_plazo = q.vt_plazo,
                                 vt_serie1 = q.vt_serie1,
                                 vt_serie2 = q.vt_serie2,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 Su_Descripcion = q.Su_Descripcion,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_telefonoOfic = q.pe_telefonoOfic,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Valor_Original = q.Valor_Original,
                                 Valor_vencido = q.Valor_vencido,
                                 Valor_x_Vencer = q.Valor_x_Vencer,
                                 Vencer_30_Dias = q.Vencer_30_Dias,
                                 Vencer_60_Dias = q.Vencer_60_Dias,
                                 Vencer_90_Dias = q.Vencer_90_Dias,
                                 Dias_Vencidos = q.Dias_Vencidos,
                                 Idtipo_cliente = q.Idtipo_cliente,
                                 Mayor_a_90Dias = q.Mayor_a_90Dias,
                                 Saldo = q.Saldo,
                                 Total_Pagado = q.Total_Pagado,
                                 TelefonoContacto = q.TelefonoContacto,
                                 IdContacto = q.IdContacto,
                                 NomContacto = q.NomContacto
                             }).ToList();
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
