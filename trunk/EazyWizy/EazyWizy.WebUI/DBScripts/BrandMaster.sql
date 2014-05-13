/*
	Name				: AV
	Date				: 20-April-2014	
	Purpose				: To Store Brands
	DROP TABLE BRANDMASTER
*/

CREATE TABLE BrandMaster
(
	idBrand INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(70) NOT NULL,	 
	CreatedBy INT,
	CreatedDate DATETIME,
	UpdatedBy INT,
	UpdatedTime DATETIME,
	SortedNumber INT,
	IsActive BIT	
)