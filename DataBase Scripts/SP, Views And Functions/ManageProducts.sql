IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ManageProducts]') AND xtype in ('P'))
	DROP PROCEDURE [dbo].ManageProducts
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageProducts]   
		@ProductId BIGINT =null,
		@CategoryId BigInt =  NULL,
		@TypeCode CHAR(15) =null,
		@Name VARCHAR(80) =null,
		@Price MONEY =null,
		@Color VARCHAR(20) =null,
		@Size VARCHAR(20) =null,
		@Image IMAGE =null,
		@ImageName NVARCHAR(250) =null,
		@Description VARCHAR(255)  =null,
		@BuyCounter nchar(10) =null,
		@OtherProductPetails VARCHAR(255)  =null,		
		@Operation int 
AS  
SET NOCOUNT ON    
 
DECLARE @ErrorMessage nvarchar(4000)
DECLARE @Severity int  
DECLARE @ErrorState int 

IF @Operation = 1  
	BEGIN  
		-- Add operation  
		BEGIN TRANSACTION insertProduct
			BEGIN TRY	
				INSERT INTO
					[dbo].[Products](
						TypeCode,
						Name,
						Price,
						Color,
						Size,
						[Image],
						ImageName,
						[Description],
						BuyCounter,
						OtherProductPetails
					)
				VALUES(			
					@TypeCode,
					@Name,
					@Price,
					@Color,
					@Size,
					@Image,
					@ImageName,
					@Description,
					@BuyCounter,
					@OtherProductPetails)
			set @ProductId = @@IDENTITY
		insert into 
			[dbo].[ProductCategory]
			([ProductId], [CategoryId])
			values(@ProductId, @CategoryId)

			COMMIT TRANSACTION insertProduct
		END TRY
		BEGIN CATCH
				ROLLBACK TRANSACTION insertProduct
				SET	@ErrorMessage = CAST(ERROR_MESSAGE() AS varchar(2500))
				SET @Severity = ERROR_SEVERITY()  
				SET @ErrorState = ERROR_STATE() 
				RAISERROR (@ErrorMessage, @Severity, @ErrorState)  
		END CATCH
END  

		ELSE IF @Operation = 2  
	BEGIN
		UPDATE 
			[dbo].[Products]
		SET			
			TypeCode = @TypeCode,
			Name = @Name,
			Price = @Price,
			Color = @Color,
			Size = @Size,
			[Image] = @Image,
			ImageName = @ImageName,
			[Description] = @Description,
			BuyCounter = @BuyCounter,
			OtherProductPetails = @OtherProductPetails
		WHERE
			ProductId = @ProductId
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteProduct
			BEGIN TRY		
				-- Delete depedents     
				DELETE 
					[dbo].[ProductBrand]
				FROM 
					[dbo].[ProductBrand] PB
					INNER JOIN [dbo].[Products]P ON PB.ProductId = P.ProductId 
				WHERE
					PB.ProductId = @ProductId
				DELETE 
					[dbo].[ProductProperty]
				WHERE
					ProductId = @ProductId
			
				DELETE 
					[dbo].[ProductIngredients]
				WHERE
					ProductId = @ProductId

				DELETE 
					[dbo].[ProductCategory]
				WHERE
					ProductId = @ProductId

				DELETE   
					[dbo].[Products]
				WHERE  
					ProductId = @ProductId
			COMMIT TRANSACTION deleteProduct
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION deleteProduct
				
				SET	@ErrorMessage = CAST(ERROR_MESSAGE() AS varchar(2500))
				SET @Severity = ERROR_SEVERITY()  
				SET @ErrorState = ERROR_STATE() 
				RAISERROR (@ErrorMessage, @Severity, @ErrorState)  
			END CATCH
	END  



	