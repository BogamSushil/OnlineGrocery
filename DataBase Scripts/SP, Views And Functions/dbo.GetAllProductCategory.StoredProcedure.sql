IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllProductCategory]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllProductCategory]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProductCategory]   
		
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_type_code,
	parent_product_type_code,
	product_type_description,
	productTypeImageName	
FROM
	[dbo].[Ref_Product_Types]

	
GO
