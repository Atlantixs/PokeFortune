using PokeFortune.Core;
using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.FavouriteEditor.GUI;
using PokeFortune.Services.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace PokeFortune.FavouriteEditor
{
	public class FavouriteEditorModule : IModule
	{
		private readonly IRegionManager _regionManager;
		private readonly IMenuManager _menuManager;

		public FavouriteEditorModule(IRegionManager regionManager, IMenuManager menuManager)
		{
			_regionManager = regionManager;
			_menuManager = menuManager;

			InitMenuItems();
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
			_regionManager.RequestNavigate(RegionNames.ModuleRegion, nameof(EditorView));
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<EditorView>();
		}

		private void InitMenuItems()
		{
			var file = new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = "Datei"
			};

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = "Neu"
			});

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = "Öffnen..."
			});

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor, true));

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = "Speichern"
			});

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = "Speichern unter..."
			});

			_menuManager.MenuItems.Add(file);
		}
	}
}