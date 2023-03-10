USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[PokeAbilities]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokeAbilities](
	[PokemonId] [int] NOT NULL,
	[AbilityId] [int] NOT NULL,
 CONSTRAINT [PK_PokeAbilities] PRIMARY KEY CLUSTERED 
(
	[PokemonId] ASC,
	[AbilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PokeAbilities]  WITH CHECK ADD  CONSTRAINT [FK_PokeAbilities_Pokedex] FOREIGN KEY([PokemonId])
REFERENCES [dbo].[Pokedex] ([Id])
GO
ALTER TABLE [dbo].[PokeAbilities] CHECK CONSTRAINT [FK_PokeAbilities_Pokedex]
GO
ALTER TABLE [dbo].[PokeAbilities]  WITH CHECK ADD  CONSTRAINT [FK_PokeAbilities_PokemonAbilities] FOREIGN KEY([AbilityId])
REFERENCES [dbo].[PokemonAbilities] ([Id])
GO
ALTER TABLE [dbo].[PokeAbilities] CHECK CONSTRAINT [FK_PokeAbilities_PokemonAbilities]
GO
