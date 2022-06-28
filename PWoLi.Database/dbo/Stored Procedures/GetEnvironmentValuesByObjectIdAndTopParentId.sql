-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEnvironmentValuesByObjectIdAndTopParentId]
@ObjectId UNIQUEIDENTIFIER,
@TopParentId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	  SELECT 
		   ME.ModuleEnvironmentId,
		   ME.ModuleId,
		   ME.EnvironmentId,
		   EV.ValueId,
		   EV.Value,
		   CO.Name,
		   CO.IsSecret,
		   EM.IsSecretEnabled EnvironmentSecretEnabled,
		   EM.Name EnvironmentName
			 FROM dbo.ModuleEnvironments ME WITH(NOLOCK) 
                 LEFT JOIN dbo.EnvironmentValue EV WITH(NOLOCK) 
		ON ME.ModuleEnvironmentId=EV.ModuleEnvironmentId AND EV.ObjectId=@ObjectId
		LEFT JOIN ConfigurationObjects CO WITH(NOLOCK) ON CO.CObjectId=ObjectId
		INNER JOIN Environments EM WITH(NOLOCK) ON EM.EnvironmentId=ME.EnvironmentId
		WHERE  ME.ModuleId=@TopParentId
END