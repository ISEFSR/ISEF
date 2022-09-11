CREATE PROCEDURE [dbo].[usp_OdstranDataPreRok]
	@rok int = 0
AS
	delete from dbo.mao where rok =@rok
	delete from dbo.ostatne where rok = @rok
RETURN 0
