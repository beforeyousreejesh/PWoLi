﻿
CREATE PROCEDURE DeleteModuleEnvironmentsBySystemId
@SystemId UNIQUEIDENTIFIER,
@UpdatedBy NVARCHAR(50),
@UpdatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE dbo.ModuleEnvironments SET IsDeleted=1 WHERE ModuleId=@SystemId

END