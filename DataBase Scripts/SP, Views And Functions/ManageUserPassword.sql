SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ManageUserPassword]   
		@EmailId nvarchar(100),
		@Password varchar(100)
		

AS  
SET NOCOUNT ON    

UPDATE
	dbo.[User]
SET 
	[Password] = @Password

WHERE
	EmailID = @EmailId
