using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.Core.Properties;
using PokeFortune.GUI.Dialogs;
using PokeFortune.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace PokeFortune.GUI
{
	public class ContentViewModel : BindableBase
	{
		public ContentViewModel(IDialogService dialogService, IMenuManager menuManager)
		{
			MenuManager = menuManager;

			var help = new FortuneMenuItem(ModuleType.General) 
			{
				Header = Resources.Help
			};

			help.MenuItems.Add(new FortuneMenuItem(ModuleType.General) 
			{
				Header = Resources.AboutPokeFortune,
				Command = new DelegateCommand(() => dialogService.ShowDialog(nameof(AboutDialogView)))
			});

			MenuManager.MenuItems.Add(help);
		}

		public IMenuManager MenuManager { get; }
	}
}