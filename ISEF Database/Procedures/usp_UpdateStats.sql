CREATE PROCEDURE [dbo].[usp_UpdateStats]
	@rok int = 0
AS
	delete from dbo.stats where Rok = @rok
	insert into dbo.stats 
		select rok, stupenkod, count(rok), sum(skut), getdate() 
			from (select rok, stupen as stupenkod, skut from dbo.ostatne where Rok=@rok union all select rok, 'o' as stupenkod, skut from dbo.mao where Rok=@rok) as d
			group by Rok, Stupenkod
RETURN 0
