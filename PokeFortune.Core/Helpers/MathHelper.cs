namespace PokeFortune.Core.Helpers
{
	public static class MathHelper
	{
		public static int Modulo(int first, int second)
		{
			int result = first % second;
			if ((result < 0 && second > 0) || (result > 0 && second < 0))
				result += second;

			return result;
		}
	}
}
