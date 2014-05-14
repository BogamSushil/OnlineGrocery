
CREATE PROCEDURE [dbo].[GetProduct]   
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
WHERE
	ProductId= @product_id