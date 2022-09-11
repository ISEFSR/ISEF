CREATE PROCEDURE [dbo].[usp_UpdateEkonomickaFromYear]
	@toYear int,
	@fromYear int
AS
	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_ek1 where rok=@fromYear) as f,
	(select * from cis_ek1 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_ek2 where rok=@fromYear) as f,
	(select * from cis_ek2 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_ek3 where rok=@fromYear) as f,
	(select * from cis_ek3 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_ek6 where rok=@fromYear) as f,
	(select * from cis_ek6 where rok=@toYear) as t
	where f.Kod = t.Kod
RETURN 0
