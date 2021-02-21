using PokeFortune.Core.Collections;
using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;
using PokeFortune.Services.Interfaces;
using System.Linq;

namespace PokeFortune.Services
{
	public class MenuManager : IMenuManager
	{
		public MenuManager()
		{
			MenuItems = new SortableCollection<FortuneMenuItem>();
			MenuItems.Comparison = new System.Comparison<FortuneMenuItem>((x, y) =>
			{
				if (x.ModuleType > y.ModuleType)
					return -1;
				else if (x.ModuleType < y.ModuleType)
					return 1;
				else
					return 0;
			});

			MenuItems.AutomaticSort = true;
		}

		public SortableCollection<FortuneMenuItem> MenuItems { get; }

		public void UpdateMenuItems(ModuleType moduleType)
		{
			UpdateMenuItemsRecursive(moduleType, MenuItems);
		}

		private void UpdateMenuItemsRecursive(ModuleType moduleType, SortableCollection<FortuneMenuItem> menuItems)
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
