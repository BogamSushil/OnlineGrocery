IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetAllProducts]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[GetAllProducts]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts] 


AS

 SET NOCOUNT ON;
 
SELECT		 
		ProductId,
		TypeCode,
		Name,
		Price,
		Color,
		Size,
		[Image],
		ImageName,
		[Description],
		BuyCounter,
		OtherProductPetails
FROM
		[dbo].[Products]
