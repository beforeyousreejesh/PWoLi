-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertIntoConfigurationObjects] 
@ObjectId UNIQUEIDENTIFIER,
@ModuleId UNIQUEIDENTIFIER,
@Name NVARCHAR(50),
@ObjectType UNIQUEIDENTIFIER,
@ParentObjectId UNIQUEIDENTIFIER=NULL,
@IsSecret BIT,
@CreatedBy VARCHAR(50),
@CreatedOn DATETIME
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [dbo].[ConfigurationObjects]
           ([CObjectId]
           ,[ModuleId]
           ,[Name]
           ,[ObjectType]
           ,[ParentObjectId]
           ,[IsSecret]
           ,[CreatedBy]
           ,[CreatedOn])
		   VALUES
		   (
			   @ObjectId,
			   @ModuleId,
			   @Name,
			   @ObjectType,
			   @ParentObjectId,
			   @IsSecret,
			   @CreatedBy,
			   @CreatedOn
			)
END