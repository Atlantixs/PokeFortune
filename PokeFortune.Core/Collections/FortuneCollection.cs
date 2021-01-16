using PokeFortune.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace PokeFortune.Core.Collections
{
	public class FortuneCollection<T> : ObservableCollection<T>
	{
		public FortuneCollection() : base()
		{ }

		public FortuneCollection(List<T> list) : base(list)
		{ }

		public FortuneCollection(IEnumerable<T> collection) : base(collection)
		{ }

		protected bool _enableNotification = true;
		public bool EnableNotification
		{
			get => _enableNotification;
			set
			{
				if (_enableNotification != value)
				{
					_enableNotification = value;
					if (value)
						OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
				}
			}
		}

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (EnableNotification)
				UIHelper.InvokeSafe(() => base.OnCollectionChanged(e));
		}

		public void AddRange(IEnumerable<T> list)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));

			_enableNotification = false;

			foreach (T item in list)
				Add(item);

			_enableNotification = true;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public void RemoveRange(IEnumerable<T> list)
		{
			if (list == null)
				throw new ArgumentNullException(nameof(list));

			_enableNotification = false;

			foreach (var item in list.Reverse())
				Remove(item);

			_enableNotification = true;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		/// <summary>
		/// Entfernt alle Elemente aus <see cref="toRemove"/> und fügt die in <see cref="toAdd"/> hinzu
		/// </summary>
		/// <param name="toRemove"></param>
		/// <param name="toAdd"></param>
		public void ReplaceRange(IEnumerable<T> toRemove, IEnumerable<T> toAdd)
		{
			_enableNotification = false;
			var added = false;
			var removed = false;

			if (toRemove != null)
			{
				var toRemoveTmp = new List<T>(toRemove);
				foreach (T item in toRemoveTmp)
					removed |= Remove(item);
			}

			if (toAdd != null)
				foreach (T item in toAdd)
				{
					Add(item);
					added = true;
				}

			_enableNotification = true;
			if (added || removed)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		/// <summary>
		/// Entfernt alle Items in der Liste und fügt die in <see cref="toAdd"/> hinzu. Sollten Elemente in der Liste bereits existieren werden diese weder entfernt, noch hinzugefügt.
		/// </summary>
		/// <param name="toAdd"></param>
		/// <param name="comparer"></param>
		public void ReplaceAllWith(IEnumerable<T> toAdd, IEqualityComparer<T> comparer = null)
		{
			var toAddTmp = toAdd?.ToList() ?? new List<T>();
			//Except zur Performance Optimierung. Sollten die Elemente bereits in der Liste sein werden sie ignoriert und es kommt ggf. zu keiner Modifikation der Liste
			ReplaceRange(this.Except(toAddTmp, comparer), toAddTmp.Except(this, comparer));
		}
	}
}