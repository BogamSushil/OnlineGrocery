IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllProducts]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllProducts]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts] 

@CategoryId Bigint = null

AS

 SET NOCOUNT ON;
 
SELECT		 
		P.ProductId,
		TypeCode,
		Name,
		Price,
		Color,
		Size,
		[Image],
		ImageName,
		[Description],
		BuyCounter,
		OtherProductPetails,
		PC.[CategoryId]
FROM
		[dbo].[Products] P
INNER JOIN [dbo].[ProductCategory] PC ON PC.[ProductId] = P.ProductId

WHERE 
	@CategoryId Is NULL Or PC.CateGoryId = @CategoryId