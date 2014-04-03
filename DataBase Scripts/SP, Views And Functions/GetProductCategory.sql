USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[GetProductCategory]    Script Date: 4/3/2014 10:08:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetProductCategory]   
		@product_type_code char(15)
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_type_code,
	parent_product_type_code,
	product_type_description,
	productTypeImageName	
FROM
	[dbo].[Ref_Product_Types]
WHERE
	product_type_code= @product_type_code