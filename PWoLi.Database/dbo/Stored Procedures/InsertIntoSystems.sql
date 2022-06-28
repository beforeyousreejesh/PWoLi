CREATE PROCEDURE InsertIntoSystems
@SystemId UNIQUEIDENTIFIER,
@Name NVarchar(50),
@ParentModule UNIQUEIDENTIFIER=NULL,
@CreatedBy NVARCHAR(50),
@CreatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     INSERT INTO [dbo].[Modules]
           ([Id]
           ,[Name]
           ,[ParentModule]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@SystemId
           ,@Name
           ,@ParentModule
           ,@CreatedBy
           ,@CreatedOn)
END