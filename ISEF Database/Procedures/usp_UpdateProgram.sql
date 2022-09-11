CREATE PROCEDURE [dbo].[usp_UpdateProgram]
	@rok int
AS
	---------------------------------------------------------------
	-- UPDATE CISPROG ---------------------------------------------
	---------------------------------------------------------------
	-- UPDATE CISPK3
	update dbo.cis_pk3
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_pk3 p, dbo.roc_pk c where p.Kod = c.prog and c.znac=1 and Rok = @rok
	-- UPDATE CISPK5
	update dbo.cis_pk5
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_pk5 p, dbo.roc_pk c where p.Kod = c.prog and c.znac=2 and Rok = @rok
	-- UPDAET CISPK7
	update dbo.cis_pk7
	set nazov = textskr, popis = text1 + text2
	from dbo.cis_pk7 p, dbo.roc_pk c where p.Kod = c.prog and c.znac=3 and Rok = @rok
RETURN 0
