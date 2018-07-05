﻿using Core.Erp.Data;
using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_cobro_Bus
    {
        cxc_cobro_Data odata = new cxc_cobro_Data();
        public List<cxc_cobro_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cxc_cobro_Info get_info(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(cxc_cobro_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<cxc_cobro_Info> get_list_para_retencion(int IdEmpresa, int IdSucursal, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list_para_retencion(IdEmpresa, IdSucursal, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_cobro_Info get_info_para_retencion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc)
        {
            try
            {
                return odata.get_info_para_retencion(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, vt_tipoDoc);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
