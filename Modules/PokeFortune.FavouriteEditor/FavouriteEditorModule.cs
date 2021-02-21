using PokeFortune.Core;
using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.Core.Properties;
using PokeFortune.FavouriteEditor.GUI;
using PokeFortune.FavouriteEditor.Managers;
using PokeFortune.Services.Interfaces;
using Prism.Commands;
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
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
			_regionManager.RequestNavigate(RegionNames.ModuleRegion, nameof(EditorView));

			InitMenuItems(containerProvider.Resolve<ITemplateManager>());
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<ITemplateManager, TemplateManager>();
			containerRegistry.RegisterForNavigation<EditorView>();
		}

		private void InitMenuItems(ITemplateManager templateManager)
		{
			var file = new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = Resources.File
			};

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = Resources.New,
				Command = new DelegateCommand(templateManager.ResetTable)
			});

			//file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			//{
			//	Header = "Öffnen..."
			//});

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor, true));

			file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = Resources.Save,
				Command = new DelegateCommand(templateManager.SaveTable)
			});

			//file.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			//{
			//	Header = "Speichern unter..."
			//});

			var options = new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = Resources.Options
			};

			options.MenuItems.Add(new FortuneMenuItem(ModuleType.FavouriteEditor)
			{
				Header = Resources.ShinyMode,
				IsCheckable = true,
				IsChecked = templateManager.ShinyMode,
				UpdateChecked = (bool isChecked) => templateManager.ShinyMode = isChecked
			});

			_menuManager.MenuItems.Add(file);
			_menuManager.MenuItems.Add(options);

			_menuManager.UpdateMenuItems(ModuleType.FavouriteEditor);
		}
	}
}