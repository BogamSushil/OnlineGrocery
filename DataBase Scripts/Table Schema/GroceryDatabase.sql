
/* ---------------------------------------------------------------------- */
/* Add table "Invoices"                                                   */
/* ---------------------------------------------------------------------- */


/****** Object:  Table [dbo].[Role]    Script Date: 4/3/2014 10:10:18 PM ******/
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


/****** Object:  Table [dbo].[User]    Script Date: 4/3/2014 10:34:11 PM ******/
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

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleID]
GO



CREATE TABLE [Invoices] (
    [invoice_number] INTEGER IDENTITY(1,1) NOT NULL,
    [order_id] INTEGER NOT NULL,
    [invoice_status_code] CHAR(10) NOT NULL,
    [invoice_date] DATETIME,
    [invoice_details] VARCHAR(255),
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([invoice_number])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Orders"                                                     */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Orders] (
    [order_id] INTEGER IDENTITY(1,1) NOT NULL,
    [customer_id] INTEGER NOT NULL,
    [order_status_code] CHAR(10) NOT NULL,
    [date_order_placed] DATETIME NOT NULL,
    [order_details] VARCHAR(255),
    CONSTRAINT [PK_Orders] PRIMARY KEY ([order_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Ref_Order_Status_Codes"                                     */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Order_Status_Codes] (
    [order_status_code] CHAR(10) NOT NULL,
    [order_status_description] VARCHAR(80),
     CONSTRAINT [PK_Ref_Order_Status_Codes] PRIMARY KEY ([order_status_code])
)
GO

/*    [- eg Cancelled, Completed] DECIMAL, */

/* ---------------------------------------------------------------------- */
/* Add table "Ref_Order_Item_Status_Codes"                                */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Order_Item_Status_Codes] (
    [order_item_status_code] CHAR(10) NOT NULL,
    [order_item_status_description] VARCHAR(80),
    CONSTRAINT [PK_Ref_Order_Item_Status_Codes] PRIMARY KEY ([order_item_status_code])
)
GO

/*    [- eg Delivered, Out of Stock] VARCHAR(10), */

/* ---------------------------------------------------------------------- */
/* Add table "Shipments"                                                  */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Shipments] (
    [shipment_id] INTEGER IDENTITY(1,1) NOT NULL,
    [order_id] INTEGER NOT NULL,
    [invoice_number] INTEGER NOT NULL,
    [shipment_tracking_number] VARCHAR(80),
    [shipment_date] DATETIME,
    [other_shipment_details] VARCHAR(255),
    CONSTRAINT [PK_Shipments] PRIMARY KEY ([shipment_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Shipment_Items"                                             */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Shipment_Items] (
    [shipment_id] INTEGER NOT NULL,
    [order_item_id] INTEGER NOT NULL,
    CONSTRAINT [PK_Shipment_Items] PRIMARY KEY ([shipment_id], [order_item_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Order_Items"                                                */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Order_Items] (
    [order_item_id] INTEGER IDENTITY(1,1) NOT NULL,
    [product_id] INTEGER NOT NULL,
    [order_id] INTEGER NOT NULL,
    [order_item_status_code] CHAR(10) NOT NULL,
    [order_item_quantity] VARCHAR(50),
    [order_item_price] MONEY,
    [other_order_item_details] VARCHAR(255),
    CONSTRAINT [PK_Order_Items] PRIMARY KEY ([order_item_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Products"                                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Products] (
    [product_id] INTEGER IDENTITY(1,1) NOT NULL,
    [product_type_code] CHAR(15) NOT NULL,
    [product_name] VARCHAR(80),
    [product_price] MONEY,
    [product_color] VARCHAR(20),
    [product_size] VARCHAR(20),	
	[productImageName] nvarchar(250) NULL,
    [product_description] VARCHAR(255),
    [other_product_details] VARCHAR(255),
    CONSTRAINT [PK_Products] PRIMARY KEY ([product_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Customers"                                                  */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Customers] (
    [customer_id] INTEGER IDENTITY(1,1) NOT NULL,
    [organisation_or_person] CHAR(20),
    [organisation_name] VARCHAR(40),
    [gender] CHAR(1),
    [first_name] VARCHAR(50),
    [middle_initial] CHAR(1),
    [last_name] VARCHAR(50),
    [email_address] VARCHAR(255),
    [login_name] VARCHAR(80),
    [login_password] VARCHAR(20),
    [phone_number] VARCHAR(255),
    [address_line_1] VARCHAR(255),
    [address_line_2] VARCHAR(255),
    [address_line_3] VARCHAR(255),
    [address_line_4] VARCHAR(80),
    [town_city] VARCHAR(50),
    [county] VARCHAR(50),
    [country] VARCHAR(50),
    CONSTRAINT [PK_Customers] PRIMARY KEY ([customer_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Ref_Gender_Codes"                                           */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Gender_Codes] (
    [gender_code] CHAR(1) NOT NULL,
    [gender_description] VARCHAR(80),
     CONSTRAINT [PK_Ref_Gender_Codes] PRIMARY KEY ([gender_code])
)
GO

/*   [- eg Male, Female, Unknown.] DECIMAL, */


/* ---------------------------------------------------------------------- */
/* Add table "Ref_Payment_Methods"                                        */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Payment_Methods] (
    [payment_method_code] CHAR(10) NOT NULL,
    [payment_method_description] VARCHAR(80),
     CONSTRAINT [PK_Ref_Payment_Methods] PRIMARY KEY ([payment_method_code])
)
GO

/*  [- eg CC=Credit Card.] DECIMAL, */

/* ---------------------------------------------------------------------- */
/* Add table "Customer_Payment_Methods"                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Customer_Payment_Methods] (
    [customer_payment_id] INTEGER IDENTITY(1,1) NOT NULL,
    [customer_id] INTEGER NOT NULL,
    [payment_method_code] CHAR(10) NOT NULL,
    [credit_card_number] VARCHAR(40),
    [payment_method_details] CHAR(50) NULL,
    CONSTRAINT [PK_Customer_Payment_Methods] PRIMARY KEY ([customer_payment_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Ref_Invoice_Status_Codes"                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Invoice_Status_Codes] (
    [invoice_status_code] CHAR(10) NOT NULL,
    [invoice_status_description] VARCHAR(80),
    CONSTRAINT [PK_Ref_Invoice_Status_Codes] PRIMARY KEY ([invoice_status_code])
)
GO

/*    [- eg Issued, Paid.] VARCHAR(10), */


/* ---------------------------------------------------------------------- */
/* Add table "Payments"                                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Payments] (
    [payment_id] INTEGER IDENTITY(1,1) NOT NULL,
    [invoice_number] INTEGER NOT NULL,
    [payment_date] DATETIME,
    [payment_amount] MONEY,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([payment_id])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Product_Prices"                                             */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Product_Prices] (
    [product_id] INTEGER NOT NULL,
    [product_type_code] CHAR(15) NOT NULL,
    [product_price] MONEY,
    CONSTRAINT [PK_Product_Prices] PRIMARY KEY ([product_id], [product_type_code])
)
GO

/* ---------------------------------------------------------------------- */
/* Add table "Ref_Product_Types"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE [Ref_Product_Types] (
    [product_type_code] CHAR(15) NOT NULL,
    [parent_product_type_code] CHAR(15),
    [product_type_description] VARCHAR(80),
	[productTypeImageName] nvarchar(250) NULL,
    CONSTRAINT [PK_Ref_Product_Types] PRIMARY KEY ([product_type_code])
)
GO

/*   [eg Book, CD] VARCHAR(1), */

/* ---------------------------------------------------------------------- */
/* Foreign key constraints                                                */
/* ---------------------------------------------------------------------- */

ALTER TABLE [Invoices] ADD CONSTRAINT [Invoice_Status_Codes_Invoices] 
    FOREIGN KEY ([invoice_status_code]) REFERENCES [Ref_Invoice_Status_Codes] ([invoice_status_code])
GO

ALTER TABLE [Invoices] ADD CONSTRAINT [Orders_Invoices] 
    FOREIGN KEY ([order_id]) REFERENCES [Orders] ([order_id])
GO

ALTER TABLE [Orders] ADD CONSTRAINT [Order_Status_Codes_Orders] 
    FOREIGN KEY ([order_status_code]) REFERENCES [Ref_Order_Status_Codes] ([order_status_code])
GO

ALTER TABLE [Orders] ADD CONSTRAINT [Customers_1_Orders] 
    FOREIGN KEY ([customer_id]) REFERENCES [Customers] ([customer_id])
GO

ALTER TABLE [Shipments] ADD CONSTRAINT [Orders_Shipments] 
    FOREIGN KEY ([order_id]) REFERENCES [Orders] ([order_id])
GO

ALTER TABLE [Shipments] ADD CONSTRAINT [Invoices_Shipments] 
    FOREIGN KEY ([invoice_number]) REFERENCES [Invoices] ([invoice_number])
GO

ALTER TABLE [Shipment_Items] ADD CONSTRAINT [Shipments_Shipment_Items] 
    FOREIGN KEY ([shipment_id]) REFERENCES [Shipments] ([shipment_id])
GO

ALTER TABLE [Shipment_Items] ADD CONSTRAINT [Order_Items_Shipment_Items] 
    FOREIGN KEY ([order_item_id]) REFERENCES [Order_Items] ([order_item_id])
GO

ALTER TABLE [Order_Items] ADD CONSTRAINT [Order_Item_Status_Order_Items] 
    FOREIGN KEY ([order_item_status_code]) REFERENCES [Ref_Order_Item_Status_Codes] ([order_item_status_code])
GO

ALTER TABLE [Order_Items] ADD CONSTRAINT [Products_Order_Items] 
    FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

ALTER TABLE [Order_Items] ADD CONSTRAINT [Orders_Order_Items] 
    FOREIGN KEY ([order_id]) REFERENCES [Orders] ([order_id])
GO

ALTER TABLE [Products] ADD CONSTRAINT [Ref_Product_Types_Products] 
    FOREIGN KEY ([product_type_code]) REFERENCES [Ref_Product_Types] ([product_type_code])
GO

ALTER TABLE [Customer_Payment_Methods] ADD CONSTRAINT [Customers_Customer_Payment_Methods] 
    FOREIGN KEY ([customer_id]) REFERENCES [Customers] ([customer_id])
GO

ALTER TABLE [Customer_Payment_Methods] ADD CONSTRAINT [Ref_Payment_Methods_Customer_Payment_Methods] 
    FOREIGN KEY ([payment_method_code]) REFERENCES [Ref_Payment_Methods] ([payment_method_code])
GO

ALTER TABLE [Payments] ADD CONSTRAINT [Invoices_Payments] 
    FOREIGN KEY ([invoice_number]) REFERENCES [Invoices] ([invoice_number])
GO

ALTER TABLE [Product_Prices] ADD CONSTRAINT [Products_Product_Prices] 
    FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

ALTER TABLE [Product_Prices] ADD CONSTRAINT [Ref_Art_Types_Product_Prices] 
    FOREIGN KEY ([product_type_code]) REFERENCES [Ref_Product_Types] ([product_type_code])
GO

ALTER TABLE [Ref_Product_Types] ADD CONSTRAINT [Ref_Product_Types_Ref_Product_Types] 
    FOREIGN KEY ([parent_product_type_code]) REFERENCES [Ref_Product_Types] ([product_type_code])
GO


/* Load sample Reference Data */

INSERT INTO Ref_Invoice_Status_Codes (invoice_status_code,invoice_status_description)
VALUES                               ('Issued','Issued')
GO

INSERT INTO Ref_Invoice_Status_Codes (invoice_status_code,invoice_status_description)
VALUES                               ('Paid','Paid')
GO

SELECT 'Ref_Invoice_Status_Codes' As Table_Name, *
FROM    Ref_Invoice_Status_Codes
GO

INSERT INTO Ref_Order_Item_Status_Codes (order_Item_status_code,order_Item_status_description)
VALUES                                  ('DEL','Delivered')
GO
 
INSERT INTO Ref_Order_Item_Status_Codes (order_Item_status_code,order_Item_status_description)
VALUES                                  ('OUT','Out of Stock')
GO

INSERT INTO Ref_Order_Item_Status_Codes (order_Item_status_code,order_Item_status_description)
VALUES                                  ('ROUTE','En Route')
GO

INSERT INTO Ref_Order_Item_Status_Codes (order_Item_status_code,order_Item_status_description)
VALUES                                  ('WAIT','Waiting')
GO

SELECT 'Ref_Order_Item_Status_Code' As Table_Name, *
FROM    Ref_Order_Item_Status_Codes
GO


INSERT INTO Ref_Order_Status_Codes (order_status_code,order_status_description)
VALUES                            ('CANC','Cancelled')
GO
 
INSERT INTO Ref_Order_Status_Codes (order_status_code,order_status_description)
VALUES                            ('COMPL','Completed')
GO

INSERT INTO Ref_Order_Status_Codes (order_status_code,order_status_description)
VALUES                            ('OPEN','Open - eg just placed')
GO

INSERT INTO Ref_Order_Status_Codes (order_status_code,order_status_description)
VALUES                            ('PROV','Provisional')
GO

SELECT 'Ref_Order_Status_Code' As Table_Name, *
FROM    Ref_Order_Status_Codes
GO

INSERT INTO Ref_Payment_Methods (payment_method_code,payment_method_description)
VALUES                          ('AMEX','American Express')
GO

INSERT INTO Ref_Payment_Methods (payment_method_code,payment_method_description)
VALUES                          ('CC','Other Credit Card')
GO
 
INSERT INTO Ref_Payment_Methods (payment_method_code,payment_method_description)
VALUES                          ('CASH','Cash')
GO

INSERT INTO Ref_Payment_Methods (payment_method_code,payment_method_description)
VALUES                          ('DD','Direct Debit')
GO

SELECT 'Ref_Payment_Methods' As Table_Name, *
FROM    Ref_Payment_Methods
GO


INSERT INTO Ref_Product_Types (product_type_code,parent_product_type_code,product_type_description)
VALUES                        ('Book'           ,NULL                    ,'Book'                  )
GO
 
INSERT INTO Ref_Product_Types (product_type_code,parent_product_type_code,product_type_description)
VALUES                        ('Camera'         ,NULL                    ,'Camera'                )
GO

INSERT INTO Ref_Product_Types (product_type_code,parent_product_type_code,product_type_description)
VALUES                        ('Digital Camera' ,'Camera'                ,'Digital Camera'        )
GO

INSERT INTO Ref_Product_Types (product_type_code,parent_product_type_code,product_type_description)
VALUES                        ('CD'             ,NULL                    ,'CD'                    )
GO

SELECT 'Ref_Product_Types' As Table_Name, *
FROM    Ref_Product_Types
GO


/* Load sample Product Data */
INSERT INTO Products (product_type_code,product_name           ,product_price,product_color,product_size,product_description,other_product_details)
VALUES               ('Digital Camera' ,'Olympus Camedia C-170',64.97        ,'Silver'     ,NULL        ,'Olympus C-170 Digital Camera',NULL      )
GO

INSERT INTO Products (product_type_code,product_name           ,product_price,product_color,product_size,product_description,other_product_details)
VALUES               ('Digital Camera' ,'Pentax Opto 50L'      ,89.97        ,'Rose'      ,NULL        ,'Pentax Opto 50L Digital Camera',NULL     )
GO

INSERT INTO Products (product_type_code,product_name            ,product_price,product_color,product_size,product_description     ,other_product_details)
VALUES               ('Digital Camera' ,'Nikon Coolpix L3 Black',99.99        ,'Black'      ,NULL        ,'Nikon Coolpix L3 Black',NULL                 )
GO

SELECT 'Products' As Table_Name, *
FROM    Products
GO


/* Load sample Customers Data */
INSERT INTO Customers (first_name,middle_initial,last_name,email_address                
		      ,address_line_1           ,address_line_2             ,address_line_3,address_line_4,town_city,county,country)
VALUES                ('John'    ,NULL          ,'Doe'    ,'john.doe@fictitiousmail.com'
 		      ,'1500 E MAIN AVE STE 201','SPRINGFIELD VA 22162-1010',NULL          ,NULL          ,NULL     ,NULL  ,NULL   )
GO

INSERT INTO Customers (first_name,middle_initial,last_name,email_address                
		      ,address_line_1           ,address_line_2             ,address_line_3,address_line_4,town_city,county          ,country)
VALUES                ('Joe '    ,NULL          ,'Bloggs'                   ,'joe.bloggs@fictitiousmail.com'
 		      ,'1776 New Cavendish Street','Marylebone'             ,NULL          ,'W11X 5BY'    ,'London' ,'Greater London','UK'   )
GO

SELECT 'Customers' As Table_Name,*
FROm    Customers
GO


INSERT INTO Customer_Payment_Methods (customer_id,payment_method_code,credit_card_number,payment_method_details         )
VALUES                               (1          ,'AMEX'             ,'123456'          ,'From 01/01/2004 to 01/01/2008')
GO

INSERT INTO Customer_Payment_Methods (customer_id,payment_method_code,credit_card_number,payment_method_details)
VALUES                               (2          ,'CASH'             ,NULL              ,NULL                  )
GO

SELECT 'Customer_Payment_Methods' As Table_Name,* 
FROM    Customer_Payment_Methods 
GO

INSERT INTO Orders (customer_id,order_status_code,date_order_placed,order_details                    )
VALUES             (1          ,'OPEN'           ,'01/01/2007'     ,'First Order from a new Customer')
GO

SELECT 'Orders' As Table_Name,* 
FROM    Orders 
GO


INSERT INTO Order_Items (product_id,order_id, order_item_status_code,order_item_quantity,order_item_price,other_order_item_details)
VALUES                  (1         ,1       ,'DEL'                 ,1                   ,100.00          ,NULL                    )
GO

INSERT INTO Order_Items (product_id,order_id, order_item_status_code,order_item_quantity,order_item_price,other_order_item_details)
VALUES                  (2         ,1       ,'ROUTE'               ,2                   ,200.00          ,'A Rare Groove'         )
GO

INSERT INTO Order_Items (product_id,order_id, order_item_status_code,order_item_quantity,order_item_price,other_order_item_details)
VALUES                  (3         ,1       ,'WAIT'                ,3                   ,300.00          ,'The usual Order'       )
GO

SELECT 'Order_Items' As Table_Name,* 
FROM    Order_Items 
GO

INSERT INTO Invoices (order_id, invoice_status_code, invoice_date, invoice_details                      )
VALUES               (1       ,'Paid'              ,'01/01/2007','Single Invoice for the complete Order')
GO

SELECT 'Invoices' As Table_Name,* 
FROM    Invoices 
GO

INSERT INTO Payments (invoice_number, payment_date, payment_amount)
VALUES               (1             ,'01/01/2007' , 600.00        )
GO

SELECT 'Payments' As Table_Name,* 
FROM    Payments 
GO

INSERT INTO Shipments (order_id, invoice_number, shipment_tracking_number,shipment_date,other_shipment_details)
VALUES                (1       , 1             ,'123456'                 ,'01/01/2007' , NULL                 )
GO

SELECT 'Shipments' As Table_Name,* 
FROM    Shipments 
GO

INSERT INTO Shipment_Items (shipment_id,order_item_id)
VALUES                     (1          ,1            )
GO

INSERT INTO Shipment_Items (shipment_id,order_item_id)
VALUES                     (1          ,2            )
GO

INSERT INTO Shipment_Items (shipment_id,order_item_id)
VALUES                     (1          ,3            )
GO

SELECT 'Shipment_Items' As Table_Name,* 
FROM    Shipment_Items 
GO


