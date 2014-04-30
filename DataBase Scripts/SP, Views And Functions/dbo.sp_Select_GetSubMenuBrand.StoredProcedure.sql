IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[sp_Select_GetSubMenuBrand]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[sp_Select_GetSubMenuBrand]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Select_GetSubMenuBrand]
AS
	
		SET NOCOUNT ON;
		SELECT IdBrand,Name,'Stationary.png'[Image] FROM dbo.BrandMaster ORDER BY SortedNumber;
	

GO
