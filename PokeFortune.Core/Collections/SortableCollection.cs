using System;
using System.Collections.Specialized;

namespace PokeFortune.Core.Collections
{
	public class SortableCollection<T> : FortuneCollection<T>
	{
		public bool AutomaticSort { get; set; }

		public Comparison<T> Comparison { private get; set; }

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (EnableNotification)
			{
				if (AutomaticSort && Comparison != null)
					Sort(Comparison);
				else
					base.OnCollectionChanged(e);
			}
		}

		public void Sort()
		{
			if (Comparison != null)
				Sort(Comparison);
		}

		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
				return;

			_enableNotification = false;

			for (int i = 1; i < Items.Count; i++)
			{
				int j = i;
				while ((j > 0) && comparison(Items[j - 1], Items[i]) > 0)
				{
					j--;
				}

				if (j < i)
					Move(i, j);
			}

			_enableNotification = true;

			base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}
