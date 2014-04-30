
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Book           ', NULL, N'Book Category', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Camera         ', NULL, N'Camera', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'CD             ', NULL, N'CD', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Digital Camera ', N'Camera         ', N'Digital Camera', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Food           ', NULL, N'Food', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'Fruit          ', NULL, N'Fruit', NULL)
INSERT [dbo].[Ref_Product_Types] ([product_type_code], [parent_product_type_code], [product_type_description], [productTypeImageName]) VALUES (N'NewCode        ', NULL, N'description', N'imagename')
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (1, N'Digital Camera ', N'Olympus Camedia C-170', 64.9700, N'Silver', NULL, NULL, N'Olympus C-170 Digital Camera', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (2, N'Digital Camera ', N'Pentax Opto 50L', 89.9700, N'Rose', NULL, NULL, N'Pentax Opto 50L Digital Camera', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (3, N'Digital Camera ', N'Nikon Coolpix L3 Black', 99.9900, N'Black', NULL, NULL, N'Nikon Coolpix L3 Black', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (4, N'Fruit          ', N'Mango', 100.0000, N'Yello', NULL, N'Mango.png', N'Mango ', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (5, N'Fruit          ', N'Apple', 80.0000, N'REd', NULL, N'Apple.png', N'Apple', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (6, N'Fruit          ', N'Banana', 10.0000, N'Yello', NULL, N'Banana.png', N'Banana', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (7, N'Fruit          ', N'Orange', 60.0000, N'Orange', NULL, N'Orange.png', N'Orange', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (8, N'Fruit          ', N'Watermelon', 50.0000, N'Green', NULL, N'Watermelon.png', N'Watermelon', NULL)
INSERT [dbo].[Products] ([product_id], [product_type_code], [product_name], [product_price], [product_color], [product_size], [productImageName], [product_description], [other_product_details]) VALUES (9, N'Fruit          ', N'Muskmelon', 60.0000, N'Orange', NULL, N'Muskmelon.png', N'Muskmelon', NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([customer_id], [organisation_or_person], [organisation_name], [gender], [first_name], [middle_initial], [last_name], [email_address], [login_name], [login_password], [phone_number], [address_line_1], [address_line_2], [address_line_3], [address_line_4], [town_city], [county], [country]) VALUES (1, NULL, NULL, NULL, N'John', NULL, N'Doe', N'john.doe@fictitiousmail.com', NULL, NULL, NULL, N'1500 E MAIN AVE STE 201', N'SPRINGFIELD VA 22162-1010', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Customers] ([customer_id], [organisation_or_person], [organisation_name], [gender], [first_name], [middle_initial], [last_name], [email_address], [login_name], [login_password], [phone_number], [address_line_1], [address_line_2], [address_line_3], [address_line_4], [town_city], [county], [country]) VALUES (2, NULL, NULL, NULL, N'Joe ', NULL, N'Bloggs', N'joe.bloggs@fictitiousmail.com', NULL, NULL, NULL, N'1776 New Cavendish Street', N'Marylebone', NULL, N'W11X 5BY', N'London', N'Greater London', N'UK')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleID], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [FirstName], [LastName], [EmailID], [Password], [HashId], [IsActive], [RoleID], [CreatedBy], [CreateDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'sysadmin', N'System', N'Admin', N'sysadmin@easywizy.com', N'OYPx08RjrE6p+y4tM3pfli1dzZ4=', N'061b1639-47cd-4cbe-abae-84c5e0853ae9', 1, 1, N'sa', CAST(0x0000A30301270FA3 AS DateTime), N'sa', CAST(0x0000A30301270FA3 AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'CANC      ', N'Cancelled')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'COMPL     ', N'Completed')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'OPEN      ', N'Open - eg just placed')
INSERT [dbo].[Ref_Order_Status_Codes] ([order_status_code], [order_status_description]) VALUES (N'PROV      ', N'Provisional')
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([order_id], [customer_id], [order_status_code], [date_order_placed], [order_details]) VALUES (1, 1, N'OPEN      ', CAST(0x000098A900000000 AS DateTime), N'First Order from a new Customer')
SET IDENTITY_INSERT [dbo].[Orders] OFF
INSERT [dbo].[Ref_Invoice_Status_Codes] ([invoice_status_code], [invoice_status_description]) VALUES (N'Issued    ', N'Issued')
INSERT [dbo].[Ref_Invoice_Status_Codes] ([invoice_status_code], [invoice_status_description]) VALUES (N'Paid      ', N'Paid')
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([invoice_number], [order_id], [invoice_status_code], [invoice_date], [invoice_details]) VALUES (1, 1, N'Paid      ', CAST(0x000098A900000000 AS DateTime), N'Single Invoice for the complete Order')
SET IDENTITY_INSERT [dbo].[Invoices] OFF
SET IDENTITY_INSERT [dbo].[Shipments] ON 

INSERT [dbo].[Shipments] ([shipment_id], [order_id], [invoice_number], [shipment_tracking_number], [shipment_date], [other_shipment_details]) VALUES (1, 1, 1, N'123456', CAST(0x000098A900000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Shipments] OFF
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([payment_id], [invoice_number], [payment_date], [payment_amount]) VALUES (1, 1, CAST(0x000098A900000000 AS DateTime), 600.0000)
SET IDENTITY_INSERT [dbo].[Payments] OFF
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'DEL       ', N'Delivered')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'OUT       ', N'Out of Stock')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'ROUTE     ', N'En Route')
INSERT [dbo].[Ref_Order_Item_Status_Codes] ([order_item_status_code], [order_item_status_description]) VALUES (N'WAIT      ', N'Waiting')
SET IDENTITY_INSERT [dbo].[Order_Items] ON 

INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (1, 1, 1, N'DEL       ', N'1', 100.0000, NULL)
INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (2, 2, 1, N'ROUTE     ', N'2', 200.0000, N'A Rare Groove')
INSERT [dbo].[Order_Items] ([order_item_id], [product_id], [order_id], [order_item_status_code], [order_item_quantity], [order_item_price], [other_order_item_details]) VALUES (3, 3, 1, N'WAIT      ', N'3', 300.0000, N'The usual Order')
SET IDENTITY_INSERT [dbo].[Order_Items] OFF
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 1)
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 2)
INSERT [dbo].[Shipment_Items] ([shipment_id], [order_item_id]) VALUES (1, 3)
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'AMEX      ', N'American Express')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'CASH      ', N'Cash')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'CC        ', N'Other Credit Card')
INSERT [dbo].[Ref_Payment_Methods] ([payment_method_code], [payment_method_description]) VALUES (N'DD        ', N'Direct Debit')
SET IDENTITY_INSERT [dbo].[Customer_Payment_Methods] ON 

