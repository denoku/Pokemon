USE [C121_hotsauce2888_gmail]
GO
/****** Object:  UserDefinedTableType [dbo].[PokemonCategories]    Script Date: 23/01/2023 13:00:18 ******/
CREATE TYPE [dbo].[PokemonCategories] AS TABLE(
	[Name] [nvarchar](50) NOT NULL
)
GO
