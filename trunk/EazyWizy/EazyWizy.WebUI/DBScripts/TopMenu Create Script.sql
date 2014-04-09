/*
	Name				: AV
	Date				: 5-April-2014	
	Purpose				: To Store TopMenu
*/

CREATE TABLE TOPMENU
(
	IdMenu INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(70) NOT NULL,
	CreatedBy INT,
	CreatedDate DATETIME,
	UpdatedBy INT,
	UpdatedTime DATETIME,
	SortedNumber INT,
	IsActive BIT	
)


/*
	Name				: AV
	Date				: 5-April-2014	
	Purpose				: To Get Menu Items
	Unit Test			: sp_Select_GetMenu
*/
CREATE PROCEDURE sp_Select_GetMenu
AS
	BEGIN
		SET NOCOUNT ON;
		SELECT IdMenu,Name FROM EazyWizy.dbo.TOPMENU ORDER BY SortedNumber;
	END
GO