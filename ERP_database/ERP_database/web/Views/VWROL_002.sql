﻿CREATE VIEW web.VWROL_002
AS
SELECT        dbo.tb_persona.pe_nombreCompleto AS NombreCompleto, dbo.tb_persona.pe_cedulaRuc AS Ruc, dbo.ro_rubro_tipo.ru_descripcion AS RubroDescripcion, web.ro_SPROL_002.IdEmpresa, web.ro_SPROL_002.IdNominaTipo, 
                         web.ro_SPROL_002.IdNominaTipoLiqui, web.ro_SPROL_002.IdPeriodo, web.ro_SPROL_002.IdEmpleado, web.ro_SPROL_002.Valor, dbo.ro_cargo.ca_descripcion AS Cargo, dbo.tb_persona.pe_apellido, 
                         dbo.tb_persona.pe_nombre, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_rubro_tipo.ru_tipo, dbo.ro_empleado.em_status, dbo.ro_rubro_tipo.ru_orden, dbo.tb_empresa.em_ruc, 
                         dbo.ro_empleado.IdSucursal, dbo.ro_catalogo.ca_descripcion AS Grupo, dbo.ro_empleado.em_codigo, dbo.ro_Departamento.de_descripcion, dbo.ro_area.Descripcion AS Area
FROM            dbo.ro_rubro_tipo INNER JOIN
                         dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         web.ro_SPROL_002 ON dbo.ro_empleado.IdEmpresa = web.ro_SPROL_002.IdEmpresa AND dbo.ro_empleado.IdEmpleado = web.ro_SPROL_002.IdEmpleado ON dbo.ro_rubro_tipo.IdRubro = web.ro_SPROL_002.IdRubro AND 
                         dbo.ro_rubro_tipo.IdEmpresa = web.ro_SPROL_002.IdEmpresa INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo AND dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa INNER JOIN
                         dbo.ro_periodo ON web.ro_SPROL_002.IdEmpresa = dbo.ro_periodo.IdEmpresa AND web.ro_SPROL_002.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.tb_empresa ON dbo.ro_empleado.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_rubro_tipo.rub_grupo = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento AND 
                         dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento AND dbo.tb_empresa.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND 
                         dbo.tb_empresa.IdEmpresa = dbo.ro_Departamento.IdEmpresa INNER JOIN
                         dbo.ro_area ON dbo.ro_rubro_tipo.IdEmpresa = dbo.ro_area.IdEmpresa AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[14] 4[5] 2[22] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -97
      End
      Begin Tables = 
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 0
               Left = 80
               Bottom = 296
               Right = 319
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 13
               Left = 494
               Bottom = 418
               Right = 783
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 24
               Left = 1199
               Bottom = 239
               Right = 1431
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_SPROL_002 (web)"
            Begin Extent = 
               Top = 139
               Left = 329
               Bottom = 269
               Right = 521
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 431
               Left = 211
               Bottom = 561
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo"
            Begin Extent = 
               Top = 246
               Left = 259
               Bottom = 376
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 148
               Left = 960
               Bottom = 371
               Right = 1179
           ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_002';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_catalogo"
            Begin Extent = 
               Top = 241
               Left = 330
               Bottom = 549
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 227
               Left = 911
               Bottom = 357
               Right = 1090
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_area"
            Begin Extent = 
               Top = 1
               Left = 1000
               Bottom = 131
               Right = 1179
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 32
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_002';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_002';

