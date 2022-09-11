CREATE PROCEDURE [dbo].[usp_GenerateZdroj]
	@fromRok int,
	@toRok int
AS
	
	insert into cis_zk1 select @toRok, kod, nazov, popis from cis_zk1 where rok= @fromRok
	insert into cis_zk2 select @toRok, kod, zk1, nazov, popis from cis_zk2 where rok= @fromRok
	insert into cis_zk3 select @toRok, kod, zk2, nazov, popis from cis_zk3 where rok= @fromRok
	insert into cis_zk4 select @toRok, kod, zk3, nazov, popis, Pom_Kod1, Pom_Kod2, Pom_Kod3, Pom_Kod4, Pom_Kod5, kde from cis_zk4 where rok= @fromRok

RETURN 0
