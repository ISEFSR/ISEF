CREATE PROCEDURE [dbo].[usp_UpdateCiselniky]
	@rok int = 0
AS
	--select * from dbo.cis_fk5
	--select top 1 * from dbo.cis_fk5
	--select top 1 * from dbo.cis_fk4
	--select top 1 * from dbo.cis_fk3
	--select top 1 * from dbo.cis_fk2
	--select top 10 * from dbo.cispar$
	exec dbo.usp_UpdateFunkcna @rok

	exec dbo.usp_UpdateEkonomicka @rok
	---------------------------------------------------------------

	exec dbo.usp_UpdateZdroj @rok
	---------------------------------------------------------------

	exec dbo.usp_UpdateProgram @rok
	---------------------------------------------------------------
RETURN 0
