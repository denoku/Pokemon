USE [C121_hotsauce2888_gmail]
GO
/****** Object:  StoredProcedure [dbo].[Pokemon_Insert]    Script Date: 05/02/2023 16:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[Pokemon_Insert]

				 @NationalPokédexNumber nvarchar(50)
				,@Name nvarchar(50)
				,@Height nvarchar(50)
				,@Weight nvarchar(50)
				,@PrimaryImageUrl nvarchar(500)
				,@Summary nvarchar(300)
				,@Gender bit
				,@CategoryId int
				,@Abilities dbo.PokemonAbilitiesV2 READONLY
				,@Type dbo.PokemonTypesV2 READONLY
				,@Weaknesses dbo.PokemonWeaknesses READONLY

				,@Id int OUTPUT
				
as

	/* ---------------------TEST CODE--------------------

		Declare @Id int = 0
		Declare @myType dbo.PokemonTypesV2

		Insert @myType (TypeId)
		Values(7)
		Insert @myType (TypeId)
		Values(13)

		Declare @myWeakness dbo.PokemonWeaknesses

		Insert @myWeakness (TypeId)
		Values(11)
		Insert @myWeakness (TypeId)
		Values(15)
		Insert @myWeakness (TypeId)
		Values(17)
		--Insert @myWeakness (TypeId)
		--Values(15)

		Declare @myAbilities (AbilityId)
		Values (9)
		Declare @myAbilities (AbilityId)
		Values (10)

							Declare  @NationalPokédexNumber nvarchar(50) = 016
									,@Name nvarchar(50) = 'Pidgey'
									,@Height nvarchar(50) = '1` 00"'
									,@Weight nvarchar(50) = '4.0 lbs'
									,@PrimaryImageUrl nvarchar(500) = 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/016.png'
									,@Summary nvarchar(300) = 'Very docile. If attacked, it will often kick up sand to protect itself rather than fight back.'
									,@Gender bit = 1
									,@CategoryId int = 12
									
								
							Execute [dbo].[Pokemon_Insert]

									 @NationalPokédexNumber
									,@Name
									,@Height
									,@Weight
									,@PrimaryImageUrl
									,@Summary
									,@Gender
									,@CategoryId
									,@AbilityId
									,@myType
									,@myWeakness
									,@myAbilities

									,@Id OUTPUT

		Select *
		from dbo.Pokedex


	*/

Begin

	INSERT [dbo].[Pokedex]
			
			([National Pokédex Number]
			,[Name]
			,[Height]
			,[Weight]
			,[PrimaryImageUrl]
			,[Summary]
			,[Gender]
			,[CategoryId])
			

	Values
			(@NationalPokédexNumber
			,@Name
			,@Height
			,@Weight
			,@PrimaryImageUrl
			,@Summary
			,@Gender
			,@CategoryId)

	Set @Id = SCOPE_IDENTITY()

	INSERT INTO dbo.PokeCategory (PokemonId, CategoryId)

	Values (@Id, @CategoryId)
	
	INSERT dbo.PokeAbilities (PokemonId, AbilityId)
	Select @Id, pa.Id
	From dbo.PokemonAbilities pa
	where exists (Select 1
					from @Abilities a
					where pa.Id = a.AbilityId)


	Insert dbo.PokeTypes (PokemonId, TypeId)
	Select @Id, pt.Id
	From dbo.PokemonType pt
	Where exists (Select 1
					from @Type t
					where pt.Id = t.TypeId)

	Insert dbo.PokeWeaknesses (PokemonId, TypeId)
	Select @Id, pt.Id
	From dbo.PokemonType pt
	Where exists (Select 1
					from @Weaknesses w
					where pt.Id = w.TypeId)



	


End
GO
