USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_GetLHSMenu]    Script Date: 4/24/2014 8:42:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Select_GetLHSMenu]
AS
	
		SET NOCOUNT ON;
		SELECT IdLHSMenu,Name,Image FROM LHSMenu ORDER BY SortedNumber;
	
