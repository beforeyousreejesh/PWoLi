-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertIntoEnvironmentValue
@ValueId UNIQUEIDENTIFIER,
@ModuleEnvironmentId UNIQUEIDENTIFIER,
@ObjectId UNIQUEIDENTIFIER,
@Value NVARCHAR(MAX),
@CreatedBy VARCHAR(50),
@CreatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[EnvironmentValue]
           ([ValueId]
           ,[ModuleEnvironmentId]
           ,[ObjectId]
           ,[Value]
           ,[IsDeleted]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@ValueId
           ,@ModuleEnvironmentId
           ,@ObjectId
           ,@Value
           ,0
           ,@CreatedBy
           ,@CreatedOn)
END