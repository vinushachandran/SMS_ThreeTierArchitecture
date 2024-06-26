USE [SMS_DB]
GO
ALTER TABLE [dbo].[Teacher_Subject_Allocation] DROP CONSTRAINT [FK__Teacher_S__Teach__797309D9]
GO
ALTER TABLE [dbo].[Teacher_Subject_Allocation] DROP CONSTRAINT [FK__Teacher_S__Subje__7A672E12]
GO
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation] DROP CONSTRAINT [FK__Student_S__Subje__7E37BEF6]
GO
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation] DROP CONSTRAINT [FK__Student_S__Stude__7F2BE32F]
GO
ALTER TABLE [dbo].[Teacher] DROP CONSTRAINT [DF__Teacher__IsEnabl__6FE99F9F]
GO
ALTER TABLE [dbo].[Subject] DROP CONSTRAINT [DF__Subject__IsEnabl__75A278F5]
GO
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [DF__Student__IsEnabl__72C60C4A]
GO
/****** Object:  Index [UQ__Teacher___7733E37D94F17654]    Script Date: 5/22/2024 3:13:19 PM ******/
ALTER TABLE [dbo].[Teacher_Subject_Allocation] DROP CONSTRAINT [UQ__Teacher___7733E37D94F17654]
GO
/****** Object:  Index [UQ__Student___58646DF939E2A1D8]    Script Date: 5/22/2024 3:13:19 PM ******/
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation] DROP CONSTRAINT [UQ__Student___58646DF939E2A1D8]
GO
/****** Object:  Table [dbo].[Teacher_Subject_Allocation]    Script Date: 5/22/2024 3:13:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teacher_Subject_Allocation]') AND type in (N'U'))
DROP TABLE [dbo].[Teacher_Subject_Allocation]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 5/22/2024 3:13:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teacher]') AND type in (N'U'))
DROP TABLE [dbo].[Teacher]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 5/22/2024 3:13:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subject]') AND type in (N'U'))
DROP TABLE [dbo].[Subject]
GO
/****** Object:  Table [dbo].[Student_Subject_Teacher_Allocation]    Script Date: 5/22/2024 3:13:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Subject_Teacher_Allocation]') AND type in (N'U'))
DROP TABLE [dbo].[Student_Subject_Teacher_Allocation]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/22/2024 3:13:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
USE [master]
GO
/****** Object:  Database [SMS_DB]    Script Date: 5/22/2024 3:13:19 PM ******/
DROP DATABASE [SMS_DB]
GO
/****** Object:  Database [SMS_DB]    Script Date: 5/22/2024 3:13:19 PM ******/
CREATE DATABASE [SMS_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SMS_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.VINUSHA\MSSQL\DATA\SMS_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SMS_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.VINUSHA\MSSQL\DATA\SMS_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SMS_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMS_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMS_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SMS_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SMS_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SMS_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SMS_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SMS_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SMS_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SMS_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SMS_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SMS_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SMS_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SMS_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SMS_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SMS_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SMS_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SMS_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SMS_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SMS_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SMS_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SMS_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SMS_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SMS_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SMS_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SMS_DB] SET  MULTI_USER 
GO
ALTER DATABASE [SMS_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SMS_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SMS_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SMS_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SMS_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SMS_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SMS_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [SMS_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SMS_DB]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/22/2024 3:13:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentRegNo] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[MiddleName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DisplayName] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[DOB] [date] NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[ContactNo] [nvarchar](15) NOT NULL,
	[IsEnable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student_Subject_Teacher_Allocation]    Script Date: 5/22/2024 3:13:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Subject_Teacher_Allocation](
	[StudentAllocationID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NOT NULL,
	[SubjectAllocationID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 5/22/2024 3:13:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubjectCode] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsEnable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 5/22/2024 3:13:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [bigint] IDENTITY(1,1) NOT NULL,
	[TeacherRegNo] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[MiddleName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DisplayName] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[DOB] [date] NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[ContactNo] [nvarchar](15) NOT NULL,
	[IsEnable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher_Subject_Allocation]    Script Date: 5/22/2024 3:13:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher_Subject_Allocation](
	[SubjectAllocationID] [bigint] IDENTITY(1,1) NOT NULL,
	[TeacherID] [bigint] NOT NULL,
	[SubjectID] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10011, N'STD1001', N'John', N'Michael', N'Smith', N'John Smith	', N'john.smith@example.com', N'Male', CAST(N'2000-01-12' AS Date), N'123 Elm St, Springfield, IL	', N'555-123-4567', 1)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10012, N'STD1002', N'Jane', N'Elizabeth', N'Doe', N'Jane Doe	', N'jane.doe@example.com', N'Female', CAST(N'2001-01-18' AS Date), N'456 Oak St, Springfield, IL	', N'555-234-5678', 0)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10013, N'STD1003', N'Robert', N'James', N'Johnson', N'Robert Johnson	', N'robert.johnson@gmail.com', N'Female', CAST(N'2002-03-20' AS Date), N'789 Pine St, Springfield, IL	', N'555-345-6789', 0)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10014, N'STD1004', N'Emily', N'Grace', N'Williams', N'Emily Williams	', N'emily.williams@gmail.com', N'Female', CAST(N'1998-01-14' AS Date), N'101 Maple St, Springfield, IL	', N'555-456-7890', 1)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10015, N'STD1005', N'Michael', N'Andrew', N'Brown', N'Michael Brown	', N'michael.brown@gmail.com', N'Male', CAST(N'1999-06-18' AS Date), N'202 Birch St, Springfield, IL	', N'555-567-8901', 1)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10016, N'STD1006', N'Sarah', N'Anne', N'Davis', N'Sarah Davis	', N'sarah.davis@gmail.com', N'Female', CAST(N'2003-10-17' AS Date), N'303 Cedar St, Springfield, IL	', N'555-678-9012', 1)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10017, N'STD1007', N'David', N'Joseph', N'Miller', N'David Miller	', N'david.miller@example.com', N'Male', CAST(N'2004-04-21' AS Date), N'404 Walnut St, Springfield, IL	', N'555-789-0123', 1)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10018, N'STD1008', N'Laura', N'Marie', N'Wilson', N'Laura Wilson	', N'laura.wilson@example.com', N'Female', CAST(N'2000-09-20' AS Date), N'505 Chestnut St, Springfield, IL	', N'555-890-1234', 0)
