using System;

namespace PokeFortune.Data
{
	public static class PokemonIconPool
	{
		#region enums

		/// <summary>
		/// Quelle der Icons
		/// </summary>
		private enum IconSource
		{
			/// <summary>
			/// Pokemon
			/// </summary>
			Pokemon
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

		private static readonly string POKEMON_FORMAT = "pack://application:,,,/PokeFortune.Data;component/Resources/{0}.{1}";

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
		private static string GetSource(IconSource source)
		{
			switch (source)
			{
				case IconSource.Pokemon:
					return POKEMON_FORMAT;
			}
			throw new InvalidOperationException("Ungültiger Aufruf!");
		}

		/// <summary>
		/// Baut aus verschiedenen definierten Informationen eine Uri auf
		/// </summary>
		/// <param name="imageName">Name des Bildes (ohne Extension)</param>
		/// <param name="iconSource">Quelle des Bildes</param>
		/// <param name="iconExtension">Extension des Bildes</param>
		/// <returns></returns>
		private static Uri GetUri(string imageName, IconExtension iconExtension = IconExtension.png, IconSource iconSource = IconSource.Pokemon)
		{
			string format = GetSource(iconSource);
			return new Uri(string.Format(format, imageName, GetExt(iconExtension)), UriKind.RelativeOrAbsolute);
		}

		#endregion method

		public static Uri GetPokemonSprite(int id, string additionalUri, bool shiny = false)
		{
			var directory = shiny ? "Shiny" : "Pokemon";

			if (string.IsNullOrEmpty(additionalUri))
				return GetUri($"{directory}/{id.ToString("D3")}");
			return GetUri($"{directory}/{id.ToString("D3")}-{additionalUri}");
		}
	}
}