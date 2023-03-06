namespace OAuth.Phone.UseCases.Utils;

internal static class RandomUtils
{
	public static int NextConfirmationCode()
	{
		const int min = 1000;
		const int max = 9999;
		return new Random(Environment.TickCount).Next(min, max);
	}
}