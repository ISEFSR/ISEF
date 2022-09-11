CREATE TABLE [dbo].[mao](
	[Rok] [int] NOT NULL,
	[Ico] [char](8) NOT NULL,
	[NadriadeneIco] [char](8) not null,
	[Okres] [char](3) NOT NULL,
	[Obec] [char](6) NOT NULL,
	[Segm] [char](2) NOT NULL,
	[Ucet] CHAR(3) NOT NULL,
	[Cast] [char](1) NOT NULL,
	[Fk] [char](5) NULL,
	[Ek] [char](6) NOT NULL,
	[Zk] [char](4) NOT NULL,
	[Pk] [char](7) NULL,
	[Skut] [decimal](16, 2) NOT NULL,
	[Rozpp] [decimal](16, 2) NOT NULL,
	[Rozpu] [decimal](16, 2) NOT NULL,
	[Sknace] [char](5) NOT NULL,
	[Druh_rozp] [char](3) NOT NULL,
	[Remove] bit not null default(0),
	foreign key (Rok, Fk) references dbo.cis_fk5 (Rok, Kod),
	foreign key (Rok, Ek) references dbo.cis_ek6 (Rok, Kod),
	foreign key (Rok, Zk) references dbo.cis_zk4 (Rok, Kod),
	foreign key (Rok, Pk) references dbo.cis_pk7 (Rok, Kod),
	foreign key (Ico) references dbo.cis_org (Ico)
	--foreign key (Ucet) references dbo.cis_ucet (Kod),
	--foreign key (Sknace) references dbo.cis_sknace(Kod)
) ON [PRIMARY]
GO