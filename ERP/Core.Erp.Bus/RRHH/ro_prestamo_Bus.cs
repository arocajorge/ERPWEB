﻿using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public class ro_prestamo_Bus
    {
        ro_prestamo_Data odata = new ro_prestamo_Data();
        ro_prestamo_detalle_Data odata_det = new ro_prestamo_detalle_Data();
        public List<ro_prestamo_Info> get_list(int IdEmpresa, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fechaInicio, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_prestamo_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdPrestamo)
        {
            try
            {
                ro_prestamo_Info info_ = new ro_prestamo_Info();
                info_= odata.get_info(IdEmpresa, IdEmpleado, IdPrestamo);
                info_.lst_detalle = odata_det.get_list(IdEmpresa, IdPrestamo);
                return info_;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_prestamo_Info info)
        {
            try
            {
                if (info.descuento_mensual)
                    get_calculomensual(info);
                if (info.descuento_quincena)
                    get_calculoquincenal(info);
                info.NumCuotas = info.lst_detalle.Count();
                info.Fecha = info.Fecha_PriPago;
                if (odata.guardarDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    info.lst_detalle.ForEach(v=> { v.IdEmpresa = info.IdEmpresa; v.IdPrestamo = info.IdPrestamo;  });
                    return odata_det.guardarDB(info.lst_detalle);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_prestamo_Info info)
        {
            try
            {
                odata = new ro_prestamo_Data();
                info.NumCuotas = info.lst_detalle.Count();
                info.Fecha = info.Fecha_PriPago;
                if (odata.modificarDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    info.lst_detalle.ForEach(v => { v.IdEmpresa = info.IdEmpresa; v.IdPrestamo = info.IdPrestamo; });
                    odata_det.eliminarDB(info);
                    return odata_det.guardarDB(info.lst_detalle);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_prestamo_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    info.IdPrestamo = info.IdPrestamo;
                    odata_det = new ro_prestamo_detalle_Data();
                    return odata_det.AnularD(info);
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_prestamo_Info get_calculomensual(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.TotalPrestamo = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if (i == 1)
                    { item.FechaPago = fecha_pago;
                    }
                    else { fecha_pago = fecha_pago.AddMonths(1); }
                   
                        item.FechaPago = info.Fecha_PriPago;
                        item.NumCuota = i;
                        item.TotalCuota = valor_cuota;
                        item.Saldo = info.MontoSol;
                        item.Saldo = saldo - item.TotalCuota;
                        item.FechaPago = fecha_pago;
                        item.EstadoPago = "PEN";
                        item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                        item.IdNominaTipoLiqui = 2;

                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);

                    info.lst_detalle.Add(item);
                    
                }
                return info;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public ro_prestamo_Info get_calculoquincenal(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.TotalPrestamo = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if (i == 1)
                    {
                        item.FechaPago = fecha_pago;
                    }
                    else { fecha_pago = fecha_pago.AddMonths(1); }

                    item.FechaPago = info.Fecha_PriPago;
                    item.NumCuota = i;
                    item.TotalCuota = valor_cuota;
                    item.Saldo = info.MontoSol;
                    item.Saldo = saldo - item.TotalCuota;
                    item.FechaPago = fecha_pago;
                    item.EstadoPago = "PEN";
                    item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                    item.IdNominaTipoLiqui = 1;

                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);
                    info.lst_detalle.Add(item);

                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ro_prestamo_Info get_calculoquincenal_y_men(ro_prestamo_Info info)
        {
            try
            {
                info.lst_detalle = new List<ro_prestamo_detalle_Info>();
                int periodo = Convert.ToInt32(info.NumCuotas);
                double valor_cuota = info.MontoSol / info.NumCuotas;
                double saldo = info.MontoSol;
                DateTime fecha_pago = info.Fecha_PriPago;
                info.TotalPrestamo = info.MontoSol;
                List<ro_prestamo_detalle_Info> listaDetalle = new List<ro_prestamo_detalle_Info>();
                for (int i = 1; i <= periodo; i++)
                {
                    ro_prestamo_detalle_Info item = new ro_prestamo_detalle_Info();

                    if(i==1)
                    {
                        fecha_pago = info.Fecha_PriPago; 
                    }
                    else
                    {
                        fecha_pago = fecha_pago.AddDays(15);
                    }
                    if (fecha_pago.Day > 15)
                    {
                        if (fecha_pago.Month != 2)
                            fecha_pago = Convert.ToDateTime("30/" + fecha_pago.Month.ToString() + "/" + fecha_pago.Year.ToString());
                        else
                            fecha_pago = Convert.ToDateTime("28/" + fecha_pago.Month.ToString() + "/" + fecha_pago.Year.ToString());

                    }
                    else
                    {
                        fecha_pago = Convert.ToDateTime("15/" + fecha_pago.Month.ToString() + "/" + fecha_pago.Year.ToString());
                    }

                    item.FechaPago = info.Fecha_PriPago;
                    item.NumCuota = i;
                    item.TotalCuota = valor_cuota;
                    item.Saldo = info.MontoSol;
                    item.Saldo = saldo - item.TotalCuota;
                    item.FechaPago = fecha_pago;
                    item.EstadoPago = "PEN";
                    item.Observacion_det = "Cuota número " + i + " fecha pago " + fecha_pago.ToString("dd/MM/yyyy");
                    if (info.Fecha_PriPago.Day > 15)
                        item.IdNominaTipoLiqui = 2;
                    else
                        item.IdNominaTipoLiqui = 1;
                    saldo = saldo - valor_cuota;
                    item.TotalCuota = Math.Round(item.TotalCuota, 2);
                    item.Saldo = Math.Round(item.Saldo, 2);
                    info.lst_detalle.Add(item);

                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_prestamo_Info get_calculo_prestamo(ro_prestamo_Info info)
        {
            try
            {
                if (info.descuento_mensual)
                    info= get_calculomensual(info);
                if (info.descuento_quincena)
                    info= get_calculoquincenal(info);
                if (info.descuento_men_quin)
                    info = get_calculoquincenal_y_men(info);
                return info;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
