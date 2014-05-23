IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllBrandMaster]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllBrandMaster]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBrandMaster] 


AS

 SET NOCOUNT ON;

SELECT		 
		BrandId,
		BrandName,
		BrandDescription,
		IsActive,
		LogoFileName,
		CreatedBy,
		CreatedDate,
		ModifiedBy,
		ModifiedDate
FROM
		[dbo].[BrandMaster]
