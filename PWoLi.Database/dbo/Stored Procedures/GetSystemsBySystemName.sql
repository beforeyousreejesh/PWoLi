-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSystemsBySystemName] 
@SystemName NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

;With SystemsCTE As(
  SELECT 
	  PM.Id SystemId,
	  PM.Name SystemName,
	  PM.ParentModule,
	  CAST(null as uniqueidentifier) TopParentId 
  FROM dbo.Modules PM WHERE PM.ParentModule is null AND PM.IsDeleted=0
  UNION ALL
  SELECT
	  MD.Id SystemId,
	  MD.Name SystemName,
	  MD.ParentModule ParentSystemId,
	  ISNULL(SCTE.TopParentId,MD.ParentModule) TopParentId
  FROM
  SystemsCTE SCTE INNER JOIN dbo.Modules MD ON MD.ParentModule=SCTE.SystemId AND MD.IsDeleted=0),
  SearchCTE AS (
  SELECT 
	SCTE.SystemId,
	SCTE.SystemName,
	SCTE.ParentModule, 
    SCTE.TopParentId 
FROM SystemsCTE SCTE WHERE SCTE.SystemName LIKE '%'+@SystemName+'%'
UNION ALL 
  SELECT 
	SCTE.SystemId,
	SCTE.SystemName,
	SCTE.ParentModule, 
    SCTE.TopParentId 
FROM SearchCTE HCTE INNER JOIN SystemsCTE SCTE ON HCTE.ParentModule=SCTE.SystemId
  )SELECT distinct HCTE.SystemId,HCTE.SystemName,HCTE.ParentModule ParentSystemId,HCTE.TopParentId FROM SearchCTE HCTE


END