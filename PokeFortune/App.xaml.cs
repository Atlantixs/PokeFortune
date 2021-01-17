using PokeFortune.Core;
using PokeFortune.Data;
using PokeFortune.FavouriteEditor;
using PokeFortune.GUI;
using PokeFortune.GUI.Dialogs;
using PokeFortune.Services;
using PokeFortune.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Windows;

namespace PokeFortune
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IMenuManager, MenuManager>();

			containerRegistry.RegisterDialog<AboutDialogView, AboutDialogViewModel>();
		}

		//protected override void InitializeModules()
		//{
		//	var splashScreen = new SplashScreen(CoreIconPool.POKE_FORTUNE.AbsoluteUri);
		//	splashScreen.Show(false);
		//	try
		//	{
		//		base.InitializeModules();
		//	}
		//	finally
		//	{
		//		splashScreen.Close(TimeSpan.Zero);
		//	}
		//}

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			PokemonList.LoadPokemons();
			moduleCatalog.AddModule<FavouriteEditorModule>();
		}

		protected override void ConfigureViewModelLocator()
		{
			base.ConfigureViewModelLocator();

			ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();

			ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
			{
				var viewModelName = viewType.FullName + "Model";
				var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
				return Type.GetType($"{viewModelName}, {viewAssemblyName}");
			});
		}
	}
}