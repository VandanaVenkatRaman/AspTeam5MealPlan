USE [master]
GO
/****** Object:  Database [MealPlanner]    Script Date: 4/21/2019 12:31:46 PM ******/
CREATE DATABASE [MealPlanner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MealPlanner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MealPlanner.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MealPlanner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MealPlanner_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MealPlanner] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MealPlanner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MealPlanner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MealPlanner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MealPlanner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MealPlanner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MealPlanner] SET ARITHABORT OFF 
GO
ALTER DATABASE [MealPlanner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MealPlanner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MealPlanner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MealPlanner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MealPlanner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MealPlanner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MealPlanner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MealPlanner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MealPlanner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MealPlanner] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MealPlanner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MealPlanner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MealPlanner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MealPlanner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MealPlanner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MealPlanner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MealPlanner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MealPlanner] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MealPlanner] SET  MULTI_USER 
GO
ALTER DATABASE [MealPlanner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MealPlanner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MealPlanner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MealPlanner] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MealPlanner] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MealPlanner] SET QUERY_STORE = OFF
GO
USE [MealPlanner]
GO
/****** Object:  Table [dbo].[BodyStats]    Script Date: 4/21/2019 12:31:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BodyStats](
	[BodyStatId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Height] [float] NOT NULL,
	[Weight] [float] NOT NULL,
	[TargetWeight] [float] NOT NULL,
	[TargetCalories] [float] NOT NULL,
	[TargetDays] [int] NOT NULL,
	[BMI] [float] NOT NULL,
	[ActivityLevel] [varchar](30) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [bit] NULL,
 CONSTRAINT [PK_BodyStats_1] PRIMARY KEY CLUSTERED 
(
	[BodyStatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food_Eaten_By_Day]    Script Date: 4/21/2019 12:31:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food_Eaten_By_Day](
	[EntryId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[API_Id_List] [nchar](10) NOT NULL,
	[Date] [date] NOT NULL,
	[TotalCalories] [float] NOT NULL,
 CONSTRAINT [PK_Food_Eateb_By_Day] PRIMARY KEY CLUSTERED 
(
	[EntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 4/21/2019 12:31:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[FoodId] [int] NOT NULL,
	[FoodName] [nvarchar](50) NOT NULL,
	[API_Id] [int] NOT NULL,
	[isBreakfast] [bit] NOT NULL,
	[isLunch] [bit] NOT NULL,
	[isDinner] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/21/2019 12:31:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
    [Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BodyStats]  WITH CHECK ADD  CONSTRAINT [FK_BodyStats_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[BodyStats] CHECK CONSTRAINT [FK_BodyStats_User]
GO
/****** Object:  StoredProcedure [dbo].[AddNewUser]    Script Date: 4/21/2019 12:31:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddNewUser]
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@Email NVARCHAR(50),
@Password NVARCHAR(50)
AS
BEGIN
INSERT INTO dbo.[User]
OUTPUT Inserted.UserID
VALUES
(
 @FirstName,
 @LastName,
 @Email,
 @Password
)

END



GO
/****** Object:  StoredProcedure [dbo].[AddUserBodyStats]    Script Date: 4/21/2019 12:31:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserBodyStats]
@UserId INT,
@Height FLOAT,
@Weight FLOAT,
@TargetWeight FLOAT,
@TargetCalories FLOAT,
@TargetDays INT,
@BMI FLOAT,
@ActivityLevel VARCHAR(30),
@Age INT,
@Gender BIT
AS
BEGIN
INSERT INTO dbo.[BodyStats]
OUTPUT Inserted.BodyStatId
VALUES
(
@UserId,
@Height,
@Weight,
@TargetWeight ,
@TargetCalories,
@TargetDays,
@BMI,
@ActivityLevel,
@Age,
@Gender
)

END

GO
USE [master]
GO
ALTER DATABASE [MealPlanner] SET  READ_WRITE 
GO

USE [MealPlanner]
GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 5/8/2019 7:26:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ValidateUser]
@InputEmail NVARCHAR(50),
@InputPassword NVARCHAR(50)
AS
BEGIN
SELECT * FROM dbo.[User] WHERE 
Email = @InputEmail
AND Password = @InputPassword

END
