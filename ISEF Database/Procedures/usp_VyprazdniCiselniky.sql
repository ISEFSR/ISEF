CREATE PROCEDURE [dbo].[usp_VyprazdniCiselniky]
	@rok int = 0
AS
delete from cis_ek6 where Rok = @rok
delete from cis_ek3 where Rok = @rok
delete from cis_ek2 where Rok = @rok
delete from cis_ek1 where Rok = @rok

delete from cis_pk7 where Rok = @rok
delete from cis_pk5 where Rok = @rok
delete from cis_pk3 where Rok = @rok

delete from cis_zk4  where Rok = @rok
delete from cis_zk3 where Rok = @rok
delete from cis_zk2 where Rok = @rok
delete from cis_zk1 where Rok = @rok

delete from cis_fk5 where Rok = @rok
delete from cis_fk4 where Rok = @rok
delete from cis_fk3 where Rok = @rok
delete from cis_fk2 where Rok = @rok
RETURN 0
