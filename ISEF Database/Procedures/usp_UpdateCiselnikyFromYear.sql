CREATE PROCEDURE [dbo].[usp_UpdateCiselnikyFromYear]
	@toYear int,
	@fromYear int
AS

	exec dbo.usp_UpdateEkonomickaFromYear @toYear, @fromYear
	exec dbo.usp_UpdateFunkcnaFromYear @toYear, @fromYear
	exec dbo.usp_UpdateZdrojFromYear @toYear, @fromYear
	exec dbo.usp_UpdateProgramFromYear @toYear, @fromYear

RETURN 0
