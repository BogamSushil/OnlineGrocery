IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[sp_Select_GetLHSMenu]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[sp_Select_GetLHSMenu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Select_GetLHSMenu]
AS
	
		SET NOCOUNT ON;
		SELECT IdLHSMenu,Name,Image FROM LHSMenu ORDER BY SortedNumber;
	

GO
