IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllCategories]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllCategories]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCategories] 

AS

 SET NOCOUNT ON;

    SELECT 
		CategoryId,
		ParentCategoryId,
		Name,
		[Description],
		ImageName,
		IsActive,
		UpdatedBy,
		UpdatedDate
    FROM 
		CategoryMaster
   ORDER BY UpdatedDate desc