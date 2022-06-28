-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetSystemGroupRole
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT GR.ModuleId SystemId,GR.GroupName,RO.Name RoleName FROM dbo.GroupRole GR INNER JOIN dbo.Role RO ON GR.RoleId=RO.Id
END