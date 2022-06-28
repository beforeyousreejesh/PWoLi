-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetConfigurationObjectsByNameAndModuleId] 
@ConfigurationName NVarchar(50),
@ModuleId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	;WITH ConfigObjectsCTE AS(
             SELECT 
			     CO.CObjectId ObjectId,
				 CO.Name,
				 CO.ObjectType,
				 CO.ParentObjectId,
				 CO.IsSecret
             FROM dbo.ConfigurationObjects CO 
			 WHERE CO.Name LIKE '%'+@ConfigurationName+ '%' AND CO.ModuleId=@ModuleId AND CO.IsDeleted=0
        UNION ALL
              SELECT 
			       CP.CObjectId ObjectId,
				   CP.Name,
				   CP.ObjectType,
				   CP.ParentObjectId,
				   CP.IsSecret
             FROM ConfigObjectsCTE CCTE 
			         INNER JOIN 
				  dbo.ConfigurationObjects CP ON CCTE.ParentObjectId=CP.CObjectId AND CP.IsDeleted=0
     )
	 SELECT DISTINCT
	      CE.ObjectId,
		  CE.Name,
		  CE.ObjectType ObjectTypeId,
		  OT.Name ObjectType,
		  CE.ParentObjectId,
		  CE.IsSecret
     FROM ConfigObjectsCTE CE
	           INNER JOIN 
		  ObjectType OT ON CE.ObjectType=OT.ObjectTypeId
END