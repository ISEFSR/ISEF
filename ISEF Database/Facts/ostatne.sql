CREATE TABLE [dbo].[ostatne](
	[Rok] [int] NOT NULL,
	[Stupen] [char](1) NOT NULL,
	[Ico] [char](8) NOT NULL,
	[Klient] [varchar](10) NOT NULL,
	[Druh_rozpisu] [char](3) NOT NULL,
	[Kapitola] [char](3) NOT NULL,
	[Ucet] [char](3) NOT NULL,
	[Fk] [char](5) NULL,
	[Ek] [char](6) NOT NULL,
	[Zk] [char](4) NOT NULL,
	[Pk] [char](7) NULL,
	[Druh_rozp] [char](3) NOT NULL,
	[Skut] [decimal](16, 2) NOT NULL,
	[Rozpp] [decimal](16, 2) NOT NULL,
	[Rozpu] [decimal](16, 2) NOT NULL,
	foreign key (Rok, Fk) references dbo.cis_fk5 (Rok, Kod),
	foreign key (Rok, Ek) references dbo.cis_ek6 (Rok, Kod),
	foreign key (Rok, Zk) references dbo.cis_zk4 (Rok, Kod),
	foreign key (Rok, Pk) references dbo.cis_pk7 (Rok, Kod),
	foreign key (Ico) references dbo.cis_org (Ico)
	--foreign key (Ucet) references dbo.cis_ucet (Kod),
) ON [PRIMARY]
GO