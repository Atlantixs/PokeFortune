using PokeFortune.Core.Collections;
using PokeFortune.Core.Enums;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace PokeFortune.Core.Models
{
	public class FortuneMenuItem : BindableBase
	{
		public FortuneMenuItem(ModuleType moduleType, bool isSeperator = false)
		{
			_moduleType = moduleType;
			IsSeparator = isSeperator;
		}

		#region properties

		private readonly ModuleType _moduleType;
		public ModuleType ModuleType => _moduleType;

		private bool _isCheckable = false;
		public bool IsCheckable
		{
			get => _isCheckable;
			set => SetProperty(ref _isCheckable, value);
		}

		private bool _isChecked = false;
		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				if (SetProperty(ref _isChecked, value))
					UpdateChecked?.Invoke(value);
			}
		}

		public Action<bool> UpdateChecked { get; set; }

		private Uri _icon = null;
		public Uri Icon
		{
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		private string _header = string.Empty;
		public string Header
		{
			get => _header;
			set => SetProperty(ref _header, value);
		}

		private ICommand _command = null;
		public ICommand Command
		{
			get => _command;
			set => SetProperty(ref _command, value);
		}

		private object _commandParameter = null;
		public object CommandParameter
		{
			get => _commandParameter;
			set => SetProperty(ref _commandParameter, value);
		}

		private bool _isVisible = true;
		public bool IsVisible
		{
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		private bool _isSeparator = false;
		public bool IsSeparator
		{
			get => _isSeparator;
			private set => SetProperty(ref _isSeparator, value);
		}

		public FortuneCollection<FortuneMenuItem> MenuItems { get; } = new FortuneCollection<FortuneMenuItem>();

		#endregion
	}
}
