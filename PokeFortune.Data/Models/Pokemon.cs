using PokeFortune.Data.Enums;
using PokeFortune.Data.Helpers;
using PokeFortune.Data.Properties;
using System.Collections.Generic;

namespace PokeFortune.Data.Models
{
	public class Pokemon : PokeBase
	{
		public Pokemon(int id, string name, PokemonType firstPokemonType, PokemonType secondPokemonType = PokemonType.None, LegendaryType legendaryType = LegendaryType.None, bool hasFemale = false) 
			: base (id, name, PokemonHelper.GetGenerationTypeByID(id), firstPokemonType, secondPokemonType)
		{
			LegendaryType = legendaryType;

			Sprite = PokemonIconPool.GetPokemonSprite(ID, string.Empty);
			ShinySprite = PokemonIconPool.GetPokemonSprite(ID, string.Empty, true);

			if (hasFemale)
				Female = new FemaleMon(this);
		}

		#region Properties

		public bool HasFormName => !string.IsNullOrEmpty(FormName);

		public string FormName { get; init; } = string.Empty;

		public string FullName => Name + (HasFormName ? $" ({FormName})" : string.Empty);

		public LegendaryType LegendaryType { get; }

		public bool HasFemale => Female != null;

		public FemaleMon Female { get; }

		public List<PokeForm> Forms { get; } = new List<PokeForm>();

		#endregion Properties
	}

	public class FemaleMon : PokeBase
	{
		protected FemaleMon(int id, string name, GenerationType generationType, PokemonType firstPokemonType, PokemonType secondPokemonType = PokemonType.None) : base(id, name, generationType, firstPokemonType, secondPokemonType)
		{
			Sprite = PokemonIconPool.GetPokemonSprite(id, "f");
			ShinySprite = PokemonIconPool.GetPokemonSprite(id, "f", true);
		}

		public FemaleMon(Pokemon orig)
			: this(orig.ID, orig.Name, orig.GenerationType, orig.FirstPokemonType, orig.SecondPokemonType)
		{
			Original = orig;
		}

		public Pokemon Original { get; }

		public string FullName => $"{Original.Name} ({Resources.Form_Female})";
	}
}