/*
	Name				: AV
	Date				: 15-April-2014	
	Purpose				: To Get LHS Menu Items
	Unit Test			: sp_Select_GetLHSMenu
*/
CREATE PROCEDURE sp_Select_GetLHSMenu
AS
	BEGIN
		SET NOCOUNT ON;
		SELECT IdLHSMenu,Name,Image FROM EazyWizy.dbo.LHSMenu ORDER BY SortedNumber;
	END
GO