USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[GetProductByCategory]    Script Date: 4/3/2014 10:07:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetProductByCategory]   
		@product_type_code char(15)
		

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
WHERE
	ProductTypeCode= @product_type_code