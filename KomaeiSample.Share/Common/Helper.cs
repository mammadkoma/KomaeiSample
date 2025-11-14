namespace KomaeiSample.Share.Common;

public static class Helper
{
    public static int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}
