CREATE PROCEDURE UpdateSystems
@SystemId UNIQUEIDENTIFIER,
@Name NVarchar(50),
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
	Name=@Name,
	ModifiedBy=@UpdatedBy,
    ModifiedOn=@UpdatedOn 
WHERE Id=@SystemId

END