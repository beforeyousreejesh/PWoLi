-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetSystemByName
@SystemName NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT MD.Id SystemId,MD.Name SystemName,MD.ParentModule ParentSystemId FROM DBO.Modules MD WITH(NOLOCK)
	WHERE MD.Name=@SystemName
END