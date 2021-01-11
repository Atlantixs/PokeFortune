using PokeFortune.Data.Enums;
using System;

namespace PokeFortune.Data.Models
{
	public class PokeForm : PokeBase
	{
		public PokeForm(Pokemon orig, int subId, string formName, string additionalUri, PokemonType firstType, PokemonType secondType, GenerationType generation, FormType formType = FormType.Other)
			: base(subId, formName, generation, firstType, secondType)
		{
			Original = orig ?? throw new ArgumentNullException(nameof(orig));

			FormType = formType;

			Sprite = PokemonIconPool.GetPokemonSprite(orig.ID, additionalUri);
			ShinySprite = PokemonIconPool.GetPokemonSprite(orig.ID, additionalUri, true);

			Original.Forms.Add(this);
		}

		public Pokemon Original { get; }

		public string FullName => Original.Name + $" ({Name})";

		public FormType FormType { get; set; }

		public bool HasShinyForm => ShinySprite != null;
	}
}
