using PokeFortune.Data.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PokeFortune.Converters
{
	public class PokemonTypeToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var brush = Brushes.Transparent;

			if (value is PokemonType type)
			{
				brush = type switch
				{
					PokemonType.Normal => new SolidColorBrush(Color.FromRgb(146, 155, 163)),
					PokemonType.Grass => new SolidColorBrush(Color.FromRgb(99, 189, 90)),
					PokemonType.Fire => new SolidColorBrush(Color.FromRgb(255, 158, 84)),
					PokemonType.Water => new SolidColorBrush(Color.FromRgb(79, 145, 215)),
					PokemonType.Flying => new SolidColorBrush(Color.FromRgb(144, 170, 223)),
					PokemonType.Electric => new SolidColorBrush(Color.FromRgb(244, 211, 57)),
					PokemonType.Bug => new SolidColorBrush(Color.FromRgb(146, 197, 43)),
					PokemonType.Poison => new SolidColorBrush(Color.FromRgb(171, 107, 201)),
					PokemonType.Rock => new SolidColorBrush(Color.FromRgb(198, 184, 141)),
					PokemonType.Ground => new SolidColorBrush(Color.FromRgb(218, 121, 67)),
					PokemonType.Fighting => new SolidColorBrush(Color.FromRgb(207, 63, 107)),
					PokemonType.Psychic => new SolidColorBrush(Color.FromRgb(250, 114, 122)),
					PokemonType.Ghost => new SolidColorBrush(Color.FromRgb(81, 105, 175)),
					PokemonType.Ice => new SolidColorBrush(Color.FromRgb(116, 207, 193)),
					PokemonType.Dragon => new SolidColorBrush(Color.FromRgb(3, 109, 196)),
					PokemonType.Dark => new SolidColorBrush(Color.FromRgb(90, 83, 101)),
					PokemonType.Steel => new SolidColorBrush(Color.FromRgb(90, 143, 163)),
					PokemonType.Fairy => new SolidColorBrush(Color.FromRgb(237, 144, 231)),
					_ => Brushes.Transparent
				};
			}

			return brush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class PokemonTypeToBackgroundColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var brush = Brushes.Transparent;

			if (value is PokemonType type)
			{
				brush = type switch
				{
					PokemonType.Normal => new SolidColorBrush(Color.FromRgb(146, 155, 163)),
					PokemonType.Grass => new SolidColorBrush(Color.FromRgb(99, 189, 90)),
					PokemonType.Fire => new SolidColorBrush(Color.FromRgb(255, 158, 84)),
					PokemonType.Water => new SolidColorBrush(Color.FromRgb(79, 145, 215)),
					PokemonType.Flying => new SolidColorBrush(Color.FromRgb(144, 170, 223)),
					PokemonType.Electric => new SolidColorBrush(Color.FromRgb(244, 211, 57)),
					PokemonType.Bug => new SolidColorBrush(Color.FromRgb(146, 194, 43)),
					PokemonType.Poison => new SolidColorBrush(Color.FromRgb(171, 107, 201)),
					PokemonType.Rock => new SolidColorBrush(Color.FromRgb(198, 184, 141)),
					PokemonType.Ground => new SolidColorBrush(Color.FromRgb(218, 121, 67)),
					PokemonType.Fighting => new SolidColorBrush(Color.FromRgb(207, 63, 107)),
					PokemonType.Psychic => new SolidColorBrush(Color.FromRgb(250, 114, 122)),
					PokemonType.Ghost => new SolidColorBrush(Color.FromRgb(81, 105, 175)),
					PokemonType.Ice => new SolidColorBrush(Color.FromRgb(116, 207, 193)),
					PokemonType.Dragon => new SolidColorBrush(Color.FromRgb(3, 109, 196)),
					PokemonType.Dark => new SolidColorBrush(Color.FromRgb(90, 83, 101)),
					PokemonType.Steel => new SolidColorBrush(Color.FromRgb(90, 143, 163)),
					PokemonType.Fairy => new SolidColorBrush(Color.FromRgb(237, 144, 231)),
					_ => Brushes.Transparent
				};
			}

			return brush;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
