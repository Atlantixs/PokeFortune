using PokeFortune.Core.Mvvm;
using PokeFortune.FavouriteEditor.Managers;
using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;

namespace PokeFortune.FavouriteEditor.GUI
{
	public class EditorViewModel : RegionViewModelBase
	{
		public ITemplateManager TemplateManager { get; }

		public EditorViewModel(IRegionManager regionManager, ITemplateManager templateManager) : base(regionManager)
		{
			TemplateManager = templateManager;

			NextRowCmd = new DelegateCommand(TemplateManager.NextRow);
			PrevRowCmd = new DelegateCommand(TemplateManager.PreviousRow);
			NextColumnCmd = new DelegateCommand(TemplateManager.NextColumn);
			PrevColumnCmd = new DelegateCommand(TemplateManager.PreviousColumn);

			SaveImageCmd = new DelegateCommand(TemplateManager.SaveTable);
			ResetImageCmd = new DelegateCommand(TemplateManager.ResetTable);
		}


		public ICommand NextRowCmd { get; init; }
		public ICommand PrevRowCmd { get; init; }
		public ICommand NextColumnCmd { get; init; }
		public ICommand PrevColumnCmd { get; init; }

		public ICommand SaveImageCmd { get; init; }
		public ICommand ResetImageCmd { get; init; }
	}
}