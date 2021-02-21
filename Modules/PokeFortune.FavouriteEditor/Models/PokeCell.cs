using PokeFortune.Data.Enums;
using PokeFortune.Data.Models;

namespace PokeFortune.FavouriteEditor.Models
{
	public class PokeCell
	{
		private PokeCell(PokeBase poke, string id, string name)
		{
			Pokemon = poke;
			ID = id;
			Name = name;
		}

		public PokeCell(Pokemon poke)
			: this(poke, poke.ID.ToString(), poke.FullName)
		{
			IsLegendary = poke.LegendaryType != LegendaryType.None;
		}

		public PokeCell(PokeForm poke)
			: this(poke, $"{poke.Original.ID}-{poke.ID}", poke.FullName)
		{
			IsLegendary = poke.Original.LegendaryType != LegendaryType.None;
			IsTransformation = poke.FormType == FormType.Mega 
												|| poke.FormType == FormType.Primal
												|| poke.FormType == FormType.Gigantamax;
		}

		public PokeCell(FemaleMon poke)
			: this (poke,$"{poke.Original.ID}-f", poke.FullName)
		{
			IsLegendary = poke.Original.LegendaryType != LegendaryType.None;
		}

		public PokeBase Pokemon { get; }

		public string ID { get; }
		public string Name { get; }

		public bool IsLegendary { get; } = false;
		public bool IsTransformation { get; } = false;
	}
}
