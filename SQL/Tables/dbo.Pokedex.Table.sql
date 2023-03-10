USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[Pokedex]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pokedex](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[National Pokédex Number] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Height] [nvarchar](50) NOT NULL,
	[Weight] [nvarchar](50) NOT NULL,
	[PrimaryImageUrl] [nvarchar](500) NOT NULL,
	[Summary] [nvarchar](500) NOT NULL,
	[Gender] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Pokedex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pokedex] ADD  CONSTRAINT [DF_Pokedex_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Pokedex] ADD  CONSTRAINT [DF_Pokedex_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
