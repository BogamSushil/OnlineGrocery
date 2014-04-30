
/****** Object:  Table [dbo].[BrandMaster]    Script Date: 4/30/2014 1:00:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMaster](
	[idBrand] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[SortedNumber] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBrand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer_Payment_Methods]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Customers]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[DiscountCouponMapper]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[DiscountCouponMaster]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Invoices]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[LHSMenu]    Script Date: 4/30/2014 1:00:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LHSMenu](
	[IdLHSMenu] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Image] [varchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[SortedNumber] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdLHSMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order_Items]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Payments]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Product_Prices]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Gender_Codes]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Invoice_Status_Codes]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Order_Item_Status_Codes]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Order_Status_Codes]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Payment_Methods]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Ref_Product_Types]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Shipment_Items]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[Shipments]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[TOPMENU]    Script Date: 4/30/2014 1:00:42 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 4/30/2014 1:00:42 PM ******/
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
