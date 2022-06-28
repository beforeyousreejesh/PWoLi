-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateEnvironmentValue
@ValueId UNIQUEIDENTIFIER,
@Value NVARCHAR(MAX),
@UpdatedBy VARCHAR(50),
@UpdatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE dbo.EnvironmentValue 
        SET 
	      Value=@Value,
	      UpdatedBy=@UpdatedBy,
	      UpdatedOn=@UpdatedOn
WHERE ValueId=@ValueId AND Value!=@Value

END