CREATE TABLE [dbo].[cis_okres]
(
	[Kod] smallint NOT NULL PRIMARY KEY,
	[KodKraj] smallint not null foreign key references dbo.cis_kraj ([kod]),
	[SkratenyNazov] char(2) not null,
	[Nazov] nvarchar(250) not null
)