
/****** Object:  StoredProcedure [dbo].[ManageUserOperation]    Script Date: 4/3/2014 10:08:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageUserOperation]   
		@UserID int = NULL,
		@UserName nvarchar(200) = NULL,
		@FirstName nvarchar(200) = NULL,
		@LastName nvarchar(200) = NULL, 
		@EmailID nvarchar(200) = NULL,
		@Password varchar(100) = NULL,
		@HashId nvarchar(250) = NULL,
		@IsActive BIT = NULL,		
		@RoleID tinyint = NULL,
		@CreatedBy  nvarchar(510) = NULL,
		@CreateDate DATETIME = NULL,
		@ModifiedBy  nvarchar(510) = NULL,
		@ModifiedDate DATETIME = NULL,		
		@Operation int 

AS  
SET NOCOUNT ON    
 
	IF @Operation = 1  
	BEGIN  
		-- Add operation  

	INSERT INTO
		dbo.[User](	
			UserName,
			FirstName,
			LastName,
			EmailID,
			[Password],
			[HashId],
			IsActive,
			RoleID,
			CreatedBy,
			CreateDate,
			ModifiedBy,
			ModifiedDate)

		VALUES(			
			@UserName,
			@FirstName,
			@LastName,
			@EmailID,
			@Password,
			@HashId,
			@IsActive,
			@RoleID,
			@CreatedBy,
			GETDATE(),
			@ModifiedBy,
			GETDATE()		
		)

	
	END  

		ELSE IF @Operation = 2  
	BEGIN
	  UPDATE 
			dbo.[User]
	  SET		
			UserName = @UserName,
			FirstName = @FirstName,
			LastName= @LastName,
			EmailID= @EmailID,			
			RoleID= @RoleID,			
			ModifiedBy= @ModifiedBy,
			ModifiedDate = GETDATE()		
		WHERE
			UserID = @UserID
	END

	ELSE IF @Operation = 3  
	BEGIN  
		BEGIN TRANSACTION deleteUser
			BEGIN TRY		
				-- Delete depedents     
				
			
			-- Delete User
				DELETE   
					dbo.[User]
				WHERE  
					UserID = @UserID  
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


	ELSE IF @Operation = 7
	BEGIN
		UPDATE 
			dbo.[User]
		SET
			IsActive = @IsActive
		WHERE
			UserID = @UserID
	END
