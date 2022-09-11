CREATE FUNCTION [dbo].[ufn_GetStupenSummary]
(
	@rok int,
	@stupen char(1)
)
RETURNS @returntable TABLE
(
	Rok int ,
	DateCreated datetime2, 
	TotalCount int, 
	TotalSum decimal,
	Kod char(1),
	Nazov nvarchar(max),
	Popis nvarchar(max)
)
AS
BEGIN
	INSERT @returntable
	SELECT * from vi_stats where Kod = @stupen and Rok = @rok
	RETURN
END
