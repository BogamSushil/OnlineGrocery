USE [master]
GO
/****** Object:  Database [EazyWizy]    Script Date: 18-05-2014 00:11:31 ******/
CREATE DATABASE [EazyWizy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GrocerySystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\GrocerySystem.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GrocerySystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\GrocerySystem_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EazyWizy] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EazyWizy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EazyWizy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EazyWizy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EazyWizy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EazyWizy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EazyWizy] SET ARITHABORT OFF 
GO
ALTER DATABASE [EazyWizy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EazyWizy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EazyWizy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EazyWizy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EazyWizy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EazyWizy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EazyWizy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EazyWizy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EazyWizy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EazyWizy] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EazyWizy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EazyWizy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EazyWizy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EazyWizy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EazyWizy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EazyWizy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EazyWizy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EazyWizy] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EazyWizy] SET  MULTI_USER 
GO
ALTER DATABASE [EazyWizy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EazyWizy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EazyWizy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EazyWizy] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [EazyWizy] SET DELAYED_DURABILITY = DISABLED 
GO
USE [EazyWizy]
GO
/****** Object:  Table [dbo].[BrandMaster]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMaster](
	[BrandId] [bigint] IDENTITY(1,1) NOT NULL,
	[BrandName] [nchar](50) NULL,
	[BrandDescription] [nchar](200) NULL,
	[IsActive] [bit] NULL,
	[BrandLogo] [image] NULL,
	[CreatedBy] [nchar](50) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nchar](50) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_BrandMaster] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryMaster]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryMaster](
	[CategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentCategoryId] [bigint] NULL,
	[Name] [nchar](50) NULL,
	[Description] [nchar](100) NULL,
	[ImageName] [varchar](150) NULL,
	[IsActive] [bit] NULL,
	[UpdatedBy] [nchar](15) NULL,
	[UpdatedDate] [time](7) NULL,
 CONSTRAINT [PK_CategoryMaster] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompanyBrand]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyBrand](
	[CompanyBrandId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NULL,
	[BrandId] [bigint] NULL,
 CONSTRAINT [PK_CompanyBrand] PRIMARY KEY CLUSTERED 
(
	[CompanyBrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompanyMaster]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyMaster](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NULL,
	[Description] [nchar](500) NULL,
	[Sector] [nvarchar](50) NULL,
	[CreatedBy] [nchar](50) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedBy] [nchar](50) NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_CompanyMaster] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerAddress]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAddress](
	[DelieveryAddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[AddressId] [bigint] NULL,
 CONSTRAINT [PK_CustomerAddress_1] PRIMARY KEY CLUSTERED 
(
	[DelieveryAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerOffers]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOffers](
	[CustomerOfferId] [bigint] IDENTITY(1,1) NOT NULL,
	[PropertyId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
 CONSTRAINT [PK_CustomerOffers] PRIMARY KEY CLUSTERED 
(
	[CustomerOfferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerPaymentMethods]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerPaymentMethods](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[PpaymentMethodCcode] [char](10) NOT NULL,
	[CreditCardNumber] [varchar](40) NULL,
	[PaymentMethodDetails] [char](50) NULL,
 CONSTRAINT [PK_Customer_Payment_Methods] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganisationOrPerson] [char](20) NULL,
	[OrganisationName] [varchar](40) NULL,
	[Gender] [char](1) NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleInitial] [char](1) NULL,
	[LastName] [varchar](50) NULL,
	[EmailAddress] [varchar](255) NULL,
	[LoginName] [varchar](80) NULL,
	[LoginPassword] [varchar](20) NULL,
	[PhoneNumber] [varchar](255) NULL,
	[TownCity] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DelieveryAddress]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DelieveryAddress](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [varchar](255) NULL,
	[AddressLine2] [varchar](255) NULL,
	[AddressLine3] [varchar](255) NULL,
	[AddressLine4] [varchar](255) NULL,
	[AddressContact] [nchar](14) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
 CONSTRAINT [PK_CustomerAddress] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IngredientsMaster]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IngredientsMaster](
	[IngrediantId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Description] [varchar](500) NULL,
	[Quantity] [nvarchar](150) NULL,
	[GoodFor] [nvarchar](150) NULL,
	[Benifits] [nvarchar](150) NULL,
 CONSTRAINT [PK_IngredientsMaster] PRIMARY KEY CLUSTERED 
(
	[IngrediantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceNumber] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[StatusCode] [char](10) NOT NULL,
	[InvoiceDate] [datetime] NULL,
	[InvoiceDetails] [varchar](255) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Measurement]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measurement](
	[PropertyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Unit] [nchar](50) NULL,
	[RatePrice] [decimal](18, 0) NULL,
	[UnitValue] [float] NULL,
 CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeasurementUnits]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementUnits](
	[UnitId] [nchar](50) NOT NULL,
	[Name] [nchar](100) NULL,
 CONSTRAINT [PK_MeasurementUnits] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Offer]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[PropertyId] [bigint] IDENTITY(1,1) NOT NULL,
	[IdentifierId] [bigint] NULL,
	[Value] [nchar](100) NULL,
	[EffectiveStartDate] [date] NULL,
	[EffectiveEndDate] [date] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OfferIdentifierMaster]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferIdentifierMaster](
	[IdentifierId] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [nchar](100) NULL,
 CONSTRAINT [PK_OfferIdentifierMaster] PRIMARY KEY CLUSTERED 
(
	[IdentifierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[OrderId] [int] NOT NULL,
	[OrderItemStatusCode] [char](10) NOT NULL,
	[OrderItemQuantity] [varchar](50) NULL,
	[OrderItemPrice] [money] NULL,
	[OtherOrderItemDetails] [varchar](255) NULL,
 CONSTRAINT [PK_Order_Items] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[orderId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[OrderStatusCode] [char](10) NOT NULL,
	[DateOrderPlaced] [datetime] NOT NULL,
	[OrderDetails] [varchar](255) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [bigint] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentAmount] [money] NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBrand](
	[ProductBrandId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[BrandId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductBrand] PRIMARY KEY CLUSTERED 
(
	[ProductBrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[CategoryId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductIngredients]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductIngredients](
	[ProductIngredientId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[IngredientId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProductIngredients] PRIMARY KEY CLUSTERED 
(
	[ProductIngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductProperty]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductProperty](
	[PropertyId] [bigint] IDENTITY(1,1) NOT NULL,
	[UnitOfMeasurement] [nvarchar](50) NULL,
	[Price] [decimal](5, 2) NULL,
	[UnitValue] [decimal](5, 2) NULL,
	[ProductId] [bigint] NULL,
 CONSTRAINT [PK_ProductProperty] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[TypeCode] [char](15) NOT NULL,
	[Name] [varchar](80) NULL,
	[Price] [money] NULL,
	[Color] [varchar](20) NULL,
	[Size] [varchar](20) NULL,
	[Image] [image] NULL,
	[ImageName] [nvarchar](250) NULL,
	[Description] [varchar](255) NULL,
	[BuyCounter] [nchar](10) NULL,
	[OtherProductPetails] [varchar](255) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Property]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property](
	[PropertyId] [bigint] IDENTITY(1,1) NOT NULL,
	[PropertyTypeId] [bigint] NULL,
	[ProdcutId] [bigint] NOT NULL,
 CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyType]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyType](
	[PropertyTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NULL,
	[Discription] [nchar](100) NULL,
	[IsTrait] [bit] NULL,
 CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED 
(
	[PropertyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RefGenderCodes]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefGenderCodes](
	[GenderCode] [char](1) NOT NULL,
	[GenderDescription] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Gender_Codes] PRIMARY KEY CLUSTERED 
(
	[GenderCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefInvoiceStatusCodes]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefInvoiceStatusCodes](
	[InvoiceStatusCode] [char](10) NOT NULL,
	[InvoiceStatusDescription] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Invoice_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[InvoiceStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefOrderItemStatusCodes]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefOrderItemStatusCodes](
	[OrderItemStatusCode] [char](10) NOT NULL,
	[OrderItemStatusDescription] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Order_Item_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[OrderItemStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefOrderStatusCodes]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefOrderStatusCodes](
	[OrderStatusCode] [char](10) NOT NULL,
	[OrderStatusDescription] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Order_Status_Codes] PRIMARY KEY CLUSTERED 
(
	[OrderStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RefPaymentMethods]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RefPaymentMethods](
	[PaymentMethodCode] [char](10) NOT NULL,
	[PaymentMethodDescription] [varchar](80) NULL,
 CONSTRAINT [PK_Ref_Payment_Methods] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShipmentItems]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentItems](
	[ShipmentId] [bigint] NOT NULL,
	[OrderItemId] [bigint] NOT NULL,
 CONSTRAINT [PK_Shipment_Items] PRIMARY KEY CLUSTERED 
(
	[ShipmentId] ASC,
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipments](
	[ShipmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[InvoiceNumber] [bigint] NOT NULL,
	[ShipmentTrackingNumber] [varchar](80) NULL,
	[ShipmentDate] [datetime] NULL,
	[OtherShipmentDetails] [varchar](255) NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SpecialProperty]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialProperty](
	[PropertyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [nchar](50) NULL,
	[Description] [nchar](100) NULL,
	[ProductID] [nchar](10) NULL,
	[TypeId] [bigint] NULL,
 CONSTRAINT [PK_SpecialProperty] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CustomerAddress]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddress_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerAddress] CHECK CONSTRAINT [FK_CustomerAddress_Customers]
GO
ALTER TABLE [dbo].[CustomerAddress]  WITH CHECK ADD  CONSTRAINT [FK_CustomerAddress_DelieveryAddress] FOREIGN KEY([AddressId])
REFERENCES [dbo].[DelieveryAddress] ([AddressId])
GO
ALTER TABLE [dbo].[CustomerAddress] CHECK CONSTRAINT [FK_CustomerAddress_DelieveryAddress]
GO
ALTER TABLE [dbo].[CustomerOffers]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOffers_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerOffers] CHECK CONSTRAINT [FK_CustomerOffers_Customers]
GO
ALTER TABLE [dbo].[CustomerOffers]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOffers_Offer] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Offer] ([PropertyId])
GO
ALTER TABLE [dbo].[CustomerOffers] CHECK CONSTRAINT [FK_CustomerOffers_Offer]
GO
ALTER TABLE [dbo].[CustomerPaymentMethods]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPaymentMethods_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerPaymentMethods] CHECK CONSTRAINT [FK_CustomerPaymentMethods_Customers]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [Invoice_Status_Codes_Invoices] FOREIGN KEY([StatusCode])
REFERENCES [dbo].[RefInvoiceStatusCodes] ([InvoiceStatusCode])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [Invoice_Status_Codes_Invoices]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [Orders_Invoices] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([orderId])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [Orders_Invoices]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [FK_Offer_OfferIdentifierMaster] FOREIGN KEY([IdentifierId])
REFERENCES [dbo].[OfferIdentifierMaster] ([IdentifierId])
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [FK_Offer_OfferIdentifierMaster]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [Products_Order_Items] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [Products_Order_Items]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Customers_1_Orders] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Customers_1_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Order_Status_Codes_Orders] FOREIGN KEY([OrderStatusCode])
REFERENCES [dbo].[RefOrderStatusCodes] ([OrderStatusCode])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Order_Status_Codes_Orders]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [Invoices_Payments] FOREIGN KEY([InvoiceNumber])
REFERENCES [dbo].[Invoices] ([InvoiceNumber])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [Invoices_Payments]
GO
ALTER TABLE [dbo].[ProductBrand]  WITH CHECK ADD  CONSTRAINT [FK_ProductBrand_BrandMaster] FOREIGN KEY([BrandId])
REFERENCES [dbo].[BrandMaster] ([BrandId])
GO
ALTER TABLE [dbo].[ProductBrand] CHECK CONSTRAINT [FK_ProductBrand_BrandMaster]
GO
ALTER TABLE [dbo].[ProductBrand]  WITH CHECK ADD  CONSTRAINT [FK_ProductBrand_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductBrand] CHECK CONSTRAINT [FK_ProductBrand_Products]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_CategoryMaster] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CategoryMaster] ([CategoryId])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_CategoryMaster]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Products]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_Measurement] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Measurement] ([PropertyId])
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_Measurement]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_Offer] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Offer] ([PropertyId])
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_Offer]
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD  CONSTRAINT [FK_Property_PropertyType] FOREIGN KEY([PropertyTypeId])
REFERENCES [dbo].[PropertyType] ([PropertyTypeId])
GO
ALTER TABLE [dbo].[Property] CHECK CONSTRAINT [FK_Property_PropertyType]
GO
ALTER TABLE [dbo].[ShipmentItems]  WITH CHECK ADD  CONSTRAINT [Order_Items_Shipment_Items] FOREIGN KEY([OrderItemId])
REFERENCES [dbo].[OrderItem] ([OrderItemId])
GO
ALTER TABLE [dbo].[ShipmentItems] CHECK CONSTRAINT [Order_Items_Shipment_Items]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [Invoices_Shipments] FOREIGN KEY([InvoiceNumber])
REFERENCES [dbo].[Invoices] ([InvoiceNumber])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [Invoices_Shipments]
GO
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [Orders_Shipments] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([orderId])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [Orders_Shipments]
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 18-05-2014 00:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategories] 
 @ParentCategory bigint

AS

 SET NOCOUNT ON;

 IF @ParentCategory IS NULL
	SET @ParentCategory=0

    SELECT [CategoryId], [Name], [Description], [IsActive], [ImageName]
    FROM CategoryMaster
    WHERE ParentCategoryId = @ParentCategory



GO
USE [master]
GO
ALTER DATABASE [EazyWizy] SET  READ_WRITE 
GO
