IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllUsers]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllUsers]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllUsers]   
		
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