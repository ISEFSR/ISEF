CREATE VIEW [dbo].vi_program
	AS 
select			p3.[Rok] as PRok,
				p3.Kod as PKod3,
				p3.Nazov as PNazov3,

				p5.Kod as PKod5,
				p5.Nazov as PNazov5,

				p7.Kod as PKod7,
				p7.Nazov as PNazov7

from			cis_pk7 p7
left join		cis_pk5 p5 on p7.Pk5 = p5.Kod and p7.Rok = p5.Rok
left join		cis_pk3 p3 on p5.Pk3 = p3.Kod and p5.Rok = p3.Rok