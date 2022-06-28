-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateConfigurationObject
@ObjectId UNIQUEIDENTIFIER,
@Name NVARCHAR(50),
@ObjectType UNIQUEIDENTIFIER,
@IsSecret BIT,
@UpdatedBy NVARCHAR(50),
@UpdatedOn DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.ConfigurationObjects 
	     SET 
		    Name=@Name,
			ObjectType=@ObjectType,
			IsSecret=@IsSecret,
			UpdatedBy=@UpdatedBy,
			UpdatedOn=@UpdatedOn
		 WHERE CObjectId=@ObjectId
END