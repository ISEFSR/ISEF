CREATE TABLE [dbo].[cis_obec]
(
	[Kod] int primary key,
	[KodOkres] smallint foreign key references dbo.cis_okres (Kod),
	[Nazov] nvarchar(250) not null ,
	[Skrateny] nvarchar(50) null
)
