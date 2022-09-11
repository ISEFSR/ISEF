CREATE TABLE [dbo].[cis_segment]
(
	[Kod] char(2) NOT NULL PRIMARY KEY,
	[SkratenyText] char(2) not null,
	[Popis] nvarchaR(100) not null, 
    [Komentar] NVARCHAR(MAX) null
)
