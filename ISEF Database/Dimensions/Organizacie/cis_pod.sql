CREATE TABLE [dbo].[cis_pod]
(
	[Kod] char(8) primary key NOT NULL, 
    [Rok] int not null,
    [Nazov] NVARCHAR(250) NOT NULL, 
    [SkratenyNazov] NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_cis_pod UNIQUE (Rok, Kod)
)
