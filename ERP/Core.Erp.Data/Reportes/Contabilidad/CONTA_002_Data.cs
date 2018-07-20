﻿using Core.Erp.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Contabilidad
{
    public class CONTA_002_Data
    {
        public List<CONTA_002_Info> get_list(int IdEmpresa, string IdCtaCble, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                List<CONTA_002_Info> Lista;
                fechaIni = fechaIni.Date;
                fechaFin = fechaFin.Date;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPCONTA_002(IdEmpresa, IdCtaCble, fechaIni, fechaFin)
                             select new CONTA_002_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 Saldo = q.Saldo,
                                 SaldoInicial = q.SaldoInicial,
                                 cb_Estado = q.cb_Estado,
                                 cb_Fecha = q.cb_Fecha,
                                 cb_Observacion = q.cb_Observacion,
                                 tc_TipoCbte = q.tc_TipoCbte
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
