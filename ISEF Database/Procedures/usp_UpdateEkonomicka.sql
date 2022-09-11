CREATE PROCEDURE [dbo].[usp_UpdateEkonomicka]
	@rok int
AS
	---------------------------------------------------------------
	-- UPDATE CISEK -----------------------------------------------
	---------------------------------------------------------------
	-- UPDATE CISEK1
	update dbo.cis_ek1
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_ek1 e, dbo.roc_ek c where e.Kod + '00' = c.pol and c.znac=1 and Rok = @rok
	-- UPDATE CISEK2
	update dbo.cis_ek2
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_ek2 e, dbo.roc_ek c where e.Kod + '0' = c.pol and c.znac=2 and Rok = @rok
	-- UPDAET CISEK3
	update dbo.cis_ek3
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_ek3 e, dbo.roc_ek c where e.Kod = c.pol and c.znac=3 and Rok = @rok
	-- UPDATE CISEK4
	update dbo.cis_ek6
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_ek6 e, dbo.roc_ek c where e.Kod = c.pol and c.znac=4 and Rok = @rok
RETURN 0
