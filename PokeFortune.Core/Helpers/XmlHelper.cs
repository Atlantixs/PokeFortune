using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PokeFortune.Core.Helpers
{
	public static class XmlHelper
	{
		public static string Serialize<T>(T value)
		{
			var xml = string.Empty;

			if (value != null)
			{
				using (var sw = new StringWriter())
				{
					var ser = new XmlSerializer(typeof(T));

					ser.Serialize(sw, value);

					xml = sw.ToString();
				}
			}

			return xml;
		}

		public static T Deserialize<T>(string xml)
		{
			T value = default;

			if (!string.IsNullOrEmpty(xml))
			{
				try
				{
					using (var sr = new StringReader(xml))
					using (var xmlReader = XmlReader.Create(sr))
					{
						var ser = new XmlSerializer(typeof(T));

						if (ser.CanDeserialize(xmlReader))
							value = (T)ser.Deserialize(xmlReader);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			return value;
		}
	}
}
