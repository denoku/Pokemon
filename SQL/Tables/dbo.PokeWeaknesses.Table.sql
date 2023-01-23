USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[PokeWeaknesses]    Script Date: 23/01/2023 13:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokeWeaknesses](
	[PokemonId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
 CONSTRAINT [PK_PokeWeaknesses] PRIMARY KEY CLUSTERED 
(
	[PokemonId] ASC,
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
