-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteConfigurationObjects
@ObjectId UNIQUEIDENTIFIER,
@UpdatedBy NVARCHAR(50),
@UpdatedOn DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		UPDATE dbo.ConfigurationObjects 
	     SET 
		    IsDeleted=1,
			UpdatedBy=@UpdatedBy,
			UpdatedOn=@UpdatedOn
		 WHERE CObjectId=@ObjectId
END