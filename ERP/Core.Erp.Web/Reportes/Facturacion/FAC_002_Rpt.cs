﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Facturacion
{
    public partial class FAC_002_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public FAC_002_Rpt()
        {
            InitializeComponent();
        }

        private void FAC_002_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;
            lbl_empresa.Text = empresa;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = string.IsNullOrEmpty(p_IdSucursal.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            decimal IdCliente = string.IsNullOrEmpty(p_IdCliente.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdCliente.Value);
            int IdClienteContacto = string.IsNullOrEmpty(p_IdClienteContacto.Value.ToString()) ? 0 : Convert.ToInt32(p_IdClienteContacto.Value);
            DateTime fechaCorte = p_fechaCorte.Value == null ? DateTime.Now : Convert.ToDateTime(p_fechaCorte.Value);
            bool MostrarSoloCarteraVencida = p_MostrarSoloCarteraVencida.Value == null ? false : Convert.ToBoolean(p_MostrarSoloCarteraVencida.Value);

            FAC_002_Bus bus_rpt = new FAC_002_Bus();
            List<FAC_002_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdCliente, IdClienteContacto, fechaCorte,MostrarSoloCarteraVencida);
            this.DataSource = lst_rpt;
        }
    }
}
