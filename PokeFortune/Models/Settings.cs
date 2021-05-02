using PokeFortune.Core.Consts;
using PokeFortune.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokeFortune.Models
{
	[Serializable]
	public class Settings
	{
		private static string SETTINGS_FILE => PathHelper.ROAMING_PATH + "settings.xml";


		[XmlAttribute]
		public string Culture { get; set; } = "en-EN";

		public void SaveSettings()
		{
			CheckDirectories();

			File.WriteAllText(SETTINGS_FILE, XmlHelper.Serialize(this));
		}

		public static void CheckDirectories()
		{
			PathHelper.CheckDirectory(PathHelper.ROAMING_PATH);

			if (!File.Exists(SETTINGS_FILE))
				File.WriteAllText(SETTINGS_FILE, XmlHelper.Serialize(new Settings()));
		}

		public static Settings GetCurrentSettings()
		{
			CheckDirectories();

			return XmlHelper.Deserialize<Settings>(File.ReadAllText(SETTINGS_FILE));
		}
	}
}
