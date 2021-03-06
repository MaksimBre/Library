/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [LibraryDB]    Script Date: 11/8/2017 7:08:38 PM ******/
CREATE DATABASE [LibraryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\LibraryDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\LibraryDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LibraryDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibraryDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryDB] SET QUERY_STORE = OFF
GO
USE [LibraryDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [LibraryDB]
GO
/****** Object:  Table [dbo].[BookGenres]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookGenres](
	[BookId] [int] NOT NULL,
	[GenreId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookRentals]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookRentals](
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[RentalDate] [date] NOT NULL,
	[ReturnDate] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[NoOfPages] [int] NOT NULL,
	[StockCount] [int] NOT NULL,
	[WriterId] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Username] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[DateOfBirth] [date] NULL,
	[DateJoined] [date] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Writers]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Writers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Biography] [nvarchar](300) NOT NULL,
	[NoOfBooks] [int] NULL,
 CONSTRAINT [PK_Writers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (10, 2)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (11, 2)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (13, 2)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (15, 2)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (16, 2)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (10, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (11, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (15, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (16, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (10, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (11, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (13, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (15, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (16, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (13, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (21, 6)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (21, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (21, 5)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (24, 7)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (24, 9)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (24, 4)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (2010, 3)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (2010, 6)
INSERT [dbo].[BookGenres] ([BookId], [GenreId]) VALUES (2010, 9)
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 10, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 11, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 13, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 10, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 11, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 11, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 13, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 10, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 11, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 16, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 16, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 13, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 15, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 11, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 13, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1069, 15, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1070, 13, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
INSERT [dbo].[BookRentals] ([UserId], [BookId], [RentalDate], [ReturnDate]) VALUES (1070, 27, CAST(N'2017-11-08' AS Date), CAST(N'2017-12-08' AS Date))
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (10, N'The Silmarillion', 300, 20, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (11, N'The Lord of the Rings: The Fellowship of the Ring', 350, 20, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (13, N'The Lord of the Rings: The Two Towers', 400, 20, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (15, N'The Lord of the Rings: The Return of the King', 320, 20, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (16, N'The Hobbit', 300, 20, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (17, N'The Book of Lost Tales I', 340, 5, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (18, N'The Book of Lost Tales II', 310, 5, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (19, N'Milenium', 200, 10, 24)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (20, N'Plemić', 250, 10, 24)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (21, N'Operater', 300, 15, 24)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (22, N'The Prince of Mist', 450, 10, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (23, N'The Midnight Palace', 400, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (24, N'Marina', 350, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (25, N'The Shadow of the Wind', 420, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (26, N'The Angel''s Game', 400, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (27, N'The Prisoner of Heaven', 360, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (28, N'The Labyrinth of Spirits', 430, 5, 25)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (29, N'Dr Jekyll and Mr Hyde', 200, 15, 26)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (30, N'A Child''s Garden of Verses', 250, 5, 26)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (1014, N'dsad', 0, 0, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (1015, N'dsad', 0, 0, 20)
INSERT [dbo].[Books] ([Id], [Title], [NoOfPages], [StockCount], [WriterId]) VALUES (2010, N'Out of Time', 5, 5, 20)
SET IDENTITY_INSERT [dbo].[Books] OFF
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([Id], [Name]) VALUES (2, N'Fantasy')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (3, N'Novel')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (4, N'Crime')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (5, N'Drama')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (6, N'Science fiction')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (7, N'Horror')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (8, N'Action')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (9, N'Adventure')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (10, N'Biography')
INSERT [dbo].[Genres] ([Id], [Name]) VALUES (11, N'Diaries')
SET IDENTITY_INSERT [dbo].[Genres] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (24, N'User')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (26, N'Librarian')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (27, N'Administrator')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1070, 24)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1069, 24)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1069, 26)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1069, 27)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2035, 24)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2035, 26)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [DateOfBirth], [DateJoined]) VALUES (1069, N'Administrator', N'Administrator', N'25e4ee4e9229397b6b17776bfceaf8e7', N'admin@gmail.com', CAST(N'1993-10-30' AS Date), CAST(N'2017-11-01' AS Date))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [DateOfBirth], [DateJoined]) VALUES (1070, N'Dragan', N'Ilic Dragan', N'5f4dcc3b5aa765d61d8327deb882cf99', N'gagi@gmail.com', NULL, CAST(N'2017-11-02' AS Date))
INSERT [dbo].[Users] ([Id], [Name], [Username], [Password], [Email], [DateOfBirth], [DateJoined]) VALUES (2035, N'Librarian', N'Libman', N'45d0957b5daef354afdd3da14321a63d', N'lib@gmail.com', NULL, CAST(N'2017-11-06' AS Date))
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Writers] ON 

INSERT [dbo].[Writers] ([Id], [Name], [Biography], [NoOfBooks]) VALUES (20, N'J.R.R. Tolken', N'John Ronald Reuel Tolkien, (3 January 1892 – 2 September 1973) was an English writer, poet, philologist, and university professor who is best known as the author of the classic high-fantasy works The Hobbit, The Lord of the Rings, and The Silmarillion.', 31)
INSERT [dbo].[Writers] ([Id], [Name], [Biography], [NoOfBooks]) VALUES (24, N'B.D. Benedikt', N'Božidar Damjanović Benedikt je kanadski pisac i filmski reditelj, rođen 7. aprila 1938. godine u malom hrvatskom mestu Vinici kod Varaždina.

Autor je više desetina klasičnih romana i naučno-popularnih knjiga objavljenih na srpskohrvatskom, poljskom, makedonskom, slovenačkom i engleskom jeziku.', 15)
INSERT [dbo].[Writers] ([Id], [Name], [Biography], [NoOfBooks]) VALUES (25, N'C.R. Safon', N'Carlos Ruiz Zafón (born 25 September 1964) is a Spanish novelist.', 20)
INSERT [dbo].[Writers] ([Id], [Name], [Biography], [NoOfBooks]) VALUES (26, N'R.L. Stevenson', N'Robert Louis Balfour Stevenson (13 November 1850 – 3 December 1894) was a Scottish novelist, poet, essayist, and travel writer. His most famous works are Treasure Island, Kidnapped, Strange Case of Dr Jekyll and Mr Hyde, and A Child''s Garden of Verses.', 30)
SET IDENTITY_INSERT [dbo].[Writers] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20171030-175226]    Script Date: 11/8/2017 7:08:39 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20171030-175226] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20171030-185826]    Script Date: 11/8/2017 7:08:39 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20171030-185826] ON [dbo].[Users]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookGenres]  WITH CHECK ADD  CONSTRAINT [FK_BooksGenres_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenres] CHECK CONSTRAINT [FK_BooksGenres_Books]
GO
ALTER TABLE [dbo].[BookGenres]  WITH CHECK ADD  CONSTRAINT [FK_BooksGenres_Genres] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookGenres] CHECK CONSTRAINT [FK_BooksGenres_Genres]
GO
ALTER TABLE [dbo].[BookRentals]  WITH CHECK ADD  CONSTRAINT [FK_BookRentals_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookRentals] CHECK CONSTRAINT [FK_BookRentals_Books]
GO
ALTER TABLE [dbo].[BookRentals]  WITH CHECK ADD  CONSTRAINT [FK_BookRentals_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookRentals] CHECK CONSTRAINT [FK_BookRentals_Users]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Writers] FOREIGN KEY([WriterId])
REFERENCES [dbo].[Writers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Writers]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[BookDelete]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookDelete]
@Id int
AS
DELETE FROM Books WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[BookDeleteBookGenre]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookDeleteBookGenre]
@BookId int,
@GenreId int
AS
DELETE FROM BookGenres WHERE BookId = @BookId AND GenreId = @GenreId
GO
/****** Object:  StoredProcedure [dbo].[BookGetAll]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookGetAll]
AS
SELECT * FROM Books
GO
/****** Object:  StoredProcedure [dbo].[BookGetAllByWriter]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookGetAllByWriter]
@Id int
AS
SELECT * FROM Books WHERE WriterId = @Id
GO
/****** Object:  StoredProcedure [dbo].[BookGetAllRentalsByUser]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookGetAllRentalsByUser]
@id int
AS
SELECT * FROM BookRentals
WHERE UserId = @id
GO
/****** Object:  StoredProcedure [dbo].[BookGetById]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookGetById]
@Id int
AS
SELECT * FROM Books WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[BookInsert]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookInsert]
@Title nvarchar(50),
@NoOfPages int,
@StockCount int,
@WriterId int
AS
INSERT INTO Books (Title, NoOfPages, StockCount, WriterId) VALUES (@Title, @NoOfPages, @StockCount, @WriterId)
SELECT CAST(SCOPE_IDENTITY() AS int)
GO
/****** Object:  StoredProcedure [dbo].[BookInsertBookGenre]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookInsertBookGenre]
@BookId int,
@GenreId int
AS
INSERT INTO BookGenres (BookId, GenreId) VALUES(@BookId, @GenreId)
GO
/****** Object:  StoredProcedure [dbo].[BookSearch]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookSearch]
@TitleSearch nvarchar(50)
AS
SELECT * FROM Books WHERE Title LIKE @TitleSearch
GO
/****** Object:  StoredProcedure [dbo].[BookUpdate]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookUpdate]
@Id int,
@Title nvarchar(50),
@NoOfPages int,
@StockCount int,
@WriterId int
AS
UPDATE Books SET Title = @Title, NoOfPages = @NoOfPages, StockCount = @StockCount, WriterId = @WriterId WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[GenreDelete]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreDelete]
@Id int
AS
DELETE FROM Genres WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[GenreGetAll]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreGetAll]
AS
SELECT * FROM Genres
GO
/****** Object:  StoredProcedure [dbo].[GenreGetAllByBookId]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreGetAllByBookId]
@Id int
AS
SELECT * FROM Genres INNER JOIN BookGenres ON Genres.Id = BookGenres.GenreId WHERE BookId = @Id
GO
/****** Object:  StoredProcedure [dbo].[GenreGetByBookId]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreGetByBookId]
@Id int
AS
SELECT BookId,Name FROM (Books
INNER JOIN BookGenres ON Books.Id = BookGenres.BookId)
INNER JOIN Genres ON GenreId = Genres.Id
WHERE BookId = @Id
GO
/****** Object:  StoredProcedure [dbo].[GenreGetById]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreGetById]
@Id int
AS
SELECT * FROM Genres WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[GenreInsert]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreInsert]
@Name nvarchar(50)
AS
INSERT INTO Genres (Name) VALUES(@Name)
SELECT CAST(SCOPE_IDENTITY() AS int)
GO
/****** Object:  StoredProcedure [dbo].[GenreSearch]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreSearch]
@Name nvarchar(50)
AS
SELECT * FROM Genres WHERE Name LIKE @Name
GO
/****** Object:  StoredProcedure [dbo].[GenreUpdate]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenreUpdate]
@Id int,
@Name nvarchar(50)
AS
UPDATE Genres SET Name = @Name WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[RoleDelete]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleDelete]
@Id int
AS
DELETE FROM Roles WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[RoleGetAll]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleGetAll]
AS
SELECT * FROM Roles
GO
/****** Object:  StoredProcedure [dbo].[RoleGetAllByUser]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleGetAllByUser]
@Id int
AS
SELECT U.Id, R.Name FROM (Users U
INNER JOIN UserRoles ON U.Id = UserRoles.UserId) 
INNER JOIN Roles R ON R.Id = RoleId
WHERE U.Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[RoleGetById]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleGetById]
@Id int
AS
SELECT * FROM Roles WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[RoleGetByName]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleGetByName]
@Name nvarchar(50)
AS
SELECT * FROM Roles WHERE Name = @Name
GO
/****** Object:  StoredProcedure [dbo].[RoleGetByUser]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleGetByUser]
@Id int
AS
SELECT U.Id, R.Name FROM (Users U
INNER JOIN UserRoles ON U.Id = UserRoles.UserId) 
INNER JOIN Roles R ON R.Id = RoleId
WHERE U.Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[RoleInsert]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleInsert]
@Name nvarchar(50)
AS
INSERT INTO Roles (Name) VALUES(@Name)
SELECT CAST(SCOPE_IDENTITY() AS int)
GO
/****** Object:  StoredProcedure [dbo].[RoleUpdate]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleUpdate]
@Id int,
@Name nvarchar(50)
AS
UPDATE Roles SET Name = @Name WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[UserDelete]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDelete]
@Id int
AS
DELETE FROM Users WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[UserDeleteAllUserRoles]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDeleteAllUserRoles]
@Id int
AS
DELETE FROM UserRoles WHERE UserId = @Id
GO
/****** Object:  StoredProcedure [dbo].[UserDeleteBookRentals]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDeleteBookRentals]
@UserId int,
@BookId int
AS
DELETE FROM BookRentals WHERE UserId = @UserId AND BookId = @BookId
GO
/****** Object:  StoredProcedure [dbo].[UserDeleteUserRole]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserDeleteUserRole]
@UserId int,
@RoleId int
AS
DELETE FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId
GO
/****** Object:  StoredProcedure [dbo].[UserGetAll]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserGetAll]
AS
SELECT * FROM Users
GO
/****** Object:  StoredProcedure [dbo].[UserGetByEmail]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserGetByEmail]
@email nvarchar(30)
AS
SELECT * FROM Users WHERE Email = @email
GO
/****** Object:  StoredProcedure [dbo].[UserGetById]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserGetById]
@Id int
AS
SELECT * FROM Users WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[UserGetByLogin]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserGetByLogin]
@username nvarchar(60),
@email nvarchar(30),
@password nvarchar(50)
AS
SELECT * FROM Users WHERE (Username = @username OR Email = @email) AND Password = @password
GO
/****** Object:  StoredProcedure [dbo].[UserInsert]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserInsert]
@Name nvarchar(60),
@UserName nvarchar(60),
@Password nvarchar(50),
@Email nvarchar(30),
@DateOfBirth Date,
@DateJoined Date
AS
BEGIN TRY
INSERT INTO Users (Name, UserName, Password, Email, DateOfBirth, DateJoined)
VALUES (@Name, @UserName, @Password, @Email ,@DateOfBirth ,@DateJoined)
SELECT CAST(SCOPE_IDENTITY() AS int)
END TRY
BEGIN CATCH
DECLARE @error NVARCHAR(255)
SET @error = (SUBSTRING( ERROR_MESSAGE(),    CHARINDEX('(',ERROR_MESSAGE()),     CHARINDEX(')',ERROR_MESSAGE())));
if(CHARINDEX('@',@error) > 0) SELECT -2;
ELSE SELECT -1;
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[UserInsertBookRentals]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserInsertBookRentals]
@UserId int,
@BookId int,
@RentalDate Date,
@ReturnDate Date
AS
INSERT INTO BookRentals (UserId, BookId, RentalDate, ReturnDate) VALUES(@UserId, @BookId, @RentalDate, @ReturnDate)
GO
/****** Object:  StoredProcedure [dbo].[UserInsertUserRole]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserInsertUserRole]
@UserId int,
@RoleId int
AS
INSERT INTO UserRoles (UserId, RoleId) VALUES(@UserId, @RoleId)
GO
/****** Object:  StoredProcedure [dbo].[UserSearch]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSearch]
@Name nvarchar(60)
AS
SELECT * FROM Users WHERE Name LIKE @Name
GO
/****** Object:  StoredProcedure [dbo].[UserUpdate]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserUpdate]
@Id int,
@Name nvarchar(60),
@Username nvarchar(60),
@Password nvarchar(50),
@Email nvarchar(30),
@DateOfBirth date,
@DateJoined date
AS
UPDATE Users SET Name = @Name, Username=@Username, Password=@Password, Email=@Email, DateOfBirth=@DateOfBirth, DateJoined = @DateJoined WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[WriterDelete]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterDelete]
@Id int
AS
DELETE FROM Writers WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[WriterGetAll]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterGetAll]
AS
SELECT * FROM Writers
GO
/****** Object:  StoredProcedure [dbo].[WriterGetById]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterGetById]
@Id int
AS
SELECT * FROM Writers WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[WriterInsert]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterInsert]
@Name nvarchar(60),
@Biography nvarchar(300),
@NoOfBooks int
AS
INSERT INTO Writers (Name, Biography, NoOfBooks) VALUES(@Name, @Biography, @NoOfBooks)
SELECT CAST(SCOPE_IDENTITY() AS int)
GO
/****** Object:  StoredProcedure [dbo].[WriterSearch]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterSearch]
@Name nvarchar(60)
AS
SELECT * FROM Writers WHERE Name LIKE @Name
GO
/****** Object:  StoredProcedure [dbo].[WriterUpdate]    Script Date: 11/8/2017 7:08:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriterUpdate]
@Id int,
@Name nvarchar(60),
@Bio nvarchar(300),
@Nob int
AS
UPDATE Writers SET Name = @Name, Biography = @Bio, NoOfBooks = @Nob WHERE Id = @Id 
GO
USE [master]
GO
ALTER DATABASE [LibraryDB] SET  READ_WRITE 
GO
