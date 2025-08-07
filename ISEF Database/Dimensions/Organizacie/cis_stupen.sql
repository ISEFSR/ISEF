CREATE TABLE [dbo].[cis_stupen]
(
	[Kod] [char](1) primary key NOT NULL,
	--[Rok] int primary key not null,
	[Nazov] [nvarchar](20) NOT NULL,
	[Popis] [nvarchar](150) NOT NULL,
    [Komentar] NVARCHAR(MAX) null, 
    [Farba] INT NOT NULL
) 