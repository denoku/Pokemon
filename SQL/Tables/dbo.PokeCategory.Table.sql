USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[PokeCategory]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokeCategory](
	[PokemonId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_PokeCategory] PRIMARY KEY CLUSTERED 
(
	[PokemonId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PokeCategory]  WITH CHECK ADD  CONSTRAINT [FK_PokeCategory_Pokedex] FOREIGN KEY([PokemonId])
REFERENCES [dbo].[Pokedex] ([Id])
GO
ALTER TABLE [dbo].[PokeCategory] CHECK CONSTRAINT [FK_PokeCategory_Pokedex]
GO
ALTER TABLE [dbo].[PokeCategory]  WITH CHECK ADD  CONSTRAINT [FK_PokeCategory_PokemonCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[PokemonCategory] ([Id])
GO
ALTER TABLE [dbo].[PokeCategory] CHECK CONSTRAINT [FK_PokeCategory_PokemonCategory]
GO
