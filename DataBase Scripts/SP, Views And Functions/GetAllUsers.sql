
/****** Object:  StoredProcedure [dbo].[GetChieldProductCategorys]    Script Date: 4/3/2014 10:00:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]   
		
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