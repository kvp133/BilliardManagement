USE [master]
GO
/****** Object:  Database [BilliardManagement]    Script Date: 6/23/2024 10:56:58 PM ******/
CREATE DATABASE [BilliardManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BilliardManagement', FILENAME = N'E:\MySQL\MSSQL16.SQLEXPRESS\MSSQL\DATA\BilliardManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BilliardManagement_log', FILENAME = N'E:\MySQL\MSSQL16.SQLEXPRESS\MSSQL\DATA\BilliardManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BilliardManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BilliardManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BilliardManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BilliardManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BilliardManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BilliardManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BilliardManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [BilliardManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BilliardManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BilliardManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BilliardManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BilliardManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BilliardManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BilliardManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BilliardManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BilliardManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BilliardManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BilliardManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BilliardManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BilliardManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BilliardManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BilliardManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BilliardManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BilliardManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BilliardManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BilliardManagement] SET  MULTI_USER 
GO
ALTER DATABASE [BilliardManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BilliardManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BilliardManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BilliardManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BilliardManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BilliardManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BilliardManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [BilliardManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BilliardManagement]
GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 6/23/2024 10:56:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[BookingDetailID] [int] IDENTITY(1,1) NOT NULL,
	[BookingID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 6/23/2024 10:56:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[TableID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[TotalAmount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 6/23/2024 10:56:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/23/2024 10:56:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tables]    Script Date: 6/23/2024 10:56:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tables](
	[TableID] [int] IDENTITY(1,1) NOT NULL,
	[TableNumber] [int] NOT NULL,
	[HourlyRate] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BookingDetails] ON 

INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [ProductID], [Quantity], [UnitPrice]) VALUES (1, 1, 1, 2, CAST(30000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[BookingDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([BookingID], [TableID], [EmployeeID], [StartTime], [EndTime], [TotalAmount]) VALUES (1, 1, 1, CAST(N'2024-06-23T20:27:05.283' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [PhoneNumber], [Email], [Role]) VALUES (1, N'manager', N'13121012', N'Nguy?n Van A', N'0123456789', N'manager@example.com', N'Manager')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [Price], [Description]) VALUES (1, N'RedBull', CAST(15000.00 AS Decimal(18, 2)), N'Testing Data')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Tables] ON 

INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (1, 1, CAST(30000.00 AS Decimal(18, 2)), N'Occupied')
INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (2, 2, CAST(40000.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (3, 3, CAST(30000.00 AS Decimal(18, 2)), N'Maintenance')
INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (4, 4, CAST(30000.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (5, 5, CAST(30000.00 AS Decimal(18, 2)), N'Available')
INSERT [dbo].[Tables] ([TableID], [TableNumber], [HourlyRate], [Status]) VALUES (6, 6, CAST(35000.00 AS Decimal(18, 2)), N'Available')
SET IDENTITY_INSERT [dbo].[Tables] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Employee__536C85E4957D287E]    Script Date: 6/23/2024 10:56:58 PM ******/
ALTER TABLE [dbo].[Employees] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Tables__E8E0DB52B6647BF5]    Script Date: 6/23/2024 10:56:58 PM ******/
ALTER TABLE [dbo].[Tables] ADD UNIQUE NONCLUSTERED 
(
	[TableNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([BookingID])
REFERENCES [dbo].[Bookings] ([BookingID])
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([TableID])
REFERENCES [dbo].[Tables] ([TableID])
GO
USE [master]
GO
ALTER DATABASE [BilliardManagement] SET  READ_WRITE 
GO
