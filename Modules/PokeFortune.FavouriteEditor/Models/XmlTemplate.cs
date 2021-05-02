using PokeFortune.FavouriteEditor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PokeFortune.FavouriteEditor.Models
{ 
	[Serializable]
	public class XmlTemplate
	{
		public XmlTemplate()
		{

		}

		public XmlTemplate(IEnumerable<XmlCell> cells)
		{
			if (cells != null)
				Cells = new List<XmlCell>(cells);
		}

		[XmlArray]
		public List<XmlCell> Cells { get; set; }
	}

	[Serializable]
	public class XmlCell
	{
		public XmlCell()
		{

		}

		public XmlCell(RowType rowType, ColumnType columnType, string pokeID)
		{
			RowType = rowType;
			ColumnType = columnType;
			PokeID = pokeID;
		}

		[XmlAttribute]
		public RowType RowType { get; set; }

		[XmlAttribute]
		public ColumnType ColumnType { get; set; }

		[XmlAttribute]
		public string PokeID { get; set; }
	}
}
