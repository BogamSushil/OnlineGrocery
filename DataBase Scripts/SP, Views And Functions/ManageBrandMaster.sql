IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ManageBrandMaster]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].ManageBrandMaster
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageBrandMaster]   
		@BrandId bigint = null,
		@BrandName nchar(50) = null,
		@BrandDescription nchar(200) = null,
		@IsActive bit = null,
		@CreatedBy nchar(50) = null,
		@CreatedDate datetime = null,
		@LogoFileName nvarchar(150) = NULL,
		@ModifiedBy nchar(50) = null,
		@ModifiedDate datetime = null,		
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		[dbo].[BrandMaster](		
			BrandName,
			BrandDescription,
			IsActive,
			LogoFileName,
			CreatedBy,
			CreatedDate,
			ModifiedBy,
			ModifiedDate
			)

		VALUES(			
			@BrandName,
			@BrandDescription,
			@IsActive,
			@LogoFileName,
			@CreatedBy,
			@CreatedDate,
			@ModifiedBy,
			@ModifiedDate
			)		
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			[dbo].[BrandMaster]
	  SET			
			BrandName =@BrandName,
			BrandDescription =@BrandDescription,
			IsActive =@IsActive,
			LogoFileName =@LogoFileName,		
			ModifiedBy =@ModifiedBy,
			ModifiedDate = GETDATE()
		WHERE
			BrandId = @BrandId
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteBrand
			BEGIN TRY		
				-- Delete depedents     
				DELETE 
					[dbo].[Products]
				FROM 
					[dbo].[Products] P
					INNER JOIN [dbo].[ProductBrand] PB ON PB.ProductId = P.ProductId 
				WHERE
					PB.BrandId = @BrandId
		
				DELETE 
					[dbo].[ProductBrand]
				WHERE
					BrandId = @BrandId
			-- Delete User
				DELETE   
					[dbo].[BrandMaster]
				WHERE  
					BrandId = @BrandId
			COMMIT TRANSACTION deleteBrand
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION deleteCategory
				DECLARE @ErrorMessage nvarchar(4000)
				DECLARE @Severity int  
				DECLARE @ErrorState int 

				SET	@ErrorMessage = CAST(ERROR_MESSAGE() AS varchar(2500))
				SET @Severity = ERROR_SEVERITY()  
				SET @ErrorState = ERROR_STATE() 
				RAISERROR (@ErrorMessage, @Severity, @ErrorState)  
			END CATCH
	END  



	ELSE IF @Operation = 7  
	BEGIN 
	UPDATE 
		[dbo].[BrandMaster]
	SET
		IsActive = @IsActive
	WHERE
		[BrandId] = @BrandId
	END