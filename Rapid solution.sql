﻿Create database RapidSolution
go
use RapidSolution
go
Create table Warehouse
(
	Id int identity primary key,
	IdCode varchar(50),
	Name nvarchar(200),
	Location nvarchar(500),
	[Description] nvarchar(500),
	DateCreated datetime default getdate()
)
go
Create table Employee
(
	Id int identity primary key,
	FullName nvarchar (150),
	UserName nvarchar(150) unique,
	Pasword nvarchar(500),
	[Role] nvarchar(100),
	DateCreated DateTime default getdate(),
	BirthDate DateTime,
	Phone varchar(30),
	Email varchar(150),
	[Address] nvarchar(200),
	[Status] bit,
	WarehouseId int references Warehouse(Id)
)
go
Create table [MasterBill]
(
	Id int identity primary key,
	MasterAirWayBill varchar(100) unique,
	DateCreated DateTime default getdate(),
	DateArrived DateTime,
	EmployeeId int references Employee(Id),
	DeclarationNo varchar(15)
)
go
Create table BoxInfo
(
	Id int identity primary key,
	BoxId varchar(100) unique,
	DateCreated DateTime default getdate(),
	ShipmentQuantity int,
	MasterBillId int references MasterBill(Id),
	EmployeeId int references Employee(Id)
)
go
Create table ShipmentInfor
(
	Id int identity primary key,
	ShipmentId varchar(100) unique,
	DateCreated DateTime default getdate(),
	Sender nvarchar(300),--o	Người gửi(CompanyName); 
	Receiver nvarchar(300),--o	Người nhận(ContactName); 
	TelReceiver varchar(50),
	TotalValue float,
	Descrition nvarchar(1000),
	BoxId int references BoxInfo(Id),
	[Weight] float,--o	Khối lượng(Weight) 
	[Status] nvarchar(100),
	EmployeeId int references Employee(Id),
	WarehouseId int references Warehouse(Id),
	DeclarationNo varchar(15),
	Country nvarchar(100),--o	Nước gửi(Country)
	[Address] nvarchar(500),--o	Địa chỉ nhận(Address)
	Consignee nvarchar(200),--o	Consignee (Destination).
	Content nvarchar(300),--o	Nội dung hàng(Content); 
	NumberPackage int default 1,--o	Số kiện(mặc định là 1);
	DateOfCompletion DateTime,
	IsSyncOms bit
)
go
Create table ShipmentOut
(
	ShipmentId varchar(100) references ShipmentInfor(ShipmentId) primary key,
	BoxIdRef int references BoxInfo(Id),
	BoxIdString varchar(100),
	MasterBillId int references [MasterBill](Id),
	MasterBillIdString varchar(100),
	DateOut DateTime default getdate(),
	DateCreated DateTime default getdate(),
	EmployeeId int references Employee(Id),
	WarehouseId int references Warehouse(Id),
	IsSyncOms bit
)
go
Create table ShipmentInforTemp
(
	Id int identity primary key,
	ShipmentId varchar(100) unique,
	DateCreated DateTime default getdate(),
	Sender nvarchar(300),
	Receiver nvarchar(300),
	TelReceiver varchar(50),
	TotalValue float,
	Descrition nvarchar(1000),
	BoxId int references BoxInfo(Id),
	[Weight] float,
	[Status] nvarchar(100),
	EmployeeId int references Employee(Id),
	WarehouseId int references Warehouse(Id)
)
go
Create table ShipmentOutTemp
(
	ShipmentId varchar(100) unique,
	BoxIdRef int references BoxInfo(Id),
	BoxIdString varchar(100),
	MasterBillId int references [MasterBill](Id),
	MasterBillIdString varchar(100),
	DateOut DateTime default getdate(),
	EmployeeId int references Employee(Id),
	WarehouseId int references Warehouse(Id)
)
go
-- Chờ thông quan
Create table ShipmentWaitToConfirm
(
	ShipmentId varchar(100) primary key,
	CreatedDate DateTime default getdate(),
	EmployeeId int references Employee(Id)
)

