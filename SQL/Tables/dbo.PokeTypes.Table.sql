USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[PokeTypes]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokeTypes](
	[PokemonId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
 CONSTRAINT [PK_PokeTypes] PRIMARY KEY CLUSTERED 
(
	[PokemonId] ASC,
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PokeTypes]  WITH CHECK ADD  CONSTRAINT [FK_PokeTypes_Pokedex] FOREIGN KEY([PokemonId])
REFERENCES [dbo].[Pokedex] ([Id])
GO
ALTER TABLE [dbo].[PokeTypes] CHECK CONSTRAINT [FK_PokeTypes_Pokedex]
GO
ALTER TABLE [dbo].[PokeTypes]  WITH CHECK ADD  CONSTRAINT [FK_PokeTypes_PokemonType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[PokemonType] ([Id])
GO
ALTER TABLE [dbo].[PokeTypes] CHECK CONSTRAINT [FK_PokeTypes_PokemonType]
GO
