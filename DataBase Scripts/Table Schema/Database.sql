/****** Object:  Database [OnlineGrocery]    Script Date: 4/22/2014 10:43:51 AM ******/
CREATE DATABASE [OnlineGrocery]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineGrocery', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\OnlineGrocery.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OnlineGrocery_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\OnlineGrocery_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineGrocery] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineGrocery].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineGrocery] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineGrocery] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineGrocery] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineGrocery] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineGrocery] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineGrocery] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineGrocery] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineGrocery] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineGrocery] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineGrocery] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineGrocery] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineGrocery] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineGrocery] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineGrocery] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineGrocery] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineGrocery] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineGrocery] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineGrocery] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineGrocery] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineGrocery] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineGrocery] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineGrocery] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineGrocery] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineGrocery] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineGrocery] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineGrocery] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineGrocery] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineGrocery] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineGrocery] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AuthenticateUser]   
		@EmailID nvarchar(200),
		@HashPassword nvarchar(100)
AS  
SET NOCOUNT ON    
 
SELECT 
	UserID,
	UserName,
	FirstName,
	LastName,
	EmailID,
	[Password],
	[HashId],
	IsActive,
	U.RoleID,
	R.Name AS RoleName,
	CreatedBy,
	CreateDate,
	ModifiedBy,
	ModifiedDate
FROM
	DBO.[User] U
INNER JOIN DBO.[Role] R On R.RoleId = U.RoleId
WHERE EmailID = @EmailID AND [Password] = @HashPassword AND IsActive = 1
GO
/****** Object:  StoredProcedure [dbo].[GetAllProduct]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProduct]   
		@product_id int
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_id,
	product_type_code,
	product_name,
	product_price,
	product_color,
	product_size,
	productImageName,
	product_description,
	other_product_details	
FROM
	[dbo].[Products]

GO
/****** Object:  StoredProcedure [dbo].[GetAllProductCategory]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProductCategory]   
		
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_type_code,
	parent_product_type_code,
	product_type_description,
	productTypeImageName	
FROM
	[dbo].[Ref_Product_Types]

	
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]   
		
AS  
SET NOCOUNT ON    
 
SELECT 
	UserID,
	UserName,
	FirstName,
	LastName,
	EmailID,
	[Password],
	[HashId],
	IsActive,
	U.RoleID,
	R.Name AS RoleName,
	CreatedBy,
	CreateDate,
	ModifiedBy,
	ModifiedDate
FROM
	DBO.[User] U
INNER JOIN DBO.[Role] R On R.RoleId = U.RoleId
GO
/****** Object:  StoredProcedure [dbo].[GetChieldProductCategorys]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetChieldProductCategorys]   
		@parent_product_type_code char(15)
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_type_code,
	parent_product_type_code,
	product_type_description,
	productTypeImageName	
FROM
	[dbo].[Ref_Product_Types]
WHERE
	parent_product_type_code= @parent_product_type_code
GO
/****** Object:  StoredProcedure [dbo].[GetProduct]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProduct]   
		@product_id int
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_id,
	product_type_code,
	product_name,
	product_price,
	product_color,
	product_size,
	productImageName,
	product_description,
	other_product_details	
FROM
	[dbo].[Products]
WHERE
	product_id= @product_id
GO
/****** Object:  StoredProcedure [dbo].[GetProductByCategory]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductByCategory]   
		@product_type_code char(15)
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_id,
	product_type_code,
	product_name,
	product_price,
	product_color,
	product_size,
	productImageName,
	product_description,
	other_product_details	
FROM
	[dbo].[Products]
WHERE
	product_type_code= @product_type_code
GO
/****** Object:  StoredProcedure [dbo].[GetProductCategory]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductCategory]   
		@product_type_code char(15)
		

AS  
SET NOCOUNT ON    
 
SELECT 
	product_type_code,
	parent_product_type_code,
	product_type_description,
	productTypeImageName	
FROM
	[dbo].[Ref_Product_Types]
WHERE
	product_type_code= @product_type_code
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUser]   
		@EmailID nvarchar(200)
AS  
SET NOCOUNT ON    
 
SELECT 
	UserID,
	UserName,
	FirstName,
	LastName,
	EmailID,
	[Password],
	[HashId],
	IsActive,
	U.RoleID,
	R.Name AS RoleName,
	CreatedBy,
	CreateDate,
	ModifiedBy,
	ModifiedDate
FROM
	DBO.[User] U
INNER JOIN DBO.[Role] R On R.RoleId = U.RoleId
WHERE EmailID = @EmailID 
GO
/****** Object:  StoredProcedure [dbo].[ManageProduct]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageProduct]   
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




GO
/****** Object:  StoredProcedure [dbo].[ManageProductCategory]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ManageProductCategory]   
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




GO
/****** Object:  StoredProcedure [dbo].[ManageUserOperation]    Script Date: 4/22/2014 10:43:51 AM ******/
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

