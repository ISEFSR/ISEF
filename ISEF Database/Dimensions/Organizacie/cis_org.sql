CREATE TABLE [dbo].[cis_org]
(
	[Ico] char(8) not null unique,
	[Rok] int not null primary key,
	[KodSegment] char(2) not null foreign key references dbo.cis_segment (kod) default('99'),
	[KodStupen] char(1) not null  default('n'),
	[KodPodriadenost] char(8) null  DEFAULT ('99999999'),
	[KodObec] int not null foreign key references dbo.cis_obec (Kod) default(999999),
	[Nazov] nvarchar(250) null,
	[Ulica] nvarchaR(250) null,
	constraint [FK_Podriadenost] foreign key (Rok, KodPodriadenost) references dbo.cis_pod(Rok, Kod),
	constraint [FK_Stupen] foreign key (Rok, KodStupen) references dbo.cis_stupen(Rok, Kod),
	CONSTRAINT PK_cis_org unique (Rok, Ico)
	
)