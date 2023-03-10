USE [C121_hotsauce2888_gmail]
GO
/****** Object:  Table [dbo].[PokeSkills]    Script Date: 23/01/2023 13:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PokeSkills](
	[PokemonId] [int] NOT NULL,
	[StatsId] [int] NOT NULL,
 CONSTRAINT [PK_PokeSkills] PRIMARY KEY CLUSTERED 
(
	[PokemonId] ASC,
	[StatsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PokeSkills]  WITH CHECK ADD  CONSTRAINT [FK_PokeSkills_Pokedex] FOREIGN KEY([PokemonId])
REFERENCES [dbo].[Pokedex] ([Id])
GO
ALTER TABLE [dbo].[PokeSkills] CHECK CONSTRAINT [FK_PokeSkills_Pokedex]
GO
ALTER TABLE [dbo].[PokeSkills]  WITH CHECK ADD  CONSTRAINT [FK_PokeSkills_Stats] FOREIGN KEY([StatsId])
REFERENCES [dbo].[Stats] ([Id])
GO
ALTER TABLE [dbo].[PokeSkills] CHECK CONSTRAINT [FK_PokeSkills_Stats]
GO
