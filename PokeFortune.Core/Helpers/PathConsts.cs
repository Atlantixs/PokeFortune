using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeFortune.Core.Consts
{
	public static class PathHelper
	{
		public static string ROAMING_PATH { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/PokeFortune/";
		public static string LOCAL_PATH { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/PokeFortune/";


		public static bool CheckDirectory(string path, bool createIfNotExist = true)
		{
			var res = Directory.Exists(path);

			if (createIfNotExist && !res)
				Directory.CreateDirectory(path);

			return res;
		}
	}
}
