CREATE TABLE [dbo].[transfer]
(
	[Rok] INT NOT NULL,
	[From] char not null foreign key references cis_stupen(Kod),
	[To] char not null foreign key references cis_stupen(Kod),
	[Polozka] char(6) not null,
	primary key (Rok, Polozka),
	foreign key (Rok, Polozka) references cis_ek6(Rok, Kod)
)