USE [master]
GO
/****** Object:  Database [TicketSystem]    Script Date: 2021/10/25 下午 02:45:35 ******/
CREATE DATABASE [TicketSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TicketSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TicketSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TicketSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TicketSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TicketSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TicketSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TicketSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TicketSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TicketSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TicketSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TicketSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [TicketSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TicketSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TicketSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TicketSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TicketSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TicketSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TicketSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TicketSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TicketSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TicketSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TicketSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TicketSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TicketSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TicketSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TicketSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TicketSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TicketSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TicketSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TicketSystem] SET  MULTI_USER 
GO
ALTER DATABASE [TicketSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TicketSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TicketSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TicketSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TicketSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TicketSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TicketSystem] SET QUERY_STORE = OFF
GO


USE [TicketSystem]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2021/10/25 下午 02:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketData]    Script Date: 2021/10/25 下午 02:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Summary] [nvarchar](300) NULL,
	[Description] [nvarchar](3000) NULL,
	[Type] [varchar](30) NULL,
	[Status] [varchar](30) NULL,
 CONSTRAINT [PK__TicketDa__3214EC07B0EDDD09] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2021/10/25 下午 02:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Role] [varchar](500) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
 CONSTRAINT [PK__User__3214EC0743F11804] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (2, N'QA')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (3, N'RD')
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (4, N'PM')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketData] ON 
GO
INSERT [dbo].[TicketData] ([Id], [Summary], [Description], [Type], [Status]) VALUES (1, N'Summy is Hi~', N'is desc~~~~~', N'bug', N'Open')
GO
INSERT [dbo].[TicketData] ([Id], [Summary], [Description], [Type], [Status]) VALUES (2, N'概要', N'描述', N'bug', N'Open')
GO
INSERT [dbo].[TicketData] ([Id], [Summary], [Description], [Type], [Status]) VALUES (3, N'置換圖片', N'更新首頁 logo 圖片', N'bug', N'Resolve')
GO
SET IDENTITY_INSERT [dbo].[TicketData] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Role], [FirstName], [LastName]) VALUES (1, N'admin', N'admin', N'Admin', N'Admin', N'user')
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Role], [FirstName], [LastName]) VALUES (2, N'qa', N'qa', N'QA', N'QA', N'user')
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Role], [FirstName], [LastName]) VALUES (3, N'rd', N'rd', N'RD', N'RD', N'user')
GO
INSERT [dbo].[User] ([Id], [Username], [Password], [Role], [FirstName], [LastName]) VALUES (4, N'pm', N'pm', N'PM', N'PM', N'user')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
USE [master]
GO
ALTER DATABASE [TicketSystem] SET  READ_WRITE 
GO
