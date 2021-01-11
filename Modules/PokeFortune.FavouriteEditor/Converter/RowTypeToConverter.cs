using PokeFortune.Core;
using PokeFortune.Core.Properties;
using PokeFortune.FavouriteEditor.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PokeFortune.FavouriteEditor.Converter
{
	public class RowTypeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is RowType colType)
			{
				return colType switch
				{
					RowType.FirstGen => Resources.FirstGen,
					RowType.SecondGen => Resources.SecondGen,
					RowType.ThirdGen => Resources.ThirdGen,
					RowType.FourthGen => Resources.FourthGen,
					RowType.FifthGen => Resources.FifthGen,
					RowType.SixthGen => Resources.SixthGen,
					RowType.SeventhGen => Resources.SeventhGen,
					RowType.EighthGen => Resources.EighthGen,
					RowType.Transformation => Resources.Transformation,
					RowType.Favourite => Resources.Favourite,
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

	public class RowTypeToUriConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is RowType colType)
			{
				return colType switch
				{
					RowType.FirstGen => CoreIconPool.RBY_96,
					RowType.SecondGen => CoreIconPool.GSC_96,
					RowType.ThirdGen => CoreIconPool.RSE_96,
					RowType.FourthGen => CoreIconPool.DPP_96,
					RowType.FifthGen => CoreIconPool.BWB2W2_96,
					RowType.SixthGen => CoreIconPool.XY_96,
					RowType.SeventhGen => CoreIconPool.USUM_96,
					RowType.EighthGen => CoreIconPool.STSD_96,
					RowType.Transformation => CoreIconPool.TRANSFORMATIONS_96,
					RowType.Favourite => CoreIconPool.SHINY_96,
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
