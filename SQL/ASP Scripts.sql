USE [master]
GO
/****** Object:  Database [MealPlanner]    Script Date: 4/29/2019 8:10:01 PM ******/
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
/****** Object:  Table [dbo].[AllergiesStats]    Script Date: 4/29/2019 8:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllergiesStats](
	[AllergiesStatsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Gluten] [bit] NOT NULL,
	[Peanut] [bit] NOT NULL,
	[Eggs] [bit] NOT NULL,
	[Dairy] [bit] NOT NULL,
	[Soy] [bit] NOT NULL,
	[Onion] [bit] NOT NULL,
	[Nuts] [bit] NOT NULL,
	[Seafood] [bit] NOT NULL,
 CONSTRAINT [PK_AllergiesStats] PRIMARY KEY CLUSTERED 
(
	[AllergiesStatsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BodyStats]    Script Date: 4/29/2019 8:10:02 PM ******/
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
	[ActivityLevel] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[AddDate]  AS (getdate()),
 CONSTRAINT [PK_BodyStats_1] PRIMARY KEY CLUSTERED 
(
	[BodyStatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Menu]    Script Date: 4/29/2019 8:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MealPlanId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Breakfast] [nvarchar](MAX) NOT NULL,
	[Lunch] [nvarchar](MAX) NOT NULL,
	[Dinner] [nvarchar](MAX) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/29/2019 8:10:02 PM ******/
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
SET IDENTITY_INSERT [dbo].[BodyStats] ON 

