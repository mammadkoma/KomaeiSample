namespace KomaeiSample.Share.Common;
public static class Helper
{
    public static List<IdTitle> GetOrderStatuses()
    {
        return new List<IdTitle>() {
            new IdTitle { Id = 2, Title = "پرداخت شده" },
            new IdTitle { Id = 3, Title = "ارسال برای چاپ" },
            new IdTitle { Id = 4, Title = "آماده تحویل" },
        };
    }

    public static int GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}
