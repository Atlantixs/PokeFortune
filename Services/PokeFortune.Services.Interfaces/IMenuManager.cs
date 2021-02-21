using PokeFortune.Core.Collections;
using PokeFortune.Core.Enums;
using PokeFortune.Core.Models;

namespace PokeFortune.Services.Interfaces
{
	public interface IMenuManager
	{
		public SortableCollection<FortuneMenuItem> MenuItems { get; }

		public void UpdateMenuItems(ModuleType moduleType);
	}
}