INSERT [dbo].[Customer_Payment_Methods] ([customer_payment_id], [customer_id], [payment_method_code], [credit_card_number], [payment_method_details]) VALUES (1, 1, N'AMEX      ', N'123456', N'From 01/01/2004 to 01/01/2008                     ')
INSERT [dbo].[Customer_Payment_Methods] ([customer_payment_id], [customer_id], [payment_method_code], [credit_card_number], [payment_method_details]) VALUES (2, 2, N'CASH      ', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Customer_Payment_Methods] OFF
SET IDENTITY_INSERT [dbo].[BrandMaster] ON 

INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (1, N'ITC', 1, CAST(0x0000A3170140EDEE AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (2, N'Hul', 1, CAST(0x0000A3170140EDEE AS DateTime), NULL, NULL, 2, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (3, N'Tata', 1, CAST(0x0000A3170140EDEF AS DateTime), NULL, NULL, 3, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (4, N'Fena', 1, CAST(0x0000A3170140EDF0 AS DateTime), NULL, NULL, 4, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (5, N'Emami', 1, CAST(0x0000A3170140EDF1 AS DateTime), NULL, NULL, 5, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (6, N'Tata', 1, CAST(0x0000A3170140EDF1 AS DateTime), NULL, NULL, 6, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (7, N'Fena', 1, CAST(0x0000A3170140EDF1 AS DateTime), NULL, NULL, 7, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (8, N'Emami', 1, CAST(0x0000A3170140EDF1 AS DateTime), NULL, NULL, 8, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (9, N'Tata', 1, CAST(0x0000A3170140EDF1 AS DateTime), NULL, NULL, 9, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (10, N'ITC', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 10, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (11, N'Hul', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 11, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (12, N'Tata', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 12, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (13, N'Fena', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 13, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (14, N'Emami', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 14, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (15, N'Emami', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 15, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (16, N'Tata', 1, CAST(0x0000A3170140EDF2 AS DateTime), NULL, NULL, 16, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (17, N'Fena', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 17, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (18, N'Emami', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 18, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (19, N'Tata', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 19, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (20, N'Fena', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 20, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (21, N'Emami', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 21, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (22, N'Tata', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 22, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (23, N'Fena', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 23, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (24, N'Emami', 1, CAST(0x0000A3170140EDF3 AS DateTime), NULL, NULL, 24, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (25, N'Tata', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 25, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (26, N'Emami', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 26, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (27, N'Tata', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 27, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (28, N'Fena', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 28, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (29, N'Emami', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 29, 1)
INSERT [dbo].[BrandMaster] ([idBrand], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (30, N'Tata', 1, CAST(0x0000A3170140EDF4 AS DateTime), NULL, NULL, 30, 1)
SET IDENTITY_INSERT [dbo].[BrandMaster] OFF
SET IDENTITY_INSERT [dbo].[LHSMenu] ON 

INSERT [dbo].[LHSMenu] ([IdLHSMenu], [Name], [Image], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (1, N'Categories', N'category.png', 1, CAST(0x0000A3170140EDE5 AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[LHSMenu] ([IdLHSMenu], [Name], [Image], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (2, N'Brand', N'brand.png', 1, CAST(0x0000A3170140EDED AS DateTime), NULL, NULL, 2, 1)
INSERT [dbo].[LHSMenu] ([IdLHSMenu], [Name], [Image], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (3, N'Exotic Fruits', N'fruits.png', 1, CAST(0x0000A3170140EDED AS DateTime), NULL, NULL, 3, 1)
INSERT [dbo].[LHSMenu] ([IdLHSMenu], [Name], [Image], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (4, N'Exotic Vegetables', N'Vegetables.png', 1, CAST(0x0000A3170140EDED AS DateTime), NULL, NULL, 4, 1)
INSERT [dbo].[LHSMenu] ([IdLHSMenu], [Name], [Image], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (5, N'Stationary', N'Stationary.png', 1, CAST(0x0000A3170140EDEE AS DateTime), NULL, NULL, 5, 1)
SET IDENTITY_INSERT [dbo].[LHSMenu] OFF
SET IDENTITY_INSERT [dbo].[TOPMENU] ON 

INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (1, N'HOME', 1, CAST(0x0000A30F01272E8C AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (2, N'Last Buy', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 2, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (3, N'Track Order', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 3, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (4, N'Sign Up', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 4, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (5, N'Customer Support', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 5, 1)
INSERT [dbo].[TOPMENU] ([IdMenu], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedTime], [SortedNumber], [IsActive]) VALUES (6, N'Log Out', 1, CAST(0x0000A30F01272EA6 AS DateTime), NULL, NULL, 6, 1)
SET IDENTITY_INSERT [dbo].[TOPMENU] OFF
