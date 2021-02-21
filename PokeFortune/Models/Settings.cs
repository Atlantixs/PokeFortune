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
		private static string SETTINGS_PATH => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/PokeFortune/";
		private static string SETTINGS_FILE => "settings.xml";


		[XmlAttribute]
		public string Culture { get; set; } = "en-EN";

		public void SaveSettings()
		{
			CheckDirectories();

			File.WriteAllText(SETTINGS_PATH + SETTINGS_FILE, XmlHelper.Serialize(this));
		}

		public static void CheckDirectories()
		{
			if (!Directory.Exists(SETTINGS_PATH))
				Directory.CreateDirectory(SETTINGS_PATH);

			if (!File.Exists(SETTINGS_PATH + SETTINGS_FILE))
				File.WriteAllText(SETTINGS_PATH + SETTINGS_FILE, XmlHelper.Serialize(new Settings()));
		}

		public static Settings GetCurrentSettings()
		{
			CheckDirectories();

			return XmlHelper.Deserialize<Settings>(File.ReadAllText(SETTINGS_PATH + SETTINGS_FILE));
		}
	}
}
