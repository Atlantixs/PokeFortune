using PokeFortune.Services.Interfaces;
using Prism.Mvvm;

namespace PokeFortune.GUI
{
	public class ContentViewModel : BindableBase
	{
		public ContentViewModel(IMenuManager menuManager)
		{
			MenuManager = menuManager;
		}

		public IMenuManager MenuManager { get; }
	}
}