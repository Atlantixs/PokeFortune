using PokeFortune.Data.Enums;

namespace PokeFortune.Data.Helpers
{
	public static class PokemonHelper
	{
		public static GenerationType GetGenerationTypeByID(int id)
		{
			if (id >= 810)
				return GenerationType.EighthGen;
			else if (id >= 722)
				return GenerationType.SeventhGen;
			else if (id >= 650)
				return GenerationType.SixthGen;
			else if (id >= 494)
				return GenerationType.FifthGen;
			else if (id >= 387)
				return GenerationType.FourthGen;
			else if (id >= 252)
				return GenerationType.ThirdGen;
			else if (id >= 152)
				return GenerationType.SecondGen;
			else
				return GenerationType.FirstGen;
		}
	}
}
