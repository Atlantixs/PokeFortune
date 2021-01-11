using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PokeFortune.Core.Helpers
{
	/// <summary>
	/// Hilfsklasse für threadübergreifende UI-Zugriffe
	/// </summary>
	public static class UIHelper
	{
		/// <summary>
		/// Führt die gegebene Action falls benötigt über <see cref="Dispatcher.Invoke(Action)"/> auf dem UI Thread (falls existent) aus.
		/// </summary>
		/// <param name="a">Die auszuführende Action</param>
		public static void InvokeSafe(Action a)
		{
			InvokeSafe(a, GetUIDispatcher());
		}

		/// <summary>
		/// Führt die gegebene Action falls benötigt über <see cref="Dispatcher.Invoke(Action)"/> auf dem UI Thread (falls existent) aus.
		/// </summary>
		/// <param name="a">Die auszuführende Action</param>
		/// <param name="disp">Der Dispatcher auf dem die Aktion durchgeführt werden soll</param>
		public static void InvokeSafe(Action a, Dispatcher disp)
		{
			if (a == null || disp?.HasShutdownStarted == true || disp?.HasShutdownFinished == true)
				return;

			if (disp == null || disp.CheckAccess())
				a();
			else
				disp.Invoke(a);
		}

		/// <summary>
		/// Führt die gegebene Action falls benötigt über <see cref="Dispatcher.Invoke(Action)"/> auf dem UI Thread (falls existent) aus.
		/// </summary>
		/// <param name="a">Die auszuführende Action</param>
		public static async Task InvokeSafeAsync(Action a)
		{
			await InvokeSafeAsync(a, GetUIDispatcher());
		}

		/// <summary>
		/// Führt die gegebene Action falls benötigt über <see cref="Dispatcher.BeginInvoke()"/> auf dem UI Thread (falls existent) aus.
		/// </summary>
		/// <param name="a">Die auszuführende Action</param>
		/// <param name="disp">Der Dispatcher auf dem die Aktion durchgeführt werden soll</param>
		public static async Task InvokeSafeAsync(Action a, Dispatcher disp)
		{
			if (a == null || disp?.HasShutdownStarted == true || disp?.HasShutdownFinished == true)
				return;

			if (disp == null || disp.CheckAccess())
				a();
			else
				await disp.BeginInvoke(a);
		}

		/// <summary>
		/// Liefert den Dispatcher
		/// </summary>
		public static Dispatcher GetUIDispatcher()
		{
			return Application.Current?.Dispatcher;
		}
	}
}