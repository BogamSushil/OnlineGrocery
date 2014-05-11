
CREATE PROCEDURE [dbo].[GetProduct]   
		@product_id int	

AS  
SET NOCOUNT ON    
 
SELECT 
	product_id,
	product_type_code,
	product_name,
	product_price,
	product_color,
	product_size,
	productImageName,
	product_description,
	other_product_details	
FROM
	[dbo].[Products]
WHERE
	product_id= @product_id