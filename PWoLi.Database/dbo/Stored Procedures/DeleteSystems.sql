CREATE PROCEDURE DeleteSystems
@SystemId UNIQUEIDENTIFIER,
@UpdatedBy NVARCHAR(50),
@UpdatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE 
dbo.Modules 
SET 
	IsDeleted=1,
	ModifiedBy=@UpdatedBy,
    ModifiedOn=@UpdatedOn 
WHERE Id=@SystemId

END