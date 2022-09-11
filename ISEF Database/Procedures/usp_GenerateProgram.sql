CREATE PROCEDURE [dbo].[usp_GenerateProgram]
	
	@fromRok int,
	@toRok int
AS
	
	insert into cis_pk3 select @toRok, kod, nazov, popis from cis_pk3 where rok= @fromRok
	insert into cis_pk5 select @toRok, kod, pk3, nazov, popis from cis_pk5 where rok= @fromRok
	insert into cis_pk7 select @toRok, kod, pk5, nazov, popis from cis_pk7 where rok= @fromRok

RETURN 0
