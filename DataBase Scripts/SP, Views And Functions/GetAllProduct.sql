
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllProduct]   
		@product_id int
		

AS  
SET NOCOUNT ON    
 
SELECT 
	ProductId,
	ProductTypeCode,
	ProductName,
	ProductPrice,
	ProductColor,
	ProductSize,
	ProductImageName,
	ProductDescription,
	OtherProductDetails	
FROM
	[dbo].[Products]
