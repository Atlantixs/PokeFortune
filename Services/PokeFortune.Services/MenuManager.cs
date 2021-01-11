using PokeFortune.Core.Collections;
using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.Services.Interfaces;
using System.Linq;

namespace PokeFortune.Services
{
	public class MenuManager : IMenuManager
	{
		public FortuneCollection<FortuneMenuItem> MenuItems { get; } = new FortuneCollection<FortuneMenuItem>();

		public void UpdateMenuItems(ModuleType moduleType)
		{
			UpdateMenuItemsRecursive(moduleType, MenuItems);
		}

		private void UpdateMenuItemsRecursive(ModuleType moduleType, FortuneCollection<FortuneMenuItem> menuItems)
		{
			//foreach(var item in menuItems)
			//{
			//	item.Visibility = item.ModuleType == ModuleType.General || item.ModuleType == moduleType;

			//	if (item.MenuItems.Any())
			//		UpdateMenuItemsRecursive(moduleType, item.MenuItems);
			//}
		}
	}
}
