using System.Windows.Controls;

namespace PokeFortune.FavouriteEditor.GUI
{
	/// <summary>
	/// Interaction logic for EditorView.xaml
	/// </summary>
	public partial class EditorView : UserControl
	{
		public EditorView()
		{
			InitializeComponent();
		}

		private void listbox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (listbox.IsMouseCaptured)
				listbox.ReleaseMouseCapture();
		}

		private void listbox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (listbox.IsMouseCaptured)
				listbox.ReleaseMouseCapture();
		}
	}
}