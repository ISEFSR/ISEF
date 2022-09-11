CREATE VIEW [dbo].vi_funkcna
	AS 
select		f2.Rok as FRok,
			f2.Kod as FKod2,
			f2.Nazov as FNazov2,

			f3.Kod as FKod3,
			f3.Nazov as FNazov3,

			f4.Kod as FKod4,
			f4.Nazov as FNazov4,

			f5.Kod as FKod5,
			f5.Nazov as FNazov5

from		cis_fk5 f5
left join   cis_fk4 f4 on f5.Fk4 = f4.Kod and f5.Rok = f4.Rok
left join	cis_fk3 f3 on f4.Fk3 = f3.Kod and f4.Rok = f3.Rok
left join	cis_fk2 f2 on f3.Fk2 = f2.Kod and f3.Rok = f2.Rok
