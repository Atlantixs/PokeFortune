using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.Core.Properties;
using PokeFortune.GUI.Dialogs;
using PokeFortune.Models;
using PokeFortune.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace PokeFortune.GUI
{
	public class ContentViewModel : BindableBase
	{
		public ContentViewModel(IDialogService dialogService, IMenuManager menuManager)
		{
			MenuManager = menuManager;

			var sett = Settings.GetCurrentSettings();
			var cultures = new List<(string name, string culture)>()
			{
				("Deutsch", "de-DE"),
				("English", "en-EN")
			};

			var lang = new FortuneMenuItem(ModuleType.General)
			{
				Header = Resources.Language
			};

			foreach (var (name, culture) in cultures)
				lang.MenuItems.Add(new FortuneMenuItem(ModuleType.General)
				{
					Header = name,
					IsCheckable = true,
					IsChecked = sett.Culture == culture,
					Command = new DelegateCommand<string>(ChangeLanguage),
					CommandParameter = culture
				});

			MenuManager.MenuItems.Add(lang);

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

		public void ChangeLanguage(string culture)
		{
			var sett = Settings.GetCurrentSettings();

			sett.Culture = culture;
			sett.SaveSettings();

			//Process.Start(Application.ResourceAssembly.Location);
			Application.Current.Shutdown();
		}
	}
}