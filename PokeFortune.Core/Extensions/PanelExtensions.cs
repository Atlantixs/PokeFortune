using System.Windows;
using System.Windows.Controls;

namespace PokeFortune.Core.Extensions
{
	public static class PanelExtensions
	{

		#region Spacing

		public static readonly DependencyProperty SpacingProperty =
			 DependencyProperty.RegisterAttached("ItemSpace", typeof(double),
			 typeof(PanelExtensions), new FrameworkPropertyMetadata(0.0, OnItemSpaceChanged));


		public static void SetItemSpace(Panel d, double value)
		{
			d.SetValue(SpacingProperty, value);
		}
		public static double GetItemSpace(Panel d)
		{
			return (double)d.GetValue(SpacingProperty);
		}

		private static void OnItemSpaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
		{
			var panel = d as Panel;

			panel.SizeChanged -= OnItemSpaceUpdated;
			panel.SizeChanged += OnItemSpaceUpdated;

			OnItemSpaceUpdated(panel, null);
		}

		static void OnItemSpaceUpdated(object sender, SizeChangedEventArgs e)
		{
			var p = sender as Panel;
			var s = GetItemSpace(p);

			if (p is StackPanel sp)
			{
				for (int i = 0, Count = sp.Children.Count; i < Count; i++)
				{
					var element = sp.Children[i] as FrameworkElement;

					if (i == 0 && Count > 1)
					{
						//erstes Element
						if (sp.Orientation == Orientation.Horizontal)
							element.Margin = new Thickness(0, element.Margin.Top, s / 2.0, element.Margin.Bottom);
						else
							element.Margin = new Thickness(element.Margin.Left, 0, element.Margin.Right, s / 2.0);
					}
					else if (i == (Count - 1) && Count > 1)
					{
						//letztes Element
						if (sp.Orientation == Orientation.Horizontal)
							element.Margin = new Thickness(s / 2.0, element.Margin.Top, 0, element.Margin.Bottom);
						else
							element.Margin = new Thickness(element.Margin.Left, s / 2.0, element.Margin.Right, 0);
					}
					else if (Count > 1)
					{
						if (sp.Orientation == Orientation.Horizontal)
							element.Margin = new Thickness(s / 2.0, element.Margin.Top, s / 2.0, element.Margin.Bottom);
						else
							element.Margin = new Thickness(element.Margin.Left, s / 2.0, element.Margin.Right, s / 2.0);
					}
				}
			}
			else
			{
				for (int i = 0, Count = p.Children.Count; i < Count; i++)
				{
					var element = p.Children[i] as FrameworkElement;

					element.Margin = new Thickness(s / 2d);
				}
			}
		}

		#endregion
	}
}
