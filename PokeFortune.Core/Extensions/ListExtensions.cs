using PokeFortune.Core.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PokeFortune.Core.Extensions
{
	public static class ListExtensions
	{
		public static T Next<T>(this IList<T> list, T currentItem)
		{
			if (list != null)
			{
				var nextIndex = MathHelper.Modulo(list.IndexOf(currentItem) + 1, list.Count);
				return list.ElementAtOrDefault(nextIndex);
			}
			return default;
		}

		public static T Previous<T>(this IList<T> list, T currentItem)
		{
			if (list != null)
			{
				var prevIndex = MathHelper.Modulo(list.IndexOf(currentItem) - 1, list.Count);
				return list.ElementAtOrDefault(prevIndex);
			}
			return default;
		}
	}
}
