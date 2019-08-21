﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Erp.Bus.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using Core.Erp.Bus.General;
using System.Collections.Generic;

namespace Core.Erp.Web.Reportes.Inventario
{
    public partial class INV_020_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public INV_020_Rpt()
        {
            InitializeComponent();
        }

        private void INV_020_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_empresa.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSucursal = p_IdSucursal.Value == null ? 0 : Convert.ToInt32(p_IdSucursal.Value);
            int IdMovi_inven_tipo = p_IdMovi_inven_tipo.Value == null ? 0 : Convert.ToInt32(p_IdMovi_inven_tipo.Value);
            decimal IdNumMovi = p_IdNumMovi.Value == null ? 0 : Convert.ToDecimal(p_IdNumMovi.Value);

            INV_020_Bus bus_rpt = new INV_020_Bus();
            List<INV_020_Info> lst_rpt = bus_rpt.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            this.DataSource = lst_rpt;

            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            lbl_empresa.Text = emp.em_nombre;

            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                logo.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }
        }
    }
}
