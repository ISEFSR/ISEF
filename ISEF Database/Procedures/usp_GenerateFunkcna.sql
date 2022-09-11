CREATE PROCEDURE [dbo].[usp_GenerateFunkcna]
	@fromRok int,
	@toRok int
AS
	
	insert into cis_fk2 select @toRok, kod, nazov, popis from cis_fk2 where rok= @fromRok
	insert into cis_fk3 select @toRok, kod, fk2, nazov, popis from cis_fk3 where rok= @fromRok
	insert into cis_fk4 select @toRok, kod, fk3, nazov, popis from cis_fk4 where rok= @fromRok
	insert into cis_fk5 select @toRok, kod, fk4, nazov, popis from cis_fk5 where rok= @fromRok

RETURN 0