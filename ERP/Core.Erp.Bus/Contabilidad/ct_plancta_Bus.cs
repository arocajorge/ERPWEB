﻿using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using DevExpress.Web;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_plancta_Bus
    {
        ct_plancta_Data odata = new ct_plancta_Data();

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarCtaMovimiento, string cta_padre)
        {
            try
            {
                return odata.get_list_bajo_demanda(args, IdEmpresa, MostrarCtaMovimiento, cta_padre);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool MostrarCtaPadre)
        {
            try
            {
                return odata.get_list_bajo_demanda(args, IdEmpresa, MostrarCtaPadre);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            try
            {
                return odata.get_info_bajo_demanda(args, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ct_plancta_Info> get_list(int IdEmpresa, bool mostrar_anulados, bool mostrar_solo_cuentas_movimiento)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados, mostrar_solo_cuentas_movimiento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_plancta_Info get_info(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCtaCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_plancta_Info info)
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

        public bool modificarDB(ct_plancta_Info info)
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

        public bool anularDB(ct_plancta_Info info)
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
        public ct_plancta_Info get_info_nuevo(int IdEmpresa, string IdCtaCble_padre)
        {
            try
            {
                return odata.get_info_nuevo(IdEmpresa, IdCtaCble_padre);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_id(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                return odata.validar_existe_id(IdEmpresa, IdCtaCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string get_CtaCble_acreedora(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_CtaCble_acreedora(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public double get_saldo_anterior(int IdEmpresa, string IdCtaCble, DateTime Fecha_corte)
        {
            try
            {
                return odata.get_saldo_anterior(IdEmpresa, IdCtaCble, Fecha_corte);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {            
            return odata.get_list_bajo_demanda(args, IdEmpresa);
        }
    }
}
