using PokeFortune.Core.Properties;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Diagnostics;
using System.Reflection;

namespace PokeFortune.GUI.Dialogs
{
	public class AboutDialogViewModel : BindableBase, IDialogAware
	{
		public AboutDialogViewModel()
		{
			var assambly = Assembly.GetExecutingAssembly();
			var fvi = FileVersionInfo.GetVersionInfo(assambly.Location);

			AboutPokeFortune = $"PokeFortune by Atlantixs\nVersion {fvi?.ProductVersion}";

			CopyrightPokemon = $"Pokémon © 1995-{DateTime.Now.Year} Nintendo, GAME FREAK and Creatures, Inc.";
		}

		public string Title => Resources.AboutPokeFortune;

		public string AboutPokeFortune { get; }

		public string CopyrightPokemon { get; }

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
			//
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			//
		}
	}
}
