SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Select_GetSubMenuBrand]
AS
	
		SET NOCOUNT ON;
		SELECT IdBrand,Name,'Stationary.png'[Image] FROM dbo.BrandMaster ORDER BY SortedNumber;
	
