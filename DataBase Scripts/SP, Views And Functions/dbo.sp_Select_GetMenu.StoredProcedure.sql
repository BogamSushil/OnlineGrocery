IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[sp_Select_GetMenu]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[sp_Select_GetMenu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Select_GetMenu]
AS  
SET NOCOUNT ON   
		
		SELECT
			IdMenu,
			Name 
		FROM 
			.TOPMENU 
		ORDER BY 
			SortedNumber;

GO
