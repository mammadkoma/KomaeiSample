namespace KomaeiSample.Client.Configs;
public class ApiService
{
    public byte ApiCount { get; set; } = 0;

    public event Action? ApiCountChanged;

    public void IncreaseApiCount()
    {
        ApiCount++;
        ApiCountChanged?.Invoke();
    }

    public void DecreaseApiCount()
    {
        ApiCount--;
        ApiCountChanged?.Invoke();
    }

    public bool IsShowOverlay
    {
        get { return ApiCount > 0 ? true : false; }
        set { }
    }
}