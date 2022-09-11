CREATE TABLE [dbo].[stats]
(
	[Rok] int not null,
	[Stupen] char(1) not null,
	[TotalCount] int not null,
	[TotalSum] decimal(16,2) not null,
	[DateCreated] datetime2 not null default(GETDATE()),
PRIMARY KEY CLUSTERED 
(
	[Rok] ASC,
	[Stupen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_cis_stats_stupen] FOREIGN KEY (Stupen) REFERENCES [dbo].[cis_stupen]([Kod])
) ON [PRIMARY] 
GO