go
CREATE TABLE ErrorLog(
	[Id] [int] IDENTITY(1,1) primary key,
	[CreationDate] [datetime] NULL,
	[AppCode] [nvarchar](4000) NULL,
	[ErrorCode] [nvarchar](4000) NULL,
	[ErrorMessage] [nvarchar](max) NULL
)
GO
CREATE TABLE [dbo].[Manifest](
	[Id] [int] IDENTITY(1,1) primary key,
	[SysDate] [datetime] NULL,
	[Active] [bit] NULL,
	[MasterAirWayBill] [varchar](100) NULL,
	[ShipmentNo] [varchar](100) NULL,
	[FlightNumber] [varchar](50) NULL,
	[FlightDate] [varchar](50) NULL,
	[BoxID] [varchar](100) NULL,
	[HSCode] [varchar](100) NULL,
	[ContactName] [nvarchar](100) NULL,
	[Tel] [varchar](200) NULL,
	[Address] [nvarchar](1000) NULL,
	[Currency] [nvarchar](20) NULL,
	[Content] [nvarchar](1000) NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [float] NULL,
	[TotalValue] [float] NULL,
	[Weight] [float] NULL,
	[Original] [nvarchar](100) NULL,
	[Destination] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[CompanyName] [nvarchar](200) NULL,
	[CreationDate] [datetime] NULL,
	DeclarationNo varchar(15)
)
--Alter table ShipmentOut add BoxId int references BoxInfo(Id)
--go
--Alter table ShipmentOut add MasterBillId int references [MasterBill](Id)
--go
--Alter table ShipmentOut add BoxIdString  varchar(100) 
--go
--Alter table ShipmentOut add MasterBillIdString varchar(100)
go
CREATE proc [dbo].[InsertManifest]
(
@MasterAirWayBill varchar(100),
@ShipmentNo varchar(100),
@FlightNumber varchar(50),
@FlightDate varchar(50),
@BoxID varchar(100),
@HSCode varchar(100),
@ContactName nvarchar(100),
@Tel varchar(200),
@Address nvarchar(1000),
@Currency nvarchar(20),
@Content nvarchar(1000),
@Quantity	int,
@UnitPrice	float,
@TotalValue	float,
@Weight float,
@Original	nvarchar(100),
@Destination nvarchar(100),
@Country nvarchar(100),
@CompanyName nvarchar(200),
@CreationDate datetime,
@ParamReturn int out) 
AS

BEGIN
Insert into Manifest(
MasterAirWayBill,
ShipmentNo,
FlightNumber,
FlightDate,
BoxID,
HSCode,
ContactName,
Tel,
Address,
Currency,
Content,
Quantity,
UnitPrice,
TotalValue,
Weight,
Original,
Destination,
Country,
CompanyName,
CreationDate
) 
VALUES(
@MasterAirWayBill,
@ShipmentNo,
@FlightNumber,
@FlightDate,
@BoxID,
@HSCode,
@ContactName,
@Tel,
@Address,
@Currency,
@Content,
@Quantity,
@UnitPrice,
@TotalValue,
@Weight,
@Original,
@Destination,
@Country,
@CompanyName,
@CreationDate)
SELECT @ParamReturn = @@IDENTITY;
Select @ParamReturn;
END;

GO
Insert into Warehouse values('HNW',N'Kho hàng Hà Nội',N'Lô 6 kho hàng sân bay Nội Bài','', getdate())
go
Insert into Warehouse values('SGW',N'Kho hàng Hồ Chí Minh',N'Lô 6 kho hàng sân bay Tân Sơn Nhất','', getdate())
go
GO
INSERT [dbo].[Employee] ([FullName], [UserName], [Pasword], [Role], [DateCreated], [BirthDate], [Phone], [Email], [Address], [Status]) VALUES (N'admin', N'admin', N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAka8GbBLIgUu0GBdToiS0egQAAAACAAAAAAAQZgAAAAEAACAAAAACe9ndbi3DhCVm41mfyD/xPZ/VIWh2IzXe+Sdf8bCHNAAAAAAOgAAAAAIAACAAAABxUH8KkL7QQn40zlFUpVyzRE2cUU/JwVLLR6SLdV7pvzAAAAA8j1wsZmqPuy5M/jgLID16ly0vGoMRy62GwyEBDRMi08EXWvvKOFC/zIxT0MC7YphAAAAA0Nd5aTtBorr9AX3YNizfOjbJe9JrPsbMq0Wm3SIleGtv+DkWsCo+Xf7JBdoZmeGQTFncfV4jsfpe6W0WzgLxuQ==', N'Administrator', CAST(N'2017-06-21 03:58:22.063' AS DateTime), CAST(N'2017-06-21 03:57:50.273' AS DateTime), N'1234567890', N'dsfdgjdh', N'fdsgfh', NULL)
