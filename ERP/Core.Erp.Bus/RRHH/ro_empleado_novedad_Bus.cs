﻿using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_empleado_novedad_Bus
    {
        ro_empleado_novedad_Data odata = new ro_empleado_novedad_Data();
        ro_empleado_novedad_det_Data odata_det = new ro_empleado_novedad_det_Data();
        public List<ro_empleado_novedad_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_inicio, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_novedad_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdNovedad)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdEmpleado, IdNovedad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_empleado_novedad_Info info)
        {
            try
            {
                info.TotalValor = info.lst_novedad_det.Sum(v=>v.Valor);
                info.NumCoutas = info.lst_novedad_det.Count();
                if (odata.guardarDB(info))
                {
                    info.IdNovedad = info.IdNovedad;
                    info.lst_novedad_det.ForEach(q => { q.IdEmpresa = info.IdEmpresa; q.IdNomina_tipo = info.IdNomina_Tipo;q.IdNomina_Tipo_Liq = info.IdNomina_TipoLiqui; q.IdEmpleado = info.IdEmpleado; q.IdNovedad = info.IdNovedad; if(q.Observacion==null)  q.Observacion = ""; });
                    odata_det = new ro_empleado_novedad_det_Data();
                    return odata_det.guardarDB(info.lst_novedad_det);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_empleado_novedad_Info info)
        {
            try
            {
                odata_det = new ro_empleado_novedad_det_Data();
                info.TotalValor = info.lst_novedad_det.Sum(v => v.Valor);
                info.NumCoutas = info.lst_novedad_det.Count();
                if (odata.modificarDB(info))
                {
                    info.lst_novedad_det.ForEach(q => { q.IdEmpresa = info.IdEmpresa; q.IdNomina_tipo = info.IdNomina_Tipo; q.IdNomina_Tipo_Liq = info.IdNomina_TipoLiqui; q.IdEmpleado = info.IdEmpleado; q.IdNovedad = info.IdNovedad; });
                    odata_det.eliminarDB(info);
                    return odata_det.guardarDB(info.lst_novedad_det);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_empleado_novedad_Info info)
        {
            try
            {

                odata_det = new ro_empleado_novedad_det_Data();
                info.TotalValor = info.lst_novedad_det.Sum(v => v.Valor);
                info.NumCoutas = info.lst_novedad_det.Count();
                if (odata.anularDB(info))
                {
                    info.lst_novedad_det.ForEach(q => { q.IdEmpresa = info.IdEmpresa; q.IdNomina_tipo = info.IdNomina_Tipo; q.IdNomina_Tipo_Liq = info.IdNomina_TipoLiqui; q.IdEmpleado = info.IdEmpleado; q.IdNovedad = info.IdNovedad; });
                  return  odata_det.AnularD(info);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