INSERT [dbo].[BodyStats] ([BodyStatId], [UserId], [Height], [Weight], [TargetWeight], [TargetCalories], [TargetDays], [BMI], [ActivityLevel], [Age], [Gender]) VALUES (17, 1, 157.5, 52.2, 52.2, 1366, 34, 21.043083900226758, 0, 27, 0)
INSERT [dbo].[BodyStats] ([BodyStatId], [UserId], [Height], [Weight], [TargetWeight], [TargetCalories], [TargetDays], [BMI], [ActivityLevel], [Age], [Gender]) VALUES (22, 8, 165, 65.25, 65.25, 1459, 36, 23.966942148760332, 0, 44, 0)
INSERT [dbo].[BodyStats] ([BodyStatId], [UserId], [Height], [Weight], [TargetWeight], [TargetCalories], [TargetDays], [BMI], [ActivityLevel], [Age], [Gender]) VALUES (25, 11, 167.5, 69.75, 69.75, 1418, 55, 24.860770773000667, 2, 33, 1)
INSERT [dbo].[BodyStats] ([BodyStatId], [UserId], [Height], [Weight], [TargetWeight], [TargetCalories], [TargetDays], [BMI], [ActivityLevel], [Age], [Gender]) VALUES (26, 1, 165, 59.85, 59.85, 1244, 24, 21.983471074380169, 3, 45, 1)
SET IDENTITY_INSERT [dbo].[BodyStats] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (1, N'jingfei2', N'zhang22', N'jingfei@student.fairfield.edu', N'123434')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (2, N'Jingfei1', N'zhang1', N'123@gmail.com', N'12345')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (3, N'Mary', N'Smith', N'mary@gmail.com', N'123456')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (4, N'CECE', N'Smith', N'cece@gmail.com', N'cece1234')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (5, N'Jack', N'Smith', N'jack@gmail.com', N'jack1234')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (6, N'Jack1', N'Smith', N'jack1@gmail.com', N'jack11234')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (7, N'John', N'Tylor', N'john@gmail.com', N'john1234')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (8, N'John2', N'Tylor', N'John2@gmail.com', N'john21234')
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [Email], [Password]) VALUES (11, N'xixi', N'yu', N'xixi@gmail.com', N'xixi1234')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Gluten]  DEFAULT ((0)) FOR [Gluten]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Peanut]  DEFAULT ((0)) FOR [Peanut]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Eggs]  DEFAULT ((0)) FOR [Eggs]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Dairy]  DEFAULT ((0)) FOR [Dairy]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Soy]  DEFAULT ((0)) FOR [Soy]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Onion]  DEFAULT ((0)) FOR [Onion]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Nuts]  DEFAULT ((0)) FOR [Nuts]
GO
ALTER TABLE [dbo].[AllergiesStats] ADD  CONSTRAINT [DF_AllergiesStats_Seafood]  DEFAULT ((0)) FOR [Seafood]
GO
ALTER TABLE [dbo].[BodyStats] ADD  CONSTRAINT [DF_BodyStats_ActivityLevel]  DEFAULT ((0)) FOR [ActivityLevel]
GO
ALTER TABLE [dbo].[BodyStats] ADD  CONSTRAINT [DF_BodyStats_Gender]  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[BodyStats]  WITH CHECK ADD  CONSTRAINT [FK_BodyStats_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[BodyStats] CHECK CONSTRAINT [FK_BodyStats_User]

/****** Object:  StoredProcedure [dbo].[spAddNewBodyStats]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddNewBodyStats]
@UserId INT,
@Height FLOAT,
@Weight FLOAT,
@TargetWeight FLOAT,
@TargetCalories FLOAT,
@TargetDays INT,
@BMI FLOAT,
@ActivityLevel NVARCHAR(30),
@Age int,
@Gender bit

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
/****** Object:  StoredProcedure [dbo].[spAddNewUser]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAddNewUser]
@FirstName nvarchar(50),  
@LastName nvarchar(50),  
@Email nvarchar(50),
@Password  nvarchar(50)
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
/****** Object:  StoredProcedure [dbo].[spValidateUser]    Script Date: 5/8/2019 7:26:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spValidateUser]
@InputEmail NVARCHAR(50),
@InputPassword NVARCHAR(50)
AS
BEGIN
SELECT * FROM dbo.[User] WHERE 
Email = @InputEmail
AND Password = @InputPassword

END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBodyStats]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spDeleteBodyStats]
@BodyStatId int
as
Begin
 Delete from dbo.BodyStats
 where BodyStatId = @BodyStatId
End
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spDeleteUser]
@UserID int
as
Begin
 Delete from dbo.[User] 
 where UserID = @UserID
End
GO
/****** Object:  StoredProcedure [dbo].[spGetAllBodyStats]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetAllBodyStats]
@UserId int
as
Begin
 Select BodyStatId, UserId, Height, [Weight], TargetWeight, TargetCalories, TargetDays, BMI, ActivityLevel,Age, Gender, AddDate
 from dbo.BodyStats
 where UserId= @UserId
End
GO
/****** Object:  StoredProcedure [dbo].[spGetUser]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetUser]
@UserID int
as
Begin
 Select FirstName, LastName, Email, [Password]
 from dbo.[User]
 where UserID= @UserID
End
GO
/****** Object:  StoredProcedure [dbo].[spGetUserId]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetUserId]
@BodyStatId int
as
Begin
 Select UserId from dbo.BodyStats
 where BodyStatId = @BodyStatId
End
GO
/****** Object:  StoredProcedure [dbo].[spGetUserIdOnName]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetUserIdOnName]
@FirstName nvarchar(50),
@LastName nvarchar(50)
as
Begin
 Select UserId from dbo.[User]
 where FirstName=@FirstName 
 AND LastName=@LastName
End
GO
/****** Object:  StoredProcedure [dbo].[spSaveBodyStats]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[spSaveBodyStats]
@BodyStatId int,
@Height FLOAT,
@Weight FLOAT,
@TargetWeight FLOAT,
@TargetCalories FLOAT,
@TargetDays INT,
@BMI FLOAT,
@ActivityLevel NVARCHAR(30),
@Age int,
@Gender bit
as
Begin
 Update dbo.BodyStats
 SET
 Height=@Height, 
 [Weight]=@Weight, 
 TargetWeight=@TargetWeight, 
 TargetCalories=@TargetCalories, 
 TargetDays=@TargetDays, 
 BMI=@BMI, 
 ActivityLevel=@ActivityLevel,
 Age=@Age , 
 Gender=@Gender
 Where BodyStatId=@BodyStatId
End

GO
/****** Object:  StoredProcedure [dbo].[spSaveUser]    Script Date: 4/29/2019 8:10:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSaveUser]
@UserID int,
@FirstName nvarchar(50),  
@LastName nvarchar(50),  
@Email nvarchar(50),
@Password  nvarchar(50)
as  
Begin  
 Update  dbo.[User]
 Set 
 FirstName=@FirstName,
 LastName= @LastName,
 Email= @Email,
 Password= @Password 
 WHERE UserID = @UserID
End
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE dbo.spSaveMenu 
    -- Add the parameters for the stored procedure here
    @MealPlanId int,
    @UserId int,
    @Breakfast nvarchar(max),
    @Lunch nvarchar(max),
    @Dinner nvarchar(max)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    Insert into dbo.Menu(MealPlanId, UserId, Breakfast, Lunch, Dinner)
    Values (@MealPlanId, @UserId, @Breakfast, @Lunch, @Dinner)
END
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE dbo.spLoadMenu 
    -- Add the parameters for the stored procedure here
    @MealPlanId int
    AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    select * 
    from dbo.Menu
    where MealPlanId = @MealPlanId
END
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE dbo.spHistoricalMealPlans 
    -- Add the parameters for the stored procedure here
    @UserId int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT *
    From dbo.Menu
    Where UserId = @UserId
END
GO
USE [master]
GO
ALTER DATABASE [MealPlanner] SET  READ_WRITE 
GO






