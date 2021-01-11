using PokeFortune.Core;
using Prism.Mvvm;
using Prism.Regions;

namespace PokeFortune.GUI
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "PokeFortune";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainWindowViewModel(IRegionManager regionManager)
		{
			regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentView));
		}
	}
}