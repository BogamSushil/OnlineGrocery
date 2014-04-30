IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[sp_GetTopMenu]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].[sp_GetTopMenu]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTopMenu]
AS  
SET NOCOUNT ON   
		BEGIN
		SELECT
			IdMenu,
			Name 
		FROM 
			.TOPMENU 
		ORDER BY 
			SortedNumber;
			END
GO
