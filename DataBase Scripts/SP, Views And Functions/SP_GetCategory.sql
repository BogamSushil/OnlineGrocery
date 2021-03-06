IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetCategories]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetCategories]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategories] 
 @ParentCategory bigint

AS

 SET NOCOUNT ON;

 IF @ParentCategory IS NULL
	SET @ParentCategory=0

    SELECT 
	
		CategoryId,
		ParentCategoryId,
		Name,
		[Description],
		ImageName,
		IsActive,
		UpdatedBy,
		UpdatedDate
    FROM CategoryMaster
    WHERE ParentCategoryId = @ParentCategory

GO