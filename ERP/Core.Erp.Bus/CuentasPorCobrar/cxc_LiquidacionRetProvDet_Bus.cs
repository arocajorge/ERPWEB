﻿using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionRetProvDet_Bus
    {
        cxc_LiquidacionRetProvDet_Data odata = new cxc_LiquidacionRetProvDet_Data();

        public List<cxc_LiquidacionRetProvDet_Info> GetList(int IdEmpresa,int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdLiquidacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_LiquidacionRetProvDet_Info> GetList_X_Cruzar(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata.GetList_X_Cruzar(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
