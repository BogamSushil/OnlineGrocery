USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[ManageProduct]    Script Date: 4/3/2014 10:08:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ManageProduct]   
		@product_id int = NULL,
		@product_type_code char(15)  = NULL,
		@product_name varchar(80) = NULL,
		@product_price money = NULL,
		@product_color varchar(20) = NULL,
		@product_size varchar(20) = NULL,
		@productImageName nvarchar(255) = NULL,
		@product_description varchar(255) = NULL,
		@other_product_details varchar(255) = NULL,
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		[dbo].[Products](
			product_type_code,
			product_name,
			product_price,
			product_color,
			product_size,
			productImageName,
			product_description,
			other_product_details
			)

		VALUES(			
			@product_type_code,
			@product_name,
			@product_price,
			@product_color,
			@product_size,
			@productImageName,
			@product_description,
			@other_product_details
		)

		
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			dbo.[Products]
	  SET			
			product_type_code = @product_type_code,
			product_name = @product_name,
			product_price = @product_price,
			product_color = @product_color,
			product_size = @product_size,
			productImageName = @productImageName,
			product_description = @product_description,
			other_product_details = @other_product_details
		WHERE
			product_id = @product_id
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteProduct
			BEGIN TRY		
				-- Delete depedents     
				
			
			-- Delete User
				DELETE   
					dbo.[Products]
				WHERE  
					product_id = @product_id
			COMMIT TRANSACTION deleteProduct
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



