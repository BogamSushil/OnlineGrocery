IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ManageCategory]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].ManageCategory
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageCategory]   
		@CategoryId bigint,
		@ParentCategoryId bigint,
		@Name nchar(50),
		@Description nchar(100),
		@ImageName nchar(150),
		@IsActive bit,
		@UpdatedBy nchar(15),		
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		[dbo].[CategoryMaster](			
			ParentCategoryId,
			Name,
			[Description],
			ImageName,
			IsActive,
			UpdatedBy,
			UpdatedDate
			)

		VALUES(			
			@ParentCategoryId,
			@Name,
			@Description,
			@ImageName,
			@IsActive,
			@UpdatedBy,
			GetDate()
			)		
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			dbo.[CategoryMaster]
	  SET			
			ParentCategoryId =@ParentCategoryId,
			Name=@Name,
			[Description]=@Description,
			ImageName=@ImageName,
			IsActive=@IsActive,
			UpdatedBy=@UpdatedBy,
			UpdatedDate=getDate()
		WHERE
			CategoryId = @CategoryId
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteCategory
			BEGIN TRY		
				-- Delete depedents     
				
			
			-- Delete User
				DELETE   
					dbo.[CategoryMaster]
				WHERE  
					CategoryId = @CategoryId
			COMMIT TRANSACTION deleteProduct
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
		dbo.CategoryMaster
	SET
		IsActive = @IsActive
	WHERE
		CategoryId = @CategoryId
	END