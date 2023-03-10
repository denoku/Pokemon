USE [C121_hotsauce2888_gmail]
GO
/****** Object:  StoredProcedure [dbo].[Pokedex_Select_By_Id]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE Proc [dbo].[Pokedex_Select_By_Id]

				@pokedexId int


as  

	/*

	Declare @PokedexId int = 001;

	Execute dbo.Pokedex_Select_By_Id @pokedexId
			

	*/

Begin


	Select p.Id
		  ,p.[National Pokédex Number]
		  ,p.Name
		  ,p.Height
		  ,p.Weight
		  ,p.PrimaryImageUrl
		  ,p.Summary
		  ,p.Gender
		  ,p.CategoryId
		  ,pc.Name Category
		  ,Abilities = (
				select distinct pa.Id, pa.Name, pa.Description
				from dbo.PokemonAbilities pa inner join dbo.PokeAbilities a
								on pa.Id = a.AbilityId
				where a.PokemonId = p.Id
				for json path
				)
		  ,Type = (
				select distinct pt.Id, pt.Name
				from dbo.PokemonType pt inner join dbo.PokeTypes t
								on pt.Id = t.TypeId
				where t.PokemonId = p.Id
				for JSON path 
				)
		,Weaknessess = (
					select distinct pt.Id, pt.Name

					from dbo.PokemonType pt inner join dbo.PokeWeaknesses pw
								on pt.Id = pw.TypeId
					where pw.PokemonId = p.Id
					for json path
					)
		,DateCreated
		,DateModified

	From dbo.Pokedex p
	inner join dbo.PokemonCategory pc on p.CategoryId = pc.Id
	Where p.[National Pokédex Number] = @pokedexId




End
GO
