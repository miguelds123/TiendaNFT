USE [master]
GO
/****** Object:  Database [ProyectoNFT]    Script Date: 16/04/2024 08:49:32 a. m. ******/
CREATE DATABASE [ProyectoNFT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoNFT', FILENAME = N'C:\Universidad\Progra Web 2\Proyecto\Base de Datos\Proyecto\ProyectoNFT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoNFT_log', FILENAME = N'C:\Universidad\Progra Web 2\Proyecto\Base de Datos\Proyecto\ProyectoNFT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProyectoNFT] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoNFT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoNFT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoNFT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoNFT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoNFT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoNFT] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoNFT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProyectoNFT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoNFT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoNFT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoNFT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoNFT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoNFT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoNFT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoNFT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoNFT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProyectoNFT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoNFT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoNFT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoNFT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoNFT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoNFT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoNFT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoNFT] SET RECOVERY FULL 
GO
ALTER DATABASE [ProyectoNFT] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoNFT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoNFT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoNFT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoNFT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoNFT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoNFT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProyectoNFT', N'ON'
GO
ALTER DATABASE [ProyectoNFT] SET QUERY_STORE = OFF
GO
USE [ProyectoNFT]
GO
USE [ProyectoNFT]
GO
/****** Object:  Sequence [dbo].[ReceiptNumber]    Script Date: 16/04/2024 08:49:32 a. m. ******/
CREATE SEQUENCE [dbo].[ReceiptNumber] 
 AS [bigint]
 START WITH 0
 INCREMENT BY 1
 MINVALUE 0
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido1] [varchar](50) NOT NULL,
	[Apellido2] [varchar](50) NOT NULL,
	[Correo] [varchar](50) NOT NULL,
	[Sexo] [char](1) NOT NULL,
	[FechaN] [datetime] NOT NULL,
	[IdPais] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
	[Cedula] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClienteNFT]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClienteNFT](
	[IdNFT] [varchar](200) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Estado] [bit] NULL,
	[idFactura] [int] NULL,
	[nombreNFT] [varchar](50) NULL,
 CONSTRAINT [PK_ClienteNFT_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaDetalle]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaDetalle](
	[IdFactura] [int] NOT NULL,
	[IdDetalle] [int] NOT NULL,
	[IdNFT] [varchar](200) NULL,
	[Precio] [numeric](18, 2) NULL,
 CONSTRAINT [PK_FacturaDetalle] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC,
	[IdDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacturaEncabezado]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacturaEncabezado](
	[Id] [int] NOT NULL,
	[IdTipoTarjeta] [int] NULL,
	[IdCliente] [int] NULL,
	[Fecha] [datetime] NULL,
	[estado] [bit] NULL,
	[numeroTarjeta] [varchar](50) NULL,
 CONSTRAINT [PK_FacturaEncabezado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NFT]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFT](
	[Id] [varchar](200) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Autor] [varchar](50) NOT NULL,
	[Valor] [numeric](18, 2) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Imagen] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_NFT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pais](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoTarjeta]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoTarjeta](
	[IdTipoTarjeta] [int] IDENTITY(1,1) NOT NULL,
	[Descrpcion] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TipoTarjeta] PRIMARY KEY CLUSTERED 
(
	[IdTipoTarjeta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 16/04/2024 08:49:32 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[NombreUsuario] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido1] [varchar](50) NOT NULL,
	[Apellido2] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL,
	[Contrasenna] [varchar](50) NOT NULL,
	[IdTipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Pais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[Pais] ([Id])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Pais]
GO
ALTER TABLE [dbo].[ClienteNFT]  WITH CHECK ADD  CONSTRAINT [FK_ClienteNFT_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
GO
ALTER TABLE [dbo].[ClienteNFT] CHECK CONSTRAINT [FK_ClienteNFT_Cliente]
GO
ALTER TABLE [dbo].[ClienteNFT]  WITH CHECK ADD  CONSTRAINT [FK_ClienteNFT_FacturaEncabezado] FOREIGN KEY([idFactura])
REFERENCES [dbo].[FacturaEncabezado] ([Id])
GO
ALTER TABLE [dbo].[ClienteNFT] CHECK CONSTRAINT [FK_ClienteNFT_FacturaEncabezado]
GO
ALTER TABLE [dbo].[ClienteNFT]  WITH CHECK ADD  CONSTRAINT [FK_ClienteNFT_NFT] FOREIGN KEY([IdNFT])
REFERENCES [dbo].[NFT] ([Id])
GO
ALTER TABLE [dbo].[ClienteNFT] CHECK CONSTRAINT [FK_ClienteNFT_NFT]
GO
ALTER TABLE [dbo].[FacturaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_FacturaDetalle_FacturaEncabezado] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[FacturaEncabezado] ([Id])
GO
ALTER TABLE [dbo].[FacturaDetalle] CHECK CONSTRAINT [FK_FacturaDetalle_FacturaEncabezado]
GO
ALTER TABLE [dbo].[FacturaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_FacturaDetalle_NFT] FOREIGN KEY([IdNFT])
REFERENCES [dbo].[NFT] ([Id])
GO
ALTER TABLE [dbo].[FacturaDetalle] CHECK CONSTRAINT [FK_FacturaDetalle_NFT]
GO
ALTER TABLE [dbo].[FacturaEncabezado]  WITH CHECK ADD  CONSTRAINT [FK_FacturaEncabezado_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
GO
ALTER TABLE [dbo].[FacturaEncabezado] CHECK CONSTRAINT [FK_FacturaEncabezado_Cliente]
GO
ALTER TABLE [dbo].[FacturaEncabezado]  WITH CHECK ADD  CONSTRAINT [FK_FacturaEncabezado_TipoTarjeta] FOREIGN KEY([IdTipoTarjeta])
REFERENCES [dbo].[TipoTarjeta] ([IdTipoTarjeta])
GO
ALTER TABLE [dbo].[FacturaEncabezado] CHECK CONSTRAINT [FK_FacturaEncabezado_TipoTarjeta]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_TipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[TipoUsuario] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_TipoUsuario]
GO
USE [master]
GO
ALTER DATABASE [ProyectoNFT] SET  READ_WRITE 
GO
