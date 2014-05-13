/*
	Name				: AV
	Date				: 20-April-2014	
	Purpose				: To Get Brand Menu Items
	Unit Test			: sp_Select_GetSubMenuBrand
*/
CREATE PROCEDURE sp_Select_GetSubMenuBrand
AS
	BEGIN
		SET NOCOUNT ON;
		SELECT IdBrand,Name,'Stationary.png'[Image] FROM EazyWizy.dbo.BrandMaster ORDER BY SortedNumber;
	END
GO