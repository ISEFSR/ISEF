CREATE PROCEDURE [dbo].[usp_UpdateZdrojFromYear]
	@toYear int,
	@fromYear int
AS
	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_zk1 where rok=@fromYear) as f,
	(select * from cis_zk1 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_zk2 where rok=@fromYear) as f,
	(select * from cis_zk2 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_zk3 where rok=@fromYear) as f,
	(select * from cis_zk3 where rok=@toYear) as t
	where f.Kod = t.Kod

	update	t
	set		t.Nazov = f.Nazov,
			t.Popis = f.Popis
	from
	(select * from cis_zk4 where rok=@fromYear) as f,
	(select * from cis_zk4 where rok=@toYear) as t
	where f.Kod = t.Kod
RETURN 0
