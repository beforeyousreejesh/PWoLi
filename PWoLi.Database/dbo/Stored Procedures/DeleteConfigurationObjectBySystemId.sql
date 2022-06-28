-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteConfigurationObjectBySystemId
@SystemId UNIQUEIDENTIFIER,
@UpdatedBy NVARCHAR(50),
@UpdatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
UPDATE dbo.ConfigurationObjects SET IsDeleted=1 WHERE ModuleId=@SystemId

END