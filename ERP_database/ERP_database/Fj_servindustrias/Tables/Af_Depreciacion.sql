﻿CREATE TABLE [Fj_servindustrias].[Af_Depreciacion] (
    [IdEmpresa]           INT           NOT NULL,
    [IdDepreciacion]      DECIMAL (18)  NOT NULL,
    [IdTipoDepreciacion]  INT           NOT NULL,
    [Cod_Depreciacion]    CHAR (20)     NOT NULL,
    [IdPeriodo]           INT           NOT NULL,
    [Descripcion]         VARCHAR (200) NULL,
    [Fecha_Depreciacion]  DATETIME      NOT NULL,
    [Num_Act_Depre]       INT           NOT NULL,
    [Valor_Tot_Act]       FLOAT (53)    NOT NULL,
    [Valor_Tot_Depre]     FLOAT (53)    NOT NULL,
    [Valor_Tot_DepreAcum] FLOAT (53)    NOT NULL,
    [Valot_Tot_Importe]   FLOAT (53)    NOT NULL,
    [IdUsuario]           VARCHAR (20)  NULL,
    [Fecha_Transac]       DATETIME      NULL,
    [IdUsuarioUltMod]     VARCHAR (20)  NULL,
    [Fecha_UltMod]        DATETIME      NULL,
    [IdUsuarioUltAnu]     VARCHAR (20)  NULL,
    [Fecha_UltAnu]        DATETIME      NULL,
    [MotivoAnula]         VARCHAR (100) NULL,
    [nom_pc]              VARCHAR (50)  NULL,
    [ip]                  VARCHAR (25)  NULL,
    [Estado]              CHAR (1)      NULL
);

