USE [OnlineGrocery]
GO
/****** Object:  StoredProcedure [dbo].[ManageProduct]    Script Date: 4/3/2014 10:08:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ManageProduct]   
		@productId int = NULL,
		@productTypeCode char(15)  = NULL,
		@productName varchar(80) = NULL,
		@productPrice money = NULL,
		@productColor varchar(20) = NULL,
		@productSize varchar(20) = NULL,
		@productImageName nvarchar(255) = NULL,
		@productDescription varchar(255) = NULL,
		@otherProductDetails varchar(255) = NULL,
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		[dbo].[Products](
			ProductTypeCode,
			ProductName,
			ProductPrice,
			ProductColor,
			ProductSize,
			ProductImageName,
			ProductDescription,
			OtherProductDetails
			)

		VALUES(			
			@productTypeCode,
			@productName,
			@productPrice,
			@productColor,
			@productSize,
			@productImageName,
			@productDescription,
			@otherProductDetails
		)

		
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			dbo.[Products]
	  SET			
			ProductTypeCode = @productTypeCode,
			ProductName = @productName,
			ProductPrice = @productPrice,
			ProductColor = @productColor,
			ProductSize = @productSize,
			ProductImageName = @productImageName,
			ProductDescription = @productDescription,
			OtherProductDetails = @otherProductDetails
		WHERE
			ProductId = @productId
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
					ProductId = @productId
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



