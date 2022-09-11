CREATE PROCEDURE [dbo].[usp_GenerateEkonomicka]

	@fromRok int,
	@toRok int
AS
	
	insert into cis_ek1 select @toRok, kod, nazov, popis from cis_ek1 where rok= @fromRok
	insert into cis_ek2 select @toRok, kod, ek1, nazov, popis from cis_ek2 where rok= @fromRok
	insert into cis_ek3 select @toRok, kod, ek2, nazov, popis from cis_ek3 where rok= @fromRok
	insert into cis_ek6 select @toRok, kod, ek3, nazov, popis from cis_ek6 where rok= @fromRok

RETURN 0
