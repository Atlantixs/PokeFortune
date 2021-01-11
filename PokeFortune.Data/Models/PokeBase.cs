using PokeFortune.Data.Enums;
using System;

namespace PokeFortune.Data.Models
{
	public abstract class PokeBase
	{
		protected PokeBase(int id, string name, GenerationType generationType, PokemonType firstPokemonType, PokemonType secondPokemonType = PokemonType.None)
		{
			ID = id;
			Name = name;

			GenerationType = generationType;

			FirstPokemonType = firstPokemonType;
			SecondPokemonType = secondPokemonType;
		}

		#region Properties

		/// <summary>
		/// ID des Pokemon
		/// </summary>
		public int ID { get; }

		public string Name { get; }

		public PokemonType FirstPokemonType { get; }
		public PokemonType SecondPokemonType { get; }

		public GenerationType GenerationType { get; }

		public Uri Sprite { get; init; }
		public Uri ShinySprite { get; init; }

		#endregion

		#region Methods

		public bool HasPokemonType(PokemonType type)
		{
			if (type == PokemonType.None)
				return true;

			return FirstPokemonType == type || SecondPokemonType == type;
		}

		public bool HasPokemonType(PokemonType firstType, PokemonType secondType)
		{
			if (firstType == PokemonType.None)
				return HasPokemonType(secondType);
			else if (secondType == PokemonType.None)
				return HasPokemonType(firstType);
			else
				return (FirstPokemonType == firstType && SecondPokemonType == secondType)
							|| (FirstPokemonType == secondType && SecondPokemonType == firstType);
		}

		#endregion
	}
}
