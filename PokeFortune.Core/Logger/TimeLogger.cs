using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeFortune.Core.Logger
{
	public class TimeLogger
	{
		private readonly Stopwatch _stopwatch = new Stopwatch();
		private TimeSpan _fullTime = new TimeSpan();

		private readonly StringBuilder _sb = new StringBuilder();

		public static TimeLogger StartNew()
		{
			var time = new TimeLogger();

			time.Start();

			return time;
		}


		public void Start()
		{
			_stopwatch.Start();
			_fullTime = new TimeSpan();
		}

		public void Time(string action)
		{
			_stopwatch.Stop();
			_sb.Append($"TIME '{action}': {_stopwatch.Elapsed} sec\n");
			_fullTime += _stopwatch.Elapsed;
			_stopwatch.Restart();
		}

		public void Stop()
		{
			_stopwatch.Stop();
			_fullTime += _stopwatch.Elapsed;
			_sb.Append($"************ 'Gesamt': {_fullTime} sec");
			Debug.WriteLine(_sb.ToString());
			_stopwatch.Reset();
		}

	}
}
