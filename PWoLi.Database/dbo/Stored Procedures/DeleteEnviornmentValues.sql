-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteEnviornmentValues
@ObjectId UNIQUEIDENTIFIER,
@UpdatedBy VARCHAR(50),
@UpdatedOn DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		UPDATE dbo.EnvironmentValue 
	     SET 
		    IsDeleted=1,
			UpdatedBy=@UpdatedBy,
			UpdatedOn=@UpdatedOn
		 WHERE ObjectId=@ObjectId

END