CREATE TABLE [dbo].[cis_fk3](
	[Rok] [int] NOT NULL,
	[Kod] [char](3) NOT NULL,
	[Fk2] [char](2) NOT NULL,
	[Nazov] [varchar](75) NOT NULL,
	[Popis] [varchar](275) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Rok] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_cis_fk3_fk2] FOREIGN KEY (Rok, Fk2) REFERENCES [dbo].[cis_fk2]([Rok], [Kod])
) ON [PRIMARY]
