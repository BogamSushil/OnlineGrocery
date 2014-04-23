/*
	Name				: AV
	Date				: 15-April-2014	
	Purpose				: To Store LHSMenu
*/

CREATE TABLE LHSMenu
(
	IdLHSMenu INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(70) NOT NULL,
	[Image] [varchar](100) NULL,
	CreatedBy INT,
	CreatedDate DATETIME,
	UpdatedBy INT,
	UpdatedTime DATETIME,
	SortedNumber INT,
	IsActive BIT	
)
