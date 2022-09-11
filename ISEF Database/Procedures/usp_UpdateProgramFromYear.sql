CREATE PROCEDURE [dbo].[usp_UpdateProgramFromYear]
	@toYear int,
	@fromYear int
AS
	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_pk3 where rok=@fromYear) as f,
	(select * from cis_pk3 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_pk5 where rok=@fromYear) as f,
	(select * from cis_pk5 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_pk7 where rok=@fromYear) as f,
	(select * from cis_pk7 where rok=@toYear) as t
	where f.Kod = t.Kod

RETURN 0