GO
/****** Object:  StoredProcedure [dbo].[sp_GetTopMenu]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTopMenu]
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
/****** Object:  Table [dbo].[Customer_Payment_Methods]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Payment_Methods](
	[customer_payment_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NOT NULL,
	[payment_method_code] [char](10) NOT NULL,
	[credit_card_number] [varchar](40) NULL,
	[payment_method_details] [char](50) NULL,
 CONSTRAINT [PK_Customer_Payment_Methods] PRIMARY KEY CLUSTERED 
(
	[customer_payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[organisation_or_person] [char](20) NULL,
	[organisation_name] [varchar](40) NULL,
	[gender] [char](1) NULL,
	[first_name] [varchar](50) NULL,
	[middle_initial] [char](1) NULL,
	[last_name] [varchar](50) NULL,
	[email_address] [varchar](255) NULL,
	[login_name] [varchar](80) NULL,
	[login_password] [varchar](20) NULL,
	[phone_number] [varchar](255) NULL,
	[address_line_1] [varchar](255) NULL,
	[address_line_2] [varchar](255) NULL,
	[address_line_3] [varchar](255) NULL,
	[address_line_4] [varchar](80) NULL,
	[town_city] [varchar](50) NULL,
	[county] [varchar](50) NULL,
	[country] [varchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiscountCouponMapper]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscountCouponMapper](
	[CouponId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[CustomerId] [int] NULL,
	[BrandId] [int] NULL,
	[CouponCode] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiscountCouponMaster]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscountCouponMaster](
	[CouponId] [int] NOT NULL,
	[IsGlobal] [bit] NULL,
	[ExprationDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[IsPercent] [bit] NOT NULL,
	[DiscountPercent] [tinyint] NULL,
	[DiscountCash] [decimal](18, 0) NULL,
	[IsActive] [bit] NOT NULL,
	[GlobalCouponCode] [varchar](20) NULL,
 CONSTRAINT [PK_DiscountMaster] PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoices](
	[invoice_number] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[invoice_status_code] [char](10) NOT NULL,
	[invoice_date] [datetime] NULL,
	[invoice_details] [varchar](255) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[invoice_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Items]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Items](
	[order_item_id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[order_item_status_code] [char](10) NOT NULL,
	[order_item_quantity] [varchar](50) NULL,
	[order_item_price] [money] NULL,
	[other_order_item_details] [varchar](255) NULL,
 CONSTRAINT [PK_Order_Items] PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NOT NULL,
	[order_status_code] [char](10) NOT NULL,
	[date_order_placed] [datetime] NOT NULL,
	[order_details] [varchar](255) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[invoice_number] [int] NOT NULL,
	[payment_date] [datetime] NULL,
	[payment_amount] [money] NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product_Prices]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Prices](
	[product_id] [int] NOT NULL,
	[product_type_code] [char](15) NOT NULL,
	[product_price] [money] NULL,
 CONSTRAINT [PK_Product_Prices] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[product_type_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductBrand](
	[BrandId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[product_price] [money] NULL,
	[product_color] [varchar](20) NULL,
	[productImageName] [nvarchar](250) NULL,
	[product_description] [varchar](255) NULL,
 CONSTRAINT [PK_ProductBrand] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[product_type_code] [char](15) NOT NULL,
	[product_name] [varchar](80) NULL,
	[product_price] [money] NULL,
	[product_color] [varchar](20) NULL,
	[product_size] [varchar](20) NULL,
	[productImageName] [nvarchar](250) NULL,
	[product_description] [varchar](255) NULL,
	[other_product_details] [varchar](255) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Gender_Codes]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Gender_Codes](
	[gender_code] [char](1) NOT NULL,
	[gender_description] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Gender_Codes] PRIMARY KEY CLUSTERED 
(
	[gender_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Invoice_Status_Codes]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Invoice_Status_Codes](
	[invoice_status_code] [char](10) NOT NULL,
	[invoice_status_description] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Invoice_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[invoice_status_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Order_Item_Status_Codes]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Order_Item_Status_Codes](
	[order_item_status_code] [char](10) NOT NULL,
	[order_item_status_description] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Order_Item_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[order_item_status_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Order_Status_Codes]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Order_Status_Codes](
	[order_status_code] [char](10) NOT NULL,
	[order_status_description] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Order_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[order_status_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Payment_Methods]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Payment_Methods](
	[payment_method_code] [char](10) NOT NULL,
	[payment_method_description] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Payment_Methods] PRIMARY KEY CLUSTERED 
(
	[payment_method_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ref_Product_Types]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ref_Product_Types](
	[product_type_code] [char](15) NOT NULL,
	[parent_product_type_code] [char](15) NULL,
	[product_type_description] [varchar](80) NULL,
	[productTypeImageName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Ref_Product_Types] PRIMARY KEY CLUSTERED 
(
	[product_type_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipment_Items]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipment_Items](
	[shipment_id] [int] NOT NULL,
	[order_item_id] [int] NOT NULL,
 CONSTRAINT [PK_Shipment_Items] PRIMARY KEY CLUSTERED 
(
	[shipment_id] ASC,
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipments](
	[shipment_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[invoice_number] [int] NOT NULL,
	[shipment_tracking_number] [varchar](80) NULL,
	[shipment_date] [datetime] NULL,
	[other_shipment_details] [varchar](255) NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[shipment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TOPMENU]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TOPMENU](
	[IdMenu] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[SortedNumber] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 4/22/2014 10:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[EmailID] [nvarchar](200) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[HashId] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NULL,
	[RoleID] [tinyint] NOT NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_User_UID] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customer_Payment_Methods] ON 

INSERT [dbo].[Customer_Payment_Methods] ([customer_payment_id], [customer_id], [payment_method_code], [credit_card_number], [payment_method_details]) VALUES (1, 1, N'AMEX      ', N'123456', N'From 01/01/2004 to 01/01/2008                     ')
INSERT [dbo].[Customer_Payment_Methods] ([customer_payment_id], [customer_id], [payment_method_code], [credit_card_number], [payment_method_details]) VALUES (2, 2, N'CASH      ', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customer_Payment_Methods] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([customer_id], [organisation_or_person], [organisation_name], [gender], [first_name], [middle_initial], [last_name], [email_address], [login_name], [login_password], [phone_number], [address_line_1], [address_line_2], [address_line_3], [address_line_4], [town_city], [county], [country]) VALUES (1, NULL, NULL, NULL, N'John', NULL, N'Doe', N'john.doe@fictitiousmail.com', NULL, NULL, NULL, N'1500 E MAIN AVE STE 201', N'SPRINGFIELD VA 22162-1010', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customers] ([customer_id], [organisation_or_person], [organisation_name], [gender], [first_name], [middle_initial], [last_name], [email_address], [login_name], [login_password], [phone_number], [address_line_1], [address_line_2], [address_line_3], [address_line_4], [town_city], [county], [country]) VALUES (2, NULL, NULL, NULL, N'Joe ', NULL, N'Bloggs', N'joe.bloggs@fictitiousmail.com', NULL, NULL, NULL, N'1776 New Cavendish Street', N'Marylebone', NULL, N'W11X 5BY', N'London', N'Greater London', N'UK')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([invoice_number], [order_id], [invoice_status_code], [invoice_date], [invoice_details]) VALUES (1, 1, N'Paid      ', CAST(0x000098A900000000 AS DateTime), N'Single Invoice for the complete Order')
SET IDENTITY_INSERT [dbo].[Invoices] OFF
SET IDENTITY_INSERT [dbo].[Order_Items] ON 

INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (1, 1, 1, N'DEL       ', N'1', 100.0000, NULL)
INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (2, 2, 1, N'ROUTE     ', N'2', 200.0000, N'A Rare Groove')
INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (3, 3, 1, N'WAIT      ', N'3', 300.0000, N'The usual Order')
SET IDENTITY_INSERT [dbo].[Order_Items] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([order_id], [customer_id], [order_status_code], [date_order_placed], [order_details]) VALUES (1, 1, N'OPEN      ', CAST(0x000098A900000000 AS DateTime), N'First Order from a new Customer')
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([payment_id], [invoice_number], [payment_date], [payment_amount]) VALUES (1, 1, CAST(0x000098A900000000 AS DateTime), 600.0000)
SET IDENTITY_INSERT [dbo].[Payments] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (1, N'Digital Camera ', N'Olympus Camedia C-170', 64.9700, N'Silver', NULL, NULL, N'Olympus C-170 Digital Camera', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (2, N'Digital Camera ', N'Pentax Opto 50L', 89.9700, N'Rose', NULL, NULL, N'Pentax Opto 50L Digital Camera', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (3, N'Digital Camera ', N'Nikon Coolpix L3 Black', 99.9900, N'Black', NULL, NULL, N'Nikon Coolpix L3 Black', NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[Ref_Invoice_Status_Codes] ([invoice_status_code], [invoice_status_description]) VALUES (N'Issued    ', N'Issued')
INSERT [dbo].[Ref_Invoice_Status_Codes] ([invoice_status_code], [invoice_status_description]) VALUES (N'Paid      ', N'Paid')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'DEL       ', N'Delivered')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'OUT       ', N'Out of Stock')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'ROUTE     ', N'En Route')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'WAIT      ', N'Waiting')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'CANC      ', N'Cancelled')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'COMPL     ', N'Completed')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'OPEN      ', N'Open - eg just placed')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'PROV      ', N'Provisional')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'AMEX      ', N'American Express')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'CASH      ', N'Cash')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'CC        ', N'Other Credit Card')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'DD        ', N'Direct Debit')
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Book           ', NULL, N'Book Category', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Camera         ', NULL, N'Camera', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'CD             ', NULL, N'CD', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Digital Camera ', N'Camera         ', N'Digital Camera', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Food           ', NULL, N'Food', NULL)
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 1)
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 2)
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 3)
SET IDENTITY_INSERT [dbo].[Shipments] ON 

INSERT [dbo].[Shipments] ([shipment_id], [order_id], [invoice_number], [shipment_tracking_number], [shipment_date], [other_shipment_details]) VALUES (1, 1, 1, N'123456', CAST(0x000098A900000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Shipments] OFF
SET IDENTITY_INSERT [dbo].[TOPMENU] ON 

INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (1, N'HOME', 1, CAST(0x0000A30F01272E8C AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (2, N'Last Buy', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 2, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (3, N'Track Order', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 3, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (4, N'Sign Up', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 4, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (5, N'Customer Support', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 5, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (6, N'Log Out', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 6, 1)
SET IDENTITY_INSERT [dbo].[TOPMENU] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [EmailID], [Password], [HashId], [IsActive], [RoleID], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'sysadmin', N'System', N'Admin', N'sysadmin@easywizy.com', N'OYPx08RjrE6p+y4tM3pfli1dzZ4=', N'061b1639-47cd-4cbe-abae-84c5e0853ae9', 1, 1, N'sa', CAST(0x0000A30301270FA3 AS DateTime), N'sa', CAST(0x0000A30301270FA3 AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[User] ADD  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (suser_sname()) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Customer_Payment_Methods]  WITH CHECK ADD  CONSTRAINT [Customers_Customer_Payment_Methods] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customers] ([customer_id])
GO
ALTER TABLE [dbo].[Customer_Payment_Methods] CHECK CONSTRAINT [Customers_Customer_Payment_Methods]
GO
ALTER TABLE [dbo].[Customer_Payment_Methods]  WITH CHECK ADD  CONSTRAINT [Ref_Payment_Methods_Customer_Payment_Methods] FOREIGN KEY([payment_method_code])
REFERENCES [dbo].[Ref_Payment_Methods] ([payment_method_code])
GO
ALTER TABLE [dbo].[Customer_Payment_Methods] CHECK CONSTRAINT [Ref_Payment_Methods_Customer_Payment_Methods]
GO
ALTER TABLE [dbo].[DiscountCouponMapper]  WITH CHECK ADD  CONSTRAINT [FK_DiscountCouponMapper_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([customer_id])
GO
ALTER TABLE [dbo].[DiscountCouponMapper] CHECK CONSTRAINT [FK_DiscountCouponMapper_Customers]
GO
ALTER TABLE [dbo].[DiscountCouponMapper]  WITH CHECK ADD  CONSTRAINT [FK_DiscountCouponMapper_DiscountCouponMaster] FOREIGN KEY([CouponId])
REFERENCES [dbo].[DiscountCouponMaster] ([CouponId])
GO
ALTER TABLE [dbo].[DiscountCouponMapper] CHECK CONSTRAINT [FK_DiscountCouponMapper_DiscountCouponMaster]
GO
ALTER TABLE [dbo].[DiscountCouponMapper]  WITH CHECK ADD  CONSTRAINT [FK_DiscountCouponMapper_ProductBrand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[ProductBrand] ([BrandId])
GO
ALTER TABLE [dbo].[DiscountCouponMapper] CHECK CONSTRAINT [FK_DiscountCouponMapper_ProductBrand]
GO
ALTER TABLE [dbo].[DiscountCouponMapper]  WITH CHECK ADD  CONSTRAINT [FK_DiscountCouponMapper_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[DiscountCouponMapper] CHECK CONSTRAINT [FK_DiscountCouponMapper_Products]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [Invoice_Status_Codes_Invoices] FOREIGN KEY([invoice_status_code])
REFERENCES [dbo].[Ref_Invoice_Status_Codes] ([invoice_status_code])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [Invoice_Status_Codes_Invoices]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [Orders_Invoices] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [Orders_Invoices]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [Order_Item_Status_Order_Items] FOREIGN KEY([order_item_status_code])
REFERENCES [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [Order_Item_Status_Order_Items]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [Orders_Order_Items] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [Orders_Order_Items]
GO
ALTER TABLE [dbo].[Order_Items]  WITH CHECK ADD  CONSTRAINT [Products_Order_Items] FOREIGN KEY([product_id])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[Order_Items] CHECK CONSTRAINT [Products_Order_Items]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Customers_1_Orders] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customers] ([customer_id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Customers_1_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Order_Status_Codes_Orders] FOREIGN KEY([order_status_code])
REFERENCES [dbo].[Ref_Order_Status_Codes] ([order_status_code])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Order_Status_Codes_Orders]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [Invoices_Payments] FOREIGN KEY([invoice_number])
REFERENCES [dbo].[Invoices] ([invoice_number])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [Invoices_Payments]
GO
ALTER TABLE [dbo].[Product_Prices]  WITH CHECK ADD  CONSTRAINT [Products_Product_Prices] FOREIGN KEY([product_id])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[Product_Prices] CHECK CONSTRAINT [Products_Product_Prices]
GO
ALTER TABLE [dbo].[Product_Prices]  WITH CHECK ADD  CONSTRAINT [Ref_Art_Types_Product_Prices] FOREIGN KEY([product_type_code])
REFERENCES [dbo].[Ref_Product_Types] ([product_type_code])
GO
ALTER TABLE [dbo].[Product_Prices] CHECK CONSTRAINT [Ref_Art_Types_Product_Prices]
GO
ALTER TABLE [dbo].[ProductBrand]  WITH CHECK ADD  CONSTRAINT [FK_ProductBrand_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([product_id])
GO
ALTER TABLE [dbo].[ProductBrand] CHECK CONSTRAINT [FK_ProductBrand_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [Ref_Product_Types_Products] FOREIGN KEY([product_type_code])
REFERENCES [dbo].[Ref_Product_Types] ([product_type_code])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [Ref_Product_Types_Products]
GO
ALTER TABLE [dbo].[Ref_Product_Types]  WITH CHECK ADD  CONSTRAINT [Ref_Product_Types_Ref_Product_Types] FOREIGN KEY([parent_product_type_code])
REFERENCES [dbo].[Ref_Product_Types] ([product_type_code])
GO
ALTER TABLE [dbo].[Ref_Product_Types] CHECK CONSTRAINT [Ref_Product_Types_Ref_Product_Types]
GO
ALTER TABLE [dbo].[Shipment_Items]  WITH CHECK ADD  CONSTRAINT [Order_Items_Shipment_Items] FOREIGN KEY([order_item_id])
REFERENCES [dbo].[Order_Items] ([order_item_id])
GO
ALTER TABLE [dbo].[Shipment_Items] CHECK CONSTRAINT [Order_Items_Shipment_Items]
GO
ALTER TABLE [dbo].[Shipment_Items]  WITH CHECK ADD  CONSTRAINT [Shipments_Shipment_Items] FOREIGN KEY([shipment_id])
REFERENCES [dbo].[Shipments] ([shipment_id])
GO
ALTER TABLE [dbo].[Shipment_Items] CHECK CONSTRAINT [Shipments_Shipment_Items]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [Invoices_Shipments] FOREIGN KEY([invoice_number])
REFERENCES [dbo].[Invoices] ([invoice_number])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [Invoices_Shipments]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [Orders_Shipments] FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [Orders_Shipments]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleID]
GO
ALTER DATABASE [OnlineGrocery] SET  READ_WRITE 
GO
