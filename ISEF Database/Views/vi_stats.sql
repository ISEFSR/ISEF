CREATE VIEW [dbo].vi_stats
AS 
SELECT		s.Rok,
			s.DateCreated, 
			s.TotalCount, 
			s.TotalSum,

			cs.Kod,
			cs.Nazov,
			cs.Popis

FROM				[stats] s 
left outer join		[cis_stupen] cs on s.Stupen = cs.Kod 