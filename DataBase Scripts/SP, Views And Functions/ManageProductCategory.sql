USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[ManageProductCategory]    Script Date: 4/3/2014 10:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ManageProductCategory]   
		@product_type_code char(15) = null,
		@parent_product_type_code char(15) = null,
		@product_type_description varchar(80) = null,
		@productTypeImageName nvarchar(250) = null,
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		[dbo].[Ref_Product_Types](	
			product_type_code,
			parent_product_type_code,
			product_type_description,
			productTypeImageName
			)

		VALUES(			
			@product_type_code,
			@parent_product_type_code,
			@product_type_description,
			@productTypeImageName	
		)

		
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			dbo.[Ref_Product_Types]
	  SET			
			parent_product_type_code = @parent_product_type_code,
			product_type_description = @product_type_description,
			productTypeImageName = @productTypeImageName
		WHERE
			product_type_code = @product_type_code
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteUser
			BEGIN TRY		
				-- Delete depedents     
				
			
			-- Delete User
				DELETE   
					dbo.[Ref_Product_Types]
				WHERE  
					product_type_code = @product_type_code
			COMMIT TRANSACTION deleteUser
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION deleteUser
				DECLARE @ErrorMessage nvarchar(4000)
				DECLARE @Severity int  
				DECLARE @ErrorState int 

				SET	@ErrorMessage = CAST(ERROR_MESSAGE() AS varchar(2500))
				SET @Severity = ERROR_SEVERITY()  
				SET @ErrorState = ERROR_STATE() 
				RAISERROR (@ErrorMessage, @Severity, @ErrorState)  
			END CATCH
	END  



