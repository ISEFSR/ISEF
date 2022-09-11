CREATE TABLE [dbo].[cis_ek3](
	[Rok] [int] NOT NULL,
	[Kod] [char](3) NOT NULL,
	[Ek2] [char](2) NOT NULL,
	[Nazov] [nvarchar](150) NOT NULL,
	[Popis] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Rok] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [FK_cis_ek3_ek2] FOREIGN KEY (Rok, Ek2) REFERENCES [dbo].[cis_ek2]([Rok], [Kod])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
