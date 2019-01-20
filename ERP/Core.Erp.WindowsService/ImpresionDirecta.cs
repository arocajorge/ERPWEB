﻿using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Reportes.Facturacion;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Core.Erp.WindowsService
{
    public partial class ImpresionDirecta : ServiceBase
    {
        public ImpresionDirecta()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = TimeSpan.FromSeconds(3).TotalMilliseconds;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            #region Variables
            tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
            tb_ColaImpresionDirecta_Bus bus_colaImpresion = new tb_ColaImpresionDirecta_Bus();
            string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";
            string IPLocal = string.Empty;
            int IdSucursal = 0;
            int IdBodega = 0;
            decimal IdCbteVta = 0;
            #endregion

            #region GetIPAdress

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPLocal = ip.ToString();
                }
            }
            #endregion

            #region GetImpresion
            var Impresion = bus_colaImpresion.GetInfoPorImprimir(IPLocal);
            if (Impresion == null)
                return;
            #endregion

            try
            {
                var reporte = bus_rep_x_emp.GetInfo(Impresion.IdEmpresa, Impresion.CodReporte);
                if (!string.IsNullOrEmpty(Impresion.Parametros))
                {
                    string[] array = Impresion.Parametros.Split(',');
                    if (array.Count() > 2)
                    {
                        IdSucursal = Convert.ToInt32(array[0]);
                        IdBodega = Convert.ToInt32(array[1]);
                        IdCbteVta = Convert.ToDecimal(array[2]);
                    }
                }
                #region 


                switch (Impresion.CodReporte)
                {
                    case "FAC_003":
                        FAC_003_Rpt RPT_003 = new FAC_003_Rpt();

                        #region Cargo diseño desde base                        
                        if (reporte != null)
                        {
                            System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                            RPT_003.LoadLayout(RootReporte);
                        }
                        #endregion

                        #region Parametros
                        if (!string.IsNullOrEmpty(Impresion.Parametros))
                        {
                            RPT_003.p_IdEmpresa.Value = Impresion.IdEmpresa;
                            RPT_003.p_IdBodega.Value = IdBodega;
                            RPT_003.p_IdSucursal.Value = IdSucursal;
                            RPT_003.p_IdCbteVta.Value = IdCbteVta;
                            RPT_003.p_mostrar_cuotas.Value = false;
                            RPT_003.PrinterName = Impresion.IPImpresora;
                            RPT_003.CreateDocument();
                        }
                        #endregion

                        PrintToolBase tool003 = new PrintToolBase(RPT_003.PrintingSystem);
                        if (string.IsNullOrEmpty(Impresion.IPImpresora))
                            tool003.Print();
                        else
                            tool003.Print(Impresion.IPImpresora);
                        break;
                    case "FAC_013":
                        FAC_013_Rpt RPT_013 = new FAC_013_Rpt();

                        #region Cargo diseño desde base                        
                        if (reporte != null)
                        {
                            System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                            RPT_013.LoadLayout(RootReporte);
                        }
                        #endregion

                        #region Parametros
                        if (!string.IsNullOrEmpty(Impresion.Parametros))
                        {
                            RPT_013.p_IdEmpresa.Value = Impresion.IdEmpresa;
                            RPT_013.p_IdBodega.Value = IdBodega;
                            RPT_013.p_IdSucursal.Value = IdSucursal;
                            RPT_013.p_IdCbteVta.Value = IdCbteVta;
                            RPT_013.PrinterName = Impresion.IPImpresora;
                            RPT_013.CreateDocument();
                        }
                        #endregion

                        PrintToolBase tool013 = new PrintToolBase(RPT_013.PrintingSystem);
                        if (string.IsNullOrEmpty(Impresion.IPImpresora))
                            tool013.Print();
                        else
                            tool013.Print(Impresion.IPImpresora);
                        break;
                }
                #endregion
                bus_colaImpresion.ModificarDB(Impresion);
            }
            catch (Exception ex)
            {
                Impresion.Comentario = ex.Message.ToString();
                bus_colaImpresion.ModificarDB(Impresion);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
