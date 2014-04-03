SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AuthenticateUser]   
		@EmailID nvarchar(200),
		@HashPassword nvarchar(100)
AS  
SET NOCOUNT ON    
 
SELECT 
	UserID,
	UserName,
	FirstName,
	LastName,
	EmailID,
	[Password],
	[HashId],
	IsActive,
	U.RoleID,
	R.Name AS RoleName,
	CreatedBy,
	CreateDate,
	ModifiedBy,
	ModifiedDate
FROM
	DBO.[User] U
INNER JOIN DBO.[Role] R On R.RoleId = U.RoleId
WHERE EmailID = @EmailID AND [Password] = @HashPassword AND IsActive = 1