CREATE VIEW [dbo].vi_zdroj
	AS
select			z1.Rok as ZRok,
				z1.Kod as ZKod1,
				z1.Nazov as ZNazov1,

				z2.Kod as ZKod2,
				z2.Nazov as ZNazov2,

				z3.Kod as ZKod3,
				z3.Nazov as ZNazov3,

				z4.Kod as ZKod4,
				z4.Nazov as ZNazov4,
				z4.Pom_Kod1,
				z4.Pom_Kod2,
				z4.Pom_Kod3,
				z4.Pom_Kod4,
				z4.Pom_Kod5,
				z4.Kde

from			cis_zk4 z4
left join		cis_zk3 z3 on z4.Zk3 = z3.Kod and z4.Rok = z3.Rok
left join		cis_zk2 z2 on z3.Zk2 = z2.Kod and z3.Rok = z2.Rok
left join		cis_zk1 z1 on z2.Zk1 = z1.Kod and z2.Rok = z1.Rok