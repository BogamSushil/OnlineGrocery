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
	product_type_code= @product_type_code