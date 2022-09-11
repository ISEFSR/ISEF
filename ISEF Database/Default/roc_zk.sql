/*povodny cislenik zdrojov importovany z excelu*/

CREATE TABLE [dbo].roc_zk(
	[zdroj] [nvarchar](255) NULL,
	[text] [nvarchar](255) NULL,
	[textzdroj] [nvarchar](255) NULL,
	[textm] [nvarchar](255) NULL,
	[znac] [nvarchar](255) NULL,
	[p_v] [nvarchar](255) NULL,
	[platnost] [bit] NOT NULL,
	[kde] [nvarchar](255) NULL,
	[aa] [nvarchar](255) NULL,
	[esfasr] [nvarchar](255) NULL,
	[pom_kod1] [nvarchar](255) NULL,
	[pom_kod2] [nvarchar](255) NULL,
	[pom_kod3] [nvarchar](255) NULL,
	[pom_kod4] [nvarchar](255) NULL,
	[pom_kod5] [nvarchar](255) NULL
) ON [PRIMARY]
GO