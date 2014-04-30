IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetUser]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetUser]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUser]   
		@EmailID nvarchar(200)
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
WHERE EmailID = @EmailID 
GO
