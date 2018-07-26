﻿using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_sis_Documento_Tipo_Talonario_Bus
    {
        tb_sis_Documento_Tipo_Talonario_Data odata = new tb_sis_Documento_Tipo_Talonario_Data();

        public List<tb_sis_Documento_Tipo_Talonario_Info> get_list(int IdEmpresa, bool mostrar_anulados)
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
        public tb_sis_Documento_Tipo_Talonario_Info get_info(int IdEmpresa, string CodDocumentoTipo, string Establecimiento, string PuntoEmision, string NumDocumento)
        {
            try
            {
                return odata.get_info(IdEmpresa, CodDocumentoTipo, Establecimiento, PuntoEmision, NumDocumento);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_sis_Documento_Tipo_Talonario_Info info)
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
        public bool modificarDB(tb_sis_Documento_Tipo_Talonario_Info info)
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
        public bool anularDB(tb_sis_Documento_Tipo_Talonario_Info info)
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
        public string get_NumeroDocumentoInicial(int IdEmpresa, string CodDcumentoTipo, string Establecimiento, string PuntoEmision)
        {
            try
            {
                return odata.get_NumeroDocumentoInicial(IdEmpresa, CodDcumentoTipo, Establecimiento, PuntoEmision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sis_Documento_Tipo_Talonario_Info get_info_ultimo_no_usado(int IdEmpresa, string CodDocumentoTipo)
        {
            try
            {
                return odata.get_info_ultimo_no_usado(IdEmpresa, CodDocumentoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_sis_Documento_Tipo_Talonario_Info get_info_ultimo_no_usado(int IdEmpresa, string Establecimiento, string PuntoEmision, string CodDocumentoTipo)
        {
            try
            {
                return odata.get_info_ultimo_no_usado(IdEmpresa, Establecimiento, PuntoEmision, CodDocumentoTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
