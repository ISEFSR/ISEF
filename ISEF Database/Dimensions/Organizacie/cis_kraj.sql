CREATE TABLE [dbo].[cis_kraj]
(
	[Kod] smallint NOT NULL PRIMARY KEY,
	[SkratenyNazov] char(2) not null,
	[Nazov] nvarchar(150) not null
)