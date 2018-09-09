﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH.RDEP;
namespace Core.Erp.Data.RRHH
{
   public class Rdep_Data
    {
        public List<Rdep_Info> gett_list(int IdEmpresa, int Anio)
        {
            List<Rdep_Info> Lista = new List<Rdep_Info>();

            try
            {
                using (Entities_rrhh context=new Entities_rrhh())
                {
                    Lista = (from q in context.vwrdep_IngrEgr_x_Empleado
                             select new Rdep_Info
                             {
                                 IdEmpresa=q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 pe_anio = q.pe_anio,
                                 Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_nombre = q.pe_nombre,
                                 pe_apellido = q.pe_apellido,
                                 Sueldo = q.Sueldo,
                                 FondosReserva = q.FondosReserva,
                                 DecimoTercerSueldo = q.DecimoTercerSueldo,
                                 DecimoCuartoSueldo = q.DecimoCuartoSueldo,
                                 Vacaciones = q.Vacaciones,
                                 GastoAlimentacion = q.GastoAlimentacion,
                                 GastoEucacion = q.GastoEucacion,
                                 GastoSalud = q.GastoSalud,
                                 GastoVestimenta = q.GastoVestimenta,
                                 GastoVivienda = q.GastoVivienda,
                                 Utilidades = q.Utilidades,
                                 IngresoVarios = q.IngresoVarios


                             }).ToList();


                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
