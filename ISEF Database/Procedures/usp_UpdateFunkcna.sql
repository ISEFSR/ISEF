CREATE PROCEDURE [dbo].[usp_UpdateFunkcna]
	@rok int
AS
	---------------------------------------------------------------
	-- UPDATE CISFK -----------------------------------------------
	---------------------------------------------------------------
	-- update cisfk1
	update dbo.cis_fk2
	set nazov = textpar, popis = text15
	from dbo.cis_fk2 e, dbo.roc_fk c where e.Kod = c.typ and c.znac=2 and Rok = @rok

	update dbo.cis_fk3
	set nazov = textpar, popis = text15
	from dbo.cis_fk3 e, dbo.roc_fk c where e.Kod = c.typ and c.znac=3 and Rok = @rok

	update dbo.cis_fk4
	set nazov = textpar, popis = text15
	from dbo.cis_fk4 e, dbo.roc_fk c where e.Kod = c.typ and c.znac=4 and Rok = @rok

	update dbo.cis_fk5
	set nazov = textpar, popis = text15
	from dbo.cis_fk5 e, dbo.roc_fk c where e.Kod = c.typ and c.znac=5 and Rok = @rok
RETURN 0
