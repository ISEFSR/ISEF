CREATE VIEW [dbo].vi_ekonomicka as
select			e1.Rok as ERok,
				e1.Kod as EKod1,
				e1.Nazov as ENazov1,

				e2.Kod as EKod2,
				e2.Nazov as ENazov2,

				e3.Kod as EKod3,
				e3.Nazov as ENazov3,

				e6.Kod as EKod6,
				e6.Nazov as ENazov6
	
from			[dbo].[cis_ek6] e6
left join		[cis_ek3] e3 on e6.Ek3 = e3.Kod and e6.Rok = e3.Rok
left join		[cis_ek2] e2 on e3.Ek2 = e2.Kod and e3.Rok = e2.Rok
left join		[cis_ek1] e1 on e2.Ek1 = e1.Kod and e2.Rok = e1.Rok