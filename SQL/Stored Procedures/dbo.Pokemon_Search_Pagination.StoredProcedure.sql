USE [C121_hotsauce2888_gmail]
GO
/****** Object:  StoredProcedure [dbo].[Pokemon_Search_Pagination]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create proc [dbo].[Pokemon_Search_Pagination]

					 @PageIndex int
					,@PageSize int
					,@Query nvarchar(100)

	as

	/*
	Declare @PageIndex int  = 0
			,@PageSize int  = 12
			,@Query nvarchar(100) = "bulbasaur"

			Execute [dbo].[Pokemon_Search_Pagination]
							 @PageIndex
							,@PageSize
							,@Query
	*/

	Begin

	Declare @offSet int = @PageIndex * @PageSize

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
		  --,pa.Name Ability
		  --,pa.Description
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
		,TotalCount = Count(1) Over()
	From dbo.Pokedex p
	inner join dbo.PokemonCategory pc on p.CategoryId = pc.Id
	--inner join dbo.PokemonAbilities pa on p.AbilityId = pa.Id
	WHERE (p.[National Pokédex Number] LIKE '%' + @Query + '%')
	OR (p.Name LIKE '%' + @Query + '%')
	order by p.[National Pokédex Number]

	Offset @offSet Rows
	Fetch Next @PageSize Rows only


	End
GO
