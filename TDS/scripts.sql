USE [master]
GO
/****** Object:  Database [TDS]    Script Date: 11/30/2022 2:33:21 AM ******/
CREATE DATABASE [TDS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TDS', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TDS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TDS_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TDS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TDS] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TDS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TDS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TDS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TDS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TDS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TDS] SET ARITHABORT OFF 
GO
ALTER DATABASE [TDS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TDS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TDS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TDS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TDS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TDS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TDS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TDS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TDS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TDS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TDS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TDS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TDS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TDS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TDS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TDS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TDS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TDS] SET RECOVERY FULL 
GO
ALTER DATABASE [TDS] SET  MULTI_USER 
GO
ALTER DATABASE [TDS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TDS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TDS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TDS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TDS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TDS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TDS', N'ON'
GO
ALTER DATABASE [TDS] SET QUERY_STORE = OFF
GO
USE [TDS]
GO
/****** Object:  Table [dbo].[Clases]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clases](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Descripcion] [varchar](100) NULL,
	[MaestroId] [int] NULL,
	[InstitucionId] [int] NULL,
	[codigo] [varchar](200) NULL,
	[estado] [bit] NULL,
	[HoraInicio] [varchar](max) NULL,
	[HoraFin] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrega]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrega](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [text] NULL,
	[TareaId] [int] NULL,
	[EstudianteId] [int] NULL,
	[Estado] [bit] NULL,
	[Calificacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](200) NULL,
	[Apellidos] [varchar](200) NULL,
	[Correo] [varchar](200) NULL,
	[Codigo] [varchar](200) NULL,
	[Direccion] [varchar](200) NULL,
	[Telefono] [varchar](200) NULL,
	[estado] [bit] NULL,
	[InstitucionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstudiantesClases]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstudiantesClases](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EstudianteId] [int] NULL,
	[ClaseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institucion]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institucion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Descripcion] [varchar](200) NULL,
	[Codigo] [varchar](200) NULL,
	[Direccion] [varchar](200) NULL,
	[Correo] [varchar](200) NULL,
	[Telefono] [varchar](200) NULL,
	[Licencias] [int] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Maestro]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maestro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](200) NULL,
	[Apellidos] [varchar](200) NULL,
	[Correo] [varchar](200) NULL,
	[Telefono] [varchar](200) NULL,
	[Direccion] [varchar](200) NULL,
	[InstitucionId] [int] NULL,
	[estado] [bit] NULL,
	[codigo] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mensajes]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mensajes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [varchar](200) NULL,
	[ClaseId] [int] NULL,
	[EstudianteId] [int] NULL,
	[estado] [bit] NULL,
	[Fecha] [datetime] NULL,
	[MaestroId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MensajesDetalle]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MensajesDetalle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [varchar](200) NULL,
	[ClaseId] [int] NULL,
	[EstudianteId] [int] NULL,
	[MensajeId] [int] NULL,
	[Fecha] [datetime] NULL,
	[Estado] [bit] NULL,
	[MaestroId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[descripcion] [varchar](500) NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarea]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarea](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](200) NULL,
	[Descripcion] [text] NULL,
	[FechaEntrega] [datetime] NULL,
	[idClase] [int] NULL,
	[Estado] [bit] NULL,
	[codigo] [varchar](200) NULL,
	[Calificacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/30/2022 2:33:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[password] [varchar](400) NULL,
	[EstudianteId] [int] NULL,
	[MaestroId] [int] NULL,
	[RolId] [int] NULL,
	[InstitucionId] [int] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clases]  WITH CHECK ADD FOREIGN KEY([InstitucionId])
REFERENCES [dbo].[Institucion] ([id])
GO
ALTER TABLE [dbo].[Clases]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Entrega]  WITH CHECK ADD FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([id])
GO
ALTER TABLE [dbo].[Entrega]  WITH CHECK ADD FOREIGN KEY([TareaId])
REFERENCES [dbo].[Tarea] ([id])
GO
ALTER TABLE [dbo].[Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Institucion] FOREIGN KEY([InstitucionId])
REFERENCES [dbo].[Institucion] ([id])
GO
ALTER TABLE [dbo].[Estudiantes] CHECK CONSTRAINT [FK_Institucion]
GO
ALTER TABLE [dbo].[EstudiantesClases]  WITH CHECK ADD FOREIGN KEY([ClaseId])
REFERENCES [dbo].[Clases] ([id])
GO
ALTER TABLE [dbo].[EstudiantesClases]  WITH CHECK ADD FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([id])
GO
ALTER TABLE [dbo].[Maestro]  WITH CHECK ADD FOREIGN KEY([InstitucionId])
REFERENCES [dbo].[Institucion] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([ClaseId])
REFERENCES [dbo].[Clases] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[MensajesDetalle]  WITH CHECK ADD FOREIGN KEY([ClaseId])
REFERENCES [dbo].[Clases] ([id])
GO
ALTER TABLE [dbo].[MensajesDetalle]  WITH CHECK ADD FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([id])
GO
ALTER TABLE [dbo].[MensajesDetalle]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[MensajesDetalle]  WITH CHECK ADD FOREIGN KEY([MensajeId])
REFERENCES [dbo].[Mensajes] ([id])
GO
ALTER TABLE [dbo].[Tarea]  WITH CHECK ADD FOREIGN KEY([idClase])
REFERENCES [dbo].[Clases] ([id])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([id])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([InstitucionId])
REFERENCES [dbo].[Institucion] ([id])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([MaestroId])
REFERENCES [dbo].[Maestro] ([id])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([RolId])
REFERENCES [dbo].[Rol] ([id])
GO
USE [master]
GO
ALTER DATABASE [TDS] SET  READ_WRITE 
GO
