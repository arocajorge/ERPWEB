﻿using Core.Erp.Data.Facturacion;
using Core.Erp.Data.General;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_cliente_Bus
    {
        fa_cliente_Data odata = new fa_cliente_Data();
        tb_persona_Data odata_per = new tb_persona_Data();  
        public List<fa_cliente_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ValidarCupoCreditoCliente(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc, decimal IdCliente, double Total, ref string mensaje)
        {
            try
            {
                return odata.ValidarCupoCreditoCliente(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, vt_tipoDoc, IdCliente, Total, ref mensaje);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public fa_cliente_Info get_info(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public fa_cliente_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                return odata.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(fa_cliente_Info info)
        {
            try
            {
                if (info.IdPersona == 0)
                {
                    info.info_persona = odata_per.armar_info(info.info_persona);
                    if (odata_per.guardarDB(info.info_persona))
                    {
                        info.IdPersona = info.info_persona.IdPersona;
                        return odata.guardarDB(info);
                    }
                }
                else
                    return odata.guardarDB(info);
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(fa_cliente_Info info)
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
        public bool anularDB(fa_cliente_Info info)
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
    }
}
