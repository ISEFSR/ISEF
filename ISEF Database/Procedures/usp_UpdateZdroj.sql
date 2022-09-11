CREATE PROCEDURE [dbo].[usp_UpdateZdroj]
	@rok int
AS
	---------------------------------------------------------------
	-- UPDATE CISZDROJ --------------------------------------------
	---------------------------------------------------------------
	-- UPDATE CISZK1
	update dbo.cis_zk1
	set nazov = [text], popis = textzdroj
	from dbo.cis_zk1 z, dbo.roc_zk c where z.Kod = c.zdroj and c.znac=1 and Rok = @rok
	-- UPDATE CISZK2
	update dbo.cis_zk2
	set nazov = [text], popis = textzdroj
	from dbo.cis_zk2 z, dbo.roc_zk c where z.Kod = c.zdroj and c.znac=2 and Rok = @rok
	-- UPDAET CISZK3
	update dbo.cis_zk3
	set nazov = [text], popis = textzdroj
	from dbo.cis_zk3 z, dbo.roc_zk c where z.Kod = c.zdroj and c.znac=3 and Rok = @rok
	-- UPDATE CISZK4
	update dbo.cis_zk4
	set nazov = [text], popis = textzdroj, Pom_Kod1 = c.pom_kod1, Pom_Kod2 = c.pom_kod2, Pom_Kod3 = c.pom_kod3, Pom_Kod4 = c.pom_kod4, Pom_Kod5 = c.pom_kod5, Kde = c.kde
	from dbo.cis_zk4 z, dbo.roc_zk c where z.Kod = c.zdroj and c.znac=4 and Rok = @rok
RETURN 0
