CREATE TABLE [dbo].[cis_zk4](
	[Rok] [int] NOT NULL,
	[Kod] [char](4) NOT NULL,
	[Zk3] [char](3) NOT NULL,
	[Nazov] [nvarchar](150) NOT NULL,
	[Popis] [nvarchar](max) NOT NULL,
[Pom_Kod1] NCHAR(10) NULL, 
    [Pom_Kod2] NCHAR(10) NULL, 
    [Pom_Kod3] NCHAR(10) NULL, 
    [Pom_Kod4] NCHAR(10) NULL, 
    [Pom_Kod5] NCHAR(10) NULL, 
    [Kde] CHAR NULL, 
	--foreign key (Kde) references cis_kodzk (Kod),
    PRIMARY KEY CLUSTERED 
(
	[Rok] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_cis_zk4_zk3] FOREIGN KEY (Rok, Zk3) REFERENCES [dbo].[cis_zk3]([Rok], [Kod])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO