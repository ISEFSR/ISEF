CREATE VIEW vi_ostatne as
select			-- Rok,
				org.*,
				fk.*,
				ek.*,
				zk.*,
				pk.*,
				Ucet,
				Druh_rozp,
				Skut,
				Rozpp,
				Rozpu
from			dbo.ostatne o
left join		dbo.vi_organizacie org on o.Ico = org.OrgIco
left join		dbo.vi_funkcna fk on o.Fk = fk.FKod5 and o.Rok = fk.FRok
left join		dbo.vi_ekonomicka ek on o.Ek = ek.EKod6 and o.Rok = ek.ERok
left join		dbo.vi_zdroj zk on o.Zk = zk.ZKod4 and o.Rok = zk.ZRok
left join		dbo.vi_program pk on o.Pk = pk.PKod7 and o.Rok = pk.PRok