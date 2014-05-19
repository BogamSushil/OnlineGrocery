IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[AuthenticateUser]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[AuthenticateUser]
GO

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
	C.FirstName,
	C.LastName,
	C.[MiddleInitial],
	U.EmailID,
	[Password],
	[HashId],
	IsActive,
	U.RoleID,
	R.Name AS RoleName,
	[Gender],
	[PhoneNumber],
	[TownCity],
	[Country],
	[OrganisationOrPerson],
	[OrganisationName],
	CreatedBy,
	CreateDate,
	ModifiedBy,
	ModifiedDate
FROM
	[dbo].[User] U
LEFT OUTER JOIN [dbo].[Customers] C On C.[EmailAddress] = U.EmailId
INNER JOIN DBO.[Role] R On R.RoleId = U.RoleId
WHERE EmailID = @EmailID AND [Password] = @HashPassword AND IsActive = 1