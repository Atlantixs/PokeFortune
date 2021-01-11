using System;

namespace PokeFortune.Core
{
	public static class CoreIconPool
	{
		#region enums

		/// <summary>
		/// Quelle der Icons
		/// </summary>
		private enum IconSource
		{
			/// <summary>
			/// Core
			/// </summary>
			Core,
			/// <summary>
			/// PokemonFavouriteEditor
			/// </summary>
			FavouriteEditor
		}

		/// <summary>
		/// Extension der Icons
		/// </summary>
		private enum IconExtension
		{
			/// <summary>
			/// JPEG
			/// </summary>
			jpg,

			/// <summary>
			/// PNG
			/// </summary>
			png
		}

		#endregion enums

		#region formats

		private static readonly string CORE_FORMAT = "pack://application:,,,/PokeFortune.Core;component/Resources/{0}.{1}";
		private static readonly string FAVOURITE_EDITOR_FORMAT = "pack://application:,,,/PokeFortune.FavouriteEditor;component/Resources/{0}.{1}";

		#endregion formats

		#region method

		/// <summary>
		/// Wandelt den Extension-Enumwert in einen string um!
		/// </summary>
		/// <param name="ext">Extension des Bilds</param>
		/// <returns></returns>
		private static string GetExt(IconExtension ext)
		{
			switch (ext)
			{
				case IconExtension.jpg:
					return "jpg";

				case IconExtension.png:
					return "png";
			}
			throw new InvalidOperationException("Ungültiger Aufruf!");
		}

		/// <summary>
		/// Wandelt die Quelle in einen string um!
		/// </summary>
		/// <param name="source">Quelle des Bildes</param>
		/// <returns></returns>
		private static string GetSource(IconSource source) =>
			source switch
			{
				IconSource.Core => CORE_FORMAT,
				IconSource.FavouriteEditor => FAVOURITE_EDITOR_FORMAT,
				_ => throw new InvalidOperationException("Ungültiger Aufruf!")
			};

		/// <summary>
		/// Baut aus verschiedenen definierten Informationen eine Uri auf
		/// </summary>
		/// <param name="imageName">Name des Bildes (ohne Extension)</param>
		/// <param name="iconSource">Quelle des Bildes</param>
		/// <param name="iconExtension">Extension des Bildes</param>
		/// <returns></returns>
		private static Uri GetUri(string imageName, IconSource iconSource, IconExtension iconExtension = IconExtension.png)
		{
			string format = GetSource(iconSource);
			return new Uri(string.Format(format, imageName, GetExt(iconExtension)), UriKind.RelativeOrAbsolute);
		}

		#endregion method

		#region static-Uris

		#region Allgemein

		#region PokemonTypeIcons

		/// <summary>
		/// Normal Icon
		/// </summary>
		public static readonly Uri NORMAL_ICON = GetUri("NormalIcon", IconSource.Core);

		/// <summary>
		/// Grass Icon
		/// </summary>
		public static readonly Uri GRASS_ICON = GetUri("GrassIcon", IconSource.Core);

		/// <summary>
		/// Fire Icon
		/// </summary>
		public static readonly Uri FIRE_ICON = GetUri("FireIcon", IconSource.Core);

		/// <summary>
		/// Water Icon
		/// </summary>
		public static readonly Uri WATER_ICON = GetUri("WaterIcon", IconSource.Core);

		/// <summary>
		/// Flying Icon
		/// </summary>
		public static readonly Uri FLYING_ICON = GetUri("FlyingIcon", IconSource.Core);

		/// <summary>
		/// Electric Icon
		/// </summary>
		public static readonly Uri ELECTRIC_ICON = GetUri("ElectricIcon", IconSource.Core);

		/// <summary>
		/// Bug Icon
		/// </summary>
		public static readonly Uri BUG_ICON = GetUri("BugIcon", IconSource.Core);

		/// <summary>
		/// Poison Icon
		/// </summary>
		public static readonly Uri POISON_ICON = GetUri("PoisonIcon", IconSource.Core);

		/// <summary>
		/// Rock Icon
		/// </summary>
		public static readonly Uri ROCK_ICON = GetUri("RockIcon", IconSource.Core);

		/// <summary>
		/// Ground Icon
		/// </summary>
		public static readonly Uri GROUND_ICON = GetUri("GroundIcon", IconSource.Core);

		/// <summary>
		/// Fighting Icon
		/// </summary>
		public static readonly Uri FIGHTING_ICON = GetUri("FightingIcon", IconSource.Core);

		/// <summary>
		/// Psychic Icon
		/// </summary>
		public static readonly Uri PSYCHIC_ICON = GetUri("PsychicIcon", IconSource.Core);

		/// <summary>
		/// Ghost Icon
		/// </summary>
		public static readonly Uri GHOST_ICON = GetUri("GhostIcon", IconSource.Core);

		/// <summary>
		/// Ice Icon
		/// </summary>
		public static readonly Uri ICE_ICON = GetUri("IceIcon", IconSource.Core);

		/// <summary>
		/// Dragon Icon
		/// </summary>
		public static readonly Uri DRAGON_ICON = GetUri("DragonIcon", IconSource.Core);

		/// <summary>
		/// Dark Icon
		/// </summary>
		public static readonly Uri DARK_ICON = GetUri("DarkIcon", IconSource.Core);

		/// <summary>
		/// Steel Icon
		/// </summary>
		public static readonly Uri STEEL_ICON = GetUri("SteelIcon", IconSource.Core);

		/// <summary>
		/// Fairy Icon
		/// </summary>
		public static readonly Uri FAIRY_ICON = GetUri("FairyIcon", IconSource.Core);

		/// <summary>
		/// Legendary Icon
		/// </summary>
		public static readonly Uri LEGENDARY_ICON = GetUri("LegendaryIcon", IconSource.Core);

		/// <summary>
		/// Shiny Icon
		/// </summary>
		public static readonly Uri SHINY_ICON = GetUri("ShinyIcon", IconSource.Core);

		#endregion

		#region Generations

		/// <summary>
		/// Erste Generation
		/// </summary>
		public static readonly Uri RBY_96 = GetUri("RBY_96", IconSource.Core);

		/// <summary>
		/// Zweite Generation
		/// </summary>
		public static readonly Uri GSC_96 = GetUri("GSC_96", IconSource.Core);

		/// <summary>
		/// Dritte Generation
		/// </summary>
		public static readonly Uri RSE_96 = GetUri("RSE_96", IconSource.Core);

		/// <summary>
		/// Vierte Generation
		/// </summary>
		public static readonly Uri DPP_96 = GetUri("DPP_96", IconSource.Core);

		/// <summary>
		/// Fünfte Generation
		/// </summary>
		public static readonly Uri BWB2W2_96 = GetUri("BWB2W2_96", IconSource.Core);

		/// <summary>
		/// Sechste Generation
		/// </summary>
		public static readonly Uri XY_96 = GetUri("XY_96", IconSource.Core);

		/// <summary>
		/// Siebte Generation
		/// </summary>
		public static readonly Uri USUM_96 = GetUri("USUM_96", IconSource.Core);

		/// <summary>
		/// Achte Generation
		/// </summary>
		public static readonly Uri STSD_96 = GetUri("StSd_96", IconSource.Core);

		#endregion

		#region Sonstiges

		/// <summary>
		/// Transformationen
		/// </summary>
		public static readonly Uri TRANSFORMATIONS_96 = GetUri("Transformations_96", IconSource.Core);

		/// <summary>
		/// Shiny
		/// </summary>
		public static readonly Uri SHINY_96 = GetUri("Shiny_96", IconSource.Core);

		#endregion

		#endregion

		#region FavouriteEditor

		/// <summary>
		/// Favoriten-Tabelle
		/// </summary>
		public static readonly Uri FAVOURITE_TABLE = GetUri("FavouriteTable", IconSource.FavouriteEditor);

		/// <summary>
		/// Normal-Hintergrund
		/// </summary>
		public static readonly Uri NORMAL_96 = GetUri("Normal_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Grass-Hintergrund
		/// </summary>
		public static readonly Uri GRASS_96 = GetUri("Grass_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Fire-Hintergrund
		/// </summary>
		public static readonly Uri FIRE_96 = GetUri("Fire_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Water-Hintergrund
		/// </summary>
		public static readonly Uri WATER_96 = GetUri("Water_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Flying-Hintergrund
		/// </summary>
		public static readonly Uri FLYING_96 = GetUri("Flying_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Electric-Hintergrund
		/// </summary>
		public static readonly Uri ELECTRIC_96 = GetUri("Electric_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Bug-Hintergrund
		/// </summary>
		public static readonly Uri BUG_96 = GetUri("Bug_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Poison-Hintergrund
		/// </summary>
		public static readonly Uri POISON_96 = GetUri("Poison_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Rock-Hintergrund
		/// </summary>
		public static readonly Uri ROCK_96 = GetUri("Rock_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Ground-Hintergrund
		/// </summary>
		public static readonly Uri GROUND_96 = GetUri("Ground_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Fighting-Hintergrund
		/// </summary>
		public static readonly Uri FIGHTING_96 = GetUri("Fighting_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Psychic-Hintergrund
		/// </summary>
		public static readonly Uri PSYCHIC_96 = GetUri("Psychic_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Ghost-Hintergrund
		/// </summary>
		public static readonly Uri GHOST_96 = GetUri("Ghost_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Ice-Hintergrund
		/// </summary>
		public static readonly Uri ICE_96 = GetUri("Ice_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Dragon-Hintergrund
		/// </summary>
		public static readonly Uri DRAGON_96 = GetUri("Dragon_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Dark-Hintergrund
		/// </summary>
		public static readonly Uri DARK_96 = GetUri("Dark_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Steel-Hintergrund
		/// </summary>
		public static readonly Uri STEEL_96 = GetUri("Steel_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Fairy-Hintergrund
		/// </summary>
		public static readonly Uri FAIRY_96 = GetUri("Fairy_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Legendary-Hintergrund
		/// </summary>
		public static readonly Uri LEGENDARY_96 = GetUri("Legendary_96", IconSource.FavouriteEditor);

		/// <summary>
		/// Favourite-Hintergrund
		/// </summary>
		public static readonly Uri FAVOURITE_96 = GetUri("Favourite_96", IconSource.FavouriteEditor);

		#endregion


		#endregion
	}
}
