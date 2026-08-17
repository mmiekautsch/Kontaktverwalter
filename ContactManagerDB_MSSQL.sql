-- Skript zur Erstellung der ContactManagerDB
-- mit Lösung der einzelnen Aufgaben

USE [master]
GO

/****** Object:  Database [ContactManagerDB]    Script Date: 15.08.2026 12:01:18 ******/
CREATE DATABASE [ContactManagerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContactManagerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ContactManagerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ContactManagerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ContactManagerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactManagerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ContactManagerDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ContactManagerDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ContactManagerDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ContactManagerDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ContactManagerDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [ContactManagerDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ContactManagerDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ContactManagerDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ContactManagerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ContactManagerDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ContactManagerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ContactManagerDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ContactManagerDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ContactManagerDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ContactManagerDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ContactManagerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ContactManagerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ContactManagerDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ContactManagerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ContactManagerDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ContactManagerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ContactManagerDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ContactManagerDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ContactManagerDB] SET  MULTI_USER 
GO

ALTER DATABASE [ContactManagerDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ContactManagerDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ContactManagerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ContactManagerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ContactManagerDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ContactManagerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [ContactManagerDB] SET QUERY_STORE = ON
GO

ALTER DATABASE [ContactManagerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [ContactManagerDB] SET  READ_WRITE 
GO

CREATE LOGIN ContactManagerDB_rw
WITH PASSWORD = 'ContactManagerDB_rw',
     CHECK_POLICY = OFF,
     CHECK_EXPIRATION = OFF;
GO

USE [ContactManagerDB]
GO
/****** Object:  User [ContactManagerDB_rw]    Script Date: 15.08.2026 12:00:21 ******/
CREATE USER [ContactManagerDB_rw] FOR LOGIN [ContactManagerDB_rw] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ContactManagerDB_rw]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ContactManagerDB_rw]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 15.08.2026 12:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID_Address] [bigint] IDENTITY(1,1) NOT NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[StreetName] [nvarchar](100) NOT NULL,
	[StreetNumber] [nvarchar](10) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[FK_Person] [bigint] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID_Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 15.08.2026 12:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[ID_Person] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[ID_Person] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneContact]    Script Date: 15.08.2026 12:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneContact](
	[ID_PhoneContact] [bigint] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](30) NOT NULL,
	[FK_Person] [bigint] NOT NULL,
 CONSTRAINT [PK_PhoneContact] PRIMARY KEY CLUSTERED 
(
	[ID_PhoneContact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Person]
GO
ALTER TABLE [dbo].[PhoneContact]  WITH CHECK ADD  CONSTRAINT [FK_PhoneContact_Person] FOREIGN KEY([FK_Person])
REFERENCES [dbo].[Person] ([ID_Person])
GO
ALTER TABLE [dbo].[PhoneContact] CHECK CONSTRAINT [FK_PhoneContact_Person]
GO


-- Musterdaten 
INSERT INTO [ContactManagerDB].[dbo].[Person] (LastName, FirstName, DateOfBirth) VALUES
(N'Schmidt', N'Anna',   '1988-04-12'),
(N'Müller',  N'Laura',  '1995-07-21'),
(N'Yilmaz',  N'Mehmet', '1979-11-03'),
(N'König',   N'Paul',   '2001-02-15'),
(N'Neumann', N'Sophie', '2002-06-29');
GO

INSERT INTO [ContactManagerDB].[dbo].[Address] (PostalCode, City, StreetName, StreetNumber, Country, FK_Person) VALUES
(N'01067', N'Dresden', N'Postplatz',       N'1',  N'Deutschland', 1),
(N'10115', N'Berlin',  N'Hauptstraße',     N'12', N'Deutschland', 2),
(N'01069', N'Dresden', N'Prager Straße',   N'7a', N'Deutschland', 3),
(N'50667', N'Köln',    N'Marktplatz',      N'7',  N'Deutschland', 4),
(N'01099', N'Dresden', N'Alaunstraße',     N'24', N'Deutschland', 5),
(N'04109', N'Leipzig', N'Katharinenstraße',N'2',  N'Deutschland', 5);
GO

INSERT INTO [ContactManagerDB].[dbo].[PhoneContact] (PhoneNumber, [Type], FK_Person) VALUES
(N'+49 351 123456', N'Privat', 1),
(N'0171 987654',    N'Mobil', 1),
(N'+49 351 456789', N'Geschäftlich', 2),
(N'0221 456789',    N'Privat', 3),
(N'+49 160 111222', N'Mobil', 4),
(N'12345',          N'Ungültiges Muster', 5),
(N'+49 341 555555', N'Privat', 5);
GO

-- Wie viele Personendatensätze sind vorhanden? 
SELECT COUNT(*) AS AnzahlPersonen
FROM [ContactManagerDB].[dbo].[Person];
GO

-- Wie viele unterschiedliche Personen wohnen in Dresden? 
SELECT COUNT(DISTINCT FK_Person) AS AnzahlPersonenInDresden
FROM [ContactManagerDB].[dbo].[Address]
WHERE City = 'Dresden';
GO

-- Wie viele Personen haben mehr als eine Telefonnummer? 
SELECT COUNT(*) AS AnzahlPersonenMitMehrerenTelefonnummern
FROM
(
    SELECT FK_Person
    FROM [ContactManagerDB].[dbo].[PhoneContact]
    GROUP BY FK_Person
    HAVING COUNT(*) > 1
) AS a;
GO

-- Anzahl unterschiedlicher Personen pro Ort. 
SELECT City, COUNT(DISTINCT FK_Person) AS AnzahlPersonen
FROM [ContactManagerDB].[dbo].[Address]
GROUP BY City;
GO

-- View: Personen mit Anschriften und Telefonnummern. 
CREATE VIEW dbo.ViewFullContactInfo
AS
    SELECT
        p.ID_Person,
        p.FirstName,
        p.LastName,
        p.DateOfBirth,
        a.PostalCode,
        a.City,
        a.StreetName,
        a.StreetNumber,
        a.Country,
        t.PhoneNumber,
        t.[Type]
    FROM [ContactManagerDB].[dbo].[Person] AS p
    LEFT JOIN [ContactManagerDB].[dbo].[Address] AS a ON a.FK_Person = p.ID_Person
    LEFT JOIN [ContactManagerDB].[dbo].[PhoneContact] AS t ON t.FK_Person = p.ID_Person;
GO

-- Telefonnummern löschen, die weder mit 0 noch mit + beginnen. 
DELETE FROM [ContactManagerDB].[dbo].[PhoneContact]
WHERE PhoneNumber NOT LIKE N'0%'
  AND PhoneNumber NOT LIKE N'+%';
GO

-- Person um eine Spalte für den Namen in Großschreibung erweitern. 
ALTER TABLE [ContactManagerDB].[dbo].[Person]
ADD LastNameUpperCase nvarchar(100) NULL;
GO

-- Neue Spalte vollständig in Großbuchstaben befüllen. 
UPDATE [ContactManagerDB].[dbo].[Person]
SET LastNameUpperCase = UPPER(LastName);
GO

