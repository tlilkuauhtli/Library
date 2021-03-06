USE [master]
GO
/****** Object:  Database [Library]    Script Date: 10/08/2015 08:31:04 a.m. ******/
CREATE DATABASE [Library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Library.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Library_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Library_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Library] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Library] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Library] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Library] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Library] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Library] SET ARITHABORT OFF 
GO
ALTER DATABASE [Library] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Library] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Library] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Library] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Library] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Library] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Library] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Library] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Library] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Library] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Library] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Library] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Library] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Library] SET  MULTI_USER 
GO
ALTER DATABASE [Library] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Library] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Library] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Library]
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchBooks]    Script Date: 10/08/2015 08:31:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_SearchBooks]
(
	@BookName		varchar(100) = Null,	-- Book's Name
	@BookAuthor		varchar(50)	= Null,		-- Book's Author
	@BookCategory	varchar(20) = Null,		-- Book's Category
	@Order			int = 1					-- Order ID
)
As
Begin

	set @Order = (Case When @Order < 1 Then 1
					When @Order > 3 Then 1 
					Else @Order End)

	Select 
		B.Book_Id As BookId, 
		B.Name As BookName, 
		(A.Surname + ', ' +A.First_Name) AuthorName, 
		C.Name As CategoryName
	From dbo.Books B With(NoLock)
	Inner Join dbo.Authors A With(NoLock) On A.Author_Id = B.Author_Id
	Inner Join dbo.Categories C With(NoLock) On C.Category_Id = B.Category_Id
	Where
	(@BookName Is Null Or LTrim(RTrim(@BookName)) = '' Or B.Name Like '%' + @BookName + '%')
	And (@BookAuthor Is Null Or LTrim(RTrim(@BookAuthor)) = '' Or (A.Surname + ' ' + A.First_Name) Like '%' + @BookAuthor + '%')
	And (@BookCategory Is Null Or LTrim(RTrim(@BookCategory)) = '' Or C.Name Like '%' + @BookCategory + '%' )
	Order By
		Case When @Order = 1 Then B.Name End Asc,
		Case When @Order = 1 Then (A.Surname + ' ' +A.First_Name) End Asc,
		Case When @Order = 1 Then C.Name End Asc,

		Case When @Order = 2 Then (A.Surname + ' ' +A.First_Name) End Asc,
		Case When @Order = 2 Then B.Name End Asc,
		Case When @Order = 2 Then C.Name End Asc,

		Case When @Order = 3 Then C.Name End Asc,
		Case When @Order = 3 Then B.Name End Asc,
		Case When @Order = 3 Then (A.Surname + ' ' +A.First_Name) End Asc
End
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 10/08/2015 08:31:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Authors](
	[Author_Id] [int] NOT NULL,
	[First_Name] [varchar](20) NOT NULL,
	[Surname] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Author_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Books]    Script Date: 10/08/2015 08:31:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Books](
	[Book_Id] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Author_Id] [int] NOT NULL,
	[Category_Id] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Book_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/08/2015 08:31:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_Id] [int] NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors] FOREIGN KEY([Author_Id])
REFERENCES [dbo].[Authors] ([Author_Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Authors]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Categories] ([Category_Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories]
GO
USE [master]
GO
ALTER DATABASE [Library] SET  READ_WRITE 
GO
