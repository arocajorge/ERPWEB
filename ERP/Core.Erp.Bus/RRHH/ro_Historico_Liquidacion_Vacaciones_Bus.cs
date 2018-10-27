﻿using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public  class ro_Historico_Liquidacion_Vacaciones_Bus
    {
        #region MyRegion
        ro_Solicitud_Vacaciones_x_empleado_Bus bus_solicitud = new ro_Solicitud_Vacaciones_x_empleado_Bus();
        ro_Historico_Liquidacion_Vacaciones_Data data = new ro_Historico_Liquidacion_Vacaciones_Data();
        ro_Historico_Liquidacion_Vacaciones_Info info = new ro_Historico_Liquidacion_Vacaciones_Info();
        ro_Solicitud_Vacaciones_x_empleado_Info info_solicitud = new ro_Solicitud_Vacaciones_x_empleado_Info();
        ro_rol_detalle_x_rubro_acumulado_Bus bus_rubros_acumulados = new ro_rol_detalle_x_rubro_acumulado_Bus();
        ro_Historico_Liquidacion_Vacaciones_Det_Bus bus_detalle = new ro_Historico_Liquidacion_Vacaciones_Det_Bus();

        #endregion
        public Boolean guardarDB(ro_Historico_Liquidacion_Vacaciones_Info Info)
        {
            try
            {

                if (data.guardarDB(Info))
                {
                    bus_detalle.Eliminar(info);
                    foreach (var item in Info.detalle)
                    {
                        item.IdEmpleado = Info.IdEmpleado;
                        item.IdEmpresa = Info.IdEmpresa;
                        item.IdLiquidacion = Info.IdLiquidacion;
                        bus_detalle.Guardar_DB(item);

                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception )
            {
                throw;
            }

        }
        public Boolean modificarDB(ro_Historico_Liquidacion_Vacaciones_Info Info)
        {
            try
            {
                Info.ValorCancelado = Info.detalle.Sum(v=>v.Valor_Cancelar);
                if (data.modificarDB(Info))
                {
                    bus_detalle.Eliminar(Info);
                    foreach (var item in Info.detalle)
                    {
                        item.IdEmpleado = Info.IdEmpleado;
                        item.IdEmpresa = Info.IdEmpresa;
                        item.IdLiquidacion = Info.IdLiquidacion;
                        bus_detalle.Guardar_DB(item);

                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ro_Historico_Liquidacion_Vacaciones_Info> get_list(int IdEmpresa)
        {
            try
            {

                return data.get_list(IdEmpresa,false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ro_Historico_Liquidacion_Vacaciones_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdLiquidacion)
        {
            try
            {
                ro_Historico_Liquidacion_Vacaciones_Info info = new ro_Historico_Liquidacion_Vacaciones_Info();
                info= data.get_info(IdEmpresa, IdEmpleado, IdLiquidacion);
               info.detalle= bus_detalle.Get_Lis(IdEmpresa, IdEmpleado, IdLiquidacion);
                return info;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Boolean anularDB(ro_Historico_Liquidacion_Vacaciones_Info Info)
        {
            try
            {
                Info.ValorCancelado = Info.detalle.Sum(v => v.Valor_Cancelar);
                if (data.anularDB(Info))
                {
                   
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }      
       public ro_Historico_Liquidacion_Vacaciones_Info obtener_valores(int IdEmpresa, decimal IdEmpleado, decimal IdSolicitud)
        {
            try
            {
                int secuancia = 1;
                info = new ro_Historico_Liquidacion_Vacaciones_Info();
                info_solicitud = bus_solicitud.get_info(IdEmpresa, IdEmpleado, IdSolicitud);
                if (info_solicitud == null)
                    return new ro_Historico_Liquidacion_Vacaciones_Info();
                else
                {
                    info.IdSolicitud = info_solicitud.IdSolicitud;
                    info.IdEmpleado = info_solicitud.IdEmpleado;
                    info.Dias_q_Corresponde = info_solicitud.Dias_q_Corresponde;
                    info.Dias_pendiente = info_solicitud.Dias_pendiente;
                    info.Dias_a_disfrutar = info_solicitud.Dias_a_disfrutar;
                    info.Fecha_Desde = info_solicitud.Fecha_Desde;
                    info.Fecha_Hasta = info_solicitud.Fecha_Hasta;
                    info.Fecha_Retorno = info_solicitud.Fecha_Retorno;
                    while (info_solicitud.Anio_Desde<info_solicitud.Anio_Hasta)
                    {
                        double valor_provision = 0;
                        valor_provision = bus_rubros_acumulados.get_vac_x_mes_x_anio(IdEmpresa, info.IdEmpleado, info_solicitud.Anio_Desde.Year, info_solicitud.Anio_Desde.Month);
                        ro_Historico_Liquidacion_Vacaciones_Det_Info info_det = new ro_Historico_Liquidacion_Vacaciones_Det_Info();
                        info_det.Anio = info_solicitud.Anio_Desde.Year;
                        info_det.Mes = info_solicitud.Anio_Desde.Month;
                        info_det.Total_Remuneracion = valor_provision*24;
                        info_det.Total_Vacaciones = valor_provision;
                        if (valor_provision != 0)
                        {
                            info_det.Valor_Cancelar =( valor_provision /15)* info_solicitud.Dias_a_disfrutar;
                        }
                        
                        info_solicitud.Anio_Desde= info_solicitud.Anio_Desde.AddMonths(1);
                        info_det.Sec = secuancia;
                        info.detalle.Add(info_det);
                        secuancia++;
                    }
                    return info;

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
