IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetProduct]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetProduct]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
