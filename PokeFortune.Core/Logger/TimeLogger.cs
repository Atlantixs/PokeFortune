using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace PokeFortune.Core.Logger
{
	public class TimeLogger
	{
		public static TimeLogger StartNewTimer([CallerMemberName] string actionName = "")
		{
			var timeLogger = new TimeLogger();
			timeLogger.ResetTimer();
			timeLogger.StartTimer(actionName);
			return timeLogger;
		}

		public TimeLogger()
		{
			_stopWatch = new Stopwatch();
			_sb = new StringBuilder();
		}

		private readonly string _seperator = "********************";

		private readonly Stopwatch _stopWatch;
		private readonly StringBuilder _sb;
		private TimeSpan _totalTime;

		private bool _timerStarted = false;

		/// <summary>
		/// Startet den Timer.
		/// </summary>
		/// <param name="actionName"></param>
		public void StartTimer([CallerMemberName] string actionName = "")
		{
#if DEBUG
			if (!_timerStarted)
			{
				_sb.Append($"{_seperator} {actionName}\n");

				_timerStarted = true;
				_stopWatch.Start();
			}
#endif
		}

		/// <summary>
		/// Stopt den Timer und resettet ihn.
		/// </summary>
		public void ResetTimer()
		{
#if DEBUG
			StopTimer();

			_stopWatch.Reset();
			_sb.Clear();
			_totalTime = new TimeSpan();
#endif
		}

		/// <summary>
		/// Stopt den Timer, resettet ihn und startet neu.
		/// </summary>
		/// <param name="actionName"></param>
		public void RestartTimer([CallerMemberName] string actionName = "")
		{
#if DEBUG
			ResetTimer();

			StartTimer(actionName);
#endif
		}

		/// <summary>
		/// Stop den Timer und loggt die Zeit.
		/// </summary>
		public void StopTimer()
		{
#if DEBUG
			if (_timerStarted)
			{
				_stopWatch.Stop();
				_totalTime += _stopWatch.Elapsed;
				_timerStarted = false;

				_sb.Append($"{_seperator} Gesamt-Zeit: {_totalTime.TotalSeconds} sec");
				Debug.Print(_sb.ToString());
			}
#endif
		}

		/// <summary>
		/// Setzt eine Zwischenzeit (Zeit schreitet weiter voran)
		/// </summary>
		/// <param name="roundName">max. 40 Zeichen</param>
		public void SetRound(string roundName)
		{
#if DEBUG
			if (_timerStarted)
			{
				_sb.Append($"Round '{string.Format("{0,40}", roundName)}': {_stopWatch.Elapsed} sec\n");
			}
#endif
		}

		/// <summary>
		/// Setzt eine Zwischenzeit (Zeit wird neu gestartet)
		/// </summary>
		/// <param name="timeName">max. 40 Zeichen</param>
		public void SetTime(string timeName)
		{
#if DEBUG
			if (_timerStarted)
			{
				_stopWatch.Stop();
				_totalTime += _stopWatch.Elapsed;
				_sb.Append($"TIME '{string.Format("{0,40}", timeName)}': {_stopWatch.Elapsed} sec\n");
				_stopWatch.Restart();
			}
#endif
		}
	}
}
