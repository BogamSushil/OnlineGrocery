USE [EazyWizy]
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 10-05-2014 15:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCategories] 

AS

 SET NOCOUNT ON;

 IF @ParentCategory IS NULL
	SET @ParentCategory=0

    SELECT [CategoryId], [Name], [Description], [IsActive], [ImageName]
    FROM CategoryMaster

GO