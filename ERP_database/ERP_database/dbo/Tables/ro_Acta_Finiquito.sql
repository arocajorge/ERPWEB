﻿CREATE TABLE [dbo].[ro_Acta_Finiquito] (
    [IdEmpresa]                    INT           NOT NULL,
    [IdActaFiniquito]              NUMERIC (18)  NOT NULL,
    [IdEmpleado]                   NUMERIC (18)  NOT NULL,
    [IdCausaTerminacion]           VARCHAR (10)  NOT NULL,
    [IdContrato]                   NUMERIC (18)  NOT NULL,
    [IdCargo]                      INT           NULL,
    [FechaIngreso]                 DATETIME      NOT NULL,
    [FechaSalida]                  DATETIME      NOT NULL,
    [UltimaRemuneracion]           FLOAT (53)    NOT NULL,
    [Observacion]                  VARCHAR (250) NULL,
    [Ingresos]                     FLOAT (53)    NOT NULL,
    [Egresos]                      FLOAT (53)    NOT NULL,
    [IdUsuario]                    VARCHAR (20)  NULL,
    [Fecha_Transac]                DATETIME      NULL,
    [IdUsuarioUltMod]              VARCHAR (20)  NULL,
    [Fecha_UltMod]                 DATETIME      NULL,
    [IdUsuarioUltAnu]              VARCHAR (20)  NULL,
    [Fecha_UltAnu]                 DATETIME      NULL,
    [nom_pc]                       VARCHAR (50)  NULL,
    [ip]                           VARCHAR (25)  NULL,
    [Estado]                       CHAR (1)      NOT NULL,
    [MotiAnula]                    VARCHAR (200) NULL,
    [IdCodSectorial]               INT           NULL,
    [EsMujerEmbarazada]            BIT           NOT NULL,
    [EsDirigenteSindical]          BIT           NOT NULL,
    [EsPorDiscapacidad]            BIT           NOT NULL,
    [EsPorEnfermedadNoProfesional] BIT           NOT NULL,
    [IdTipoCbte]                   INT           NULL,
    [IdCbteCble]                   NUMERIC (18)  NULL,
    [IdOrdenPago]                  NUMERIC (9)   NULL,
    CONSTRAINT [PK_ro_Acta_Finiquito_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdActaFiniquito] ASC),
    CONSTRAINT [FK_ro_Acta_Finiquito_ro_cargo] FOREIGN KEY ([IdEmpresa], [IdCargo]) REFERENCES [dbo].[ro_cargo] ([IdEmpresa], [IdCargo]),
    CONSTRAINT [FK_ro_Acta_Finiquito_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

