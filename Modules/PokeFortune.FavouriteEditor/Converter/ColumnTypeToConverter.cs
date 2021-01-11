using PokeFortune.Core;
using PokeFortune.Core.Properties;
using PokeFortune.FavouriteEditor.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PokeFortune.FavouriteEditor.Converter
{
	public class ColumnTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ColumnType colType)
			{
				return colType switch
				{
					ColumnType.Normal => Resources.Type_Normal,
					ColumnType.Grass => Resources.Type_Grass,
					ColumnType.Fire => Resources.Type_Fire,
					ColumnType.Water => Resources.Type_Water,
					ColumnType.Flying => Resources.Type_Flying,
					ColumnType.Electric => Resources.Type_Electric,
					ColumnType.Bug => Resources.Type_Bug,
					ColumnType.Poison => Resources.Type_Poison,
					ColumnType.Rock => Resources.Type_Rock,
					ColumnType.Ground => Resources.Type_Ground,
					ColumnType.Fighting => Resources.Type_Fighting,
					ColumnType.Psychic => Resources.Type_Psychic,
					ColumnType.Ghost => Resources.Type_Ghost,
					ColumnType.Ice => Resources.Type_Ice,
					ColumnType.Dragon => Resources.Type_Dragon,
					ColumnType.Dark => Resources.Type_Dark,
					ColumnType.Steel => Resources.Type_Steel,
					ColumnType.Fairy => Resources.Type_Fairy,
					ColumnType.Legendary => Resources.Legendary,
					ColumnType.Favourite => Resources.Favourite,
					_ => string.Empty,
				};
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class ColumnTypeToUriConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ColumnType colType)
			{
				return colType switch
				{
					ColumnType.Normal => CoreIconPool.NORMAL_ICON,
					ColumnType.Grass => CoreIconPool.GRASS_ICON,
					ColumnType.Fire => CoreIconPool.FIRE_ICON,
					ColumnType.Water => CoreIconPool.WATER_ICON,
					ColumnType.Flying => CoreIconPool.FLYING_ICON,
					ColumnType.Electric => CoreIconPool.ELECTRIC_ICON,
					ColumnType.Bug => CoreIconPool.BUG_ICON,
					ColumnType.Poison => CoreIconPool.POISON_ICON,
					ColumnType.Rock => CoreIconPool.ROCK_ICON,
					ColumnType.Ground => CoreIconPool.GROUND_ICON,
					ColumnType.Fighting => CoreIconPool.FIGHTING_ICON,
					ColumnType.Psychic => CoreIconPool.PSYCHIC_ICON,
					ColumnType.Ghost => CoreIconPool.GHOST_ICON,
					ColumnType.Ice => CoreIconPool.ICE_ICON,
					ColumnType.Dragon => CoreIconPool.DRAGON_ICON,
					ColumnType.Dark => CoreIconPool.DARK_ICON,
					ColumnType.Steel => CoreIconPool.STEEL_ICON,
					ColumnType.Fairy => CoreIconPool.FAIRY_ICON,
					ColumnType.Legendary => CoreIconPool.LEGENDARY_ICON,
					ColumnType.Favourite => CoreIconPool.SHINY_ICON,
					_ => DependencyProperty.UnsetValue
				};
			}

			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