INSERT [dbo].[Student] ([StudentID], [StudentRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10019, N'STD1009', N'James', N'Alexander', N'Moore', N'James Moore	', N'james.moore@example.com', N'Male', CAST(N'1999-08-12' AS Date), N'606 Spruce St, Springfield, IL	', N'555-901-2345', 0)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Student_Subject_Teacher_Allocation] ON 

INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10041, 10011, 10042)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (20039, 10011, 10045)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10046, 10011, 10046)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10042, 10011, 10047)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10044, 10011, 10048)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10043, 10011, 10050)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10049, 10011, 10051)
INSERT [dbo].[Student_Subject_Teacher_Allocation] ([StudentAllocationID], [StudentID], [SubjectAllocationID]) VALUES (10048, 10015, 10042)
SET IDENTITY_INSERT [dbo].[Student_Subject_Teacher_Allocation] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (36, N'MATH101', N'Calculus ', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (37, N'ENG102', N'English Literature', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (38, N'CS103', N'Introduction to Programming', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (39, N'BIO104', N'Biology', 0)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (40, N'CHEM105', N'Organic Chemistry', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (41, N'PHYS106', N'Physics ', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (42, N'HIST107', N'History', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (43, N'ECON109', N'Microeconomics', 1)
INSERT [dbo].[Subject] ([SubjectID], [SubjectCode], [Name], [IsEnable]) VALUES (44, N'SOC110', N'Sociology', 0)
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10037, N'TR2001', N'Alice', N'Margaret', N'Johnson', N'Alice Johnson	', N'alice.johnson@gmail.com', N'Female', CAST(N'1990-01-09' AS Date), N'100 Park Ave, Springfield, IL	', N'555-234-5678', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10038, N'TR2002', N'Brian', N'Charles', N'Smith', N'Brian Smith	', N'brian.smith@gmail.com', N'Male', CAST(N'1989-06-16' AS Date), N'101 Park Ave, Springfield, IL	', N'555-345-6789', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10039, N'TR2003', N'Carol', N'Ann', N'Brown', N'Carol Brown	', N'carol.brown@gmail.com', N'Female', CAST(N'1994-07-13' AS Date), N'102 Park Ave, Springfield, IL	', N'555-456-7890', 0)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10040, N'TR2004', N'David', N'Edward', N'Wilson', N'David Wilson	', N'david.wilson@gmail.com', N'Male', CAST(N'1986-02-12' AS Date), N'103 Park Ave, Springfield, IL	', N'555-567-8901', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10041, N'TR2005', N'Emma', N'Louise', N'Taylor', N'Emma Taylor	', N'emma.taylor@gmail.com', N'Female', CAST(N'1988-03-17' AS Date), N'104 Park Ave, Springfield, IL	', N'555-678-9012', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10042, N'TR2006', N'Frank', N'Henry', N'Anderson', N'Frank Anderson	', N'frank.anderson@gmail.com', N'Male', CAST(N'1992-05-18' AS Date), N'105 Park Ave, Springfield, IL	', N'555-789-0123', 0)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10043, N'TR2007', N'Grace', N'Marie', N'Thomas', N'Grace Thomas	', N'grace.thomas@gmail.com', N'Female', CAST(N'1991-08-13' AS Date), N'106 Park Ave, Springfield, IL	', N'555-890-1234', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherRegNo], [FirstName], [MiddleName], [LastName], [DisplayName], [Email], [Gender], [DOB], [Address], [ContactNo], [IsEnable]) VALUES (10044, N'TR2008', N'Henry', N'Samuel', N'Jackson', N'Henry Jackson	', N'henry.jackson@gmail.com', N'Male', CAST(N'1995-03-03' AS Date), N'107 Park Ave, Springfield, IL	', N'555-901-2345', 1)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher_Subject_Allocation] ON 

INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10042, 10037, 37)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10043, 10037, 42)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10050, 10037, 43)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10044, 10038, 36)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10045, 10038, 41)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10051, 10040, 37)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10046, 10040, 40)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10048, 10041, 36)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10047, 10041, 38)
INSERT [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID], [TeacherID], [SubjectID]) VALUES (10049, 10043, 43)
SET IDENTITY_INSERT [dbo].[Teacher_Subject_Allocation] OFF
GO
/****** Object:  Index [UQ__Student___58646DF939E2A1D8]    Script Date: 5/22/2024 3:13:20 PM ******/
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation] ADD UNIQUE NONCLUSTERED 
(
	[StudentID] ASC,
	[SubjectAllocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Teacher___7733E37D94F17654]    Script Date: 5/22/2024 3:13:20 PM ******/
ALTER TABLE [dbo].[Teacher_Subject_Allocation] ADD UNIQUE NONCLUSTERED 
(
	[TeacherID] ASC,
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student] ADD  DEFAULT ((1)) FOR [IsEnable]
GO
ALTER TABLE [dbo].[Subject] ADD  DEFAULT ((1)) FOR [IsEnable]
GO
ALTER TABLE [dbo].[Teacher] ADD  DEFAULT ((1)) FOR [IsEnable]
GO
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Student_Subject_Teacher_Allocation]  WITH CHECK ADD FOREIGN KEY([SubjectAllocationID])
REFERENCES [dbo].[Teacher_Subject_Allocation] ([SubjectAllocationID])
GO
ALTER TABLE [dbo].[Teacher_Subject_Allocation]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Teacher_Subject_Allocation]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
USE [master]
GO
ALTER DATABASE [SMS_DB] SET  READ_WRITE 
GO
