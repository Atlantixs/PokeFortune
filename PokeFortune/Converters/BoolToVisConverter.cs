using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PokeFortune.Converters
{
	public class BoolToVisConverter : IValueConverter
	{
		public bool Collapsed { get; set; } = true;
		public bool Reverse { get; set; } = false;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool isVis)
			{
				if (Reverse)
					isVis = !isVis;

				if (isVis)
					return Visibility.Visible;
				else
				{
					if (Collapsed)
						return Visibility.Collapsed;

					return Visibility.Hidden;
				}
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
