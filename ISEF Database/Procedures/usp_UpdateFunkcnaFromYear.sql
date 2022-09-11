CREATE PROCEDURE [dbo].[usp_UpdateFunkcnaFromYear]
	@toYear int,
	@fromYear int
AS
	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_fk2 where rok=@fromYear) as f,
	(select * from cis_fk2 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_fk3 where rok=@fromYear) as f,
	(select * from cis_fk3 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_fk4 where rok=@fromYear) as f,
	(select * from cis_fk4 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_fk5 where rok=@fromYear) as f,
	(select * from cis_fk5 where rok=@toYear) as t
	where f.Kod = t.Kod
RETURN 0
