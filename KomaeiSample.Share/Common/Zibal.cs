namespace KomaeiSample.Share.Common;
public class ZibalResponse
{
    public string? message { get; set; }
    public int result { get; set; }
    public long trackId { get; set; }
}

public class ZibalVerifyResponse
{
    public string? message { get; set; }
    public int? result { get; set; }
    public string? refNumber { get; set; }
    public DateTime? paidAt { get; set; }
    public int? status { get; set; }
    public long? amount { get; set; }
    public string? orderId { get; set; }
    public string? description { get; set; }
    public string? cardNumber { get; set; }
    public List<MultiplexingInfo>? multiplexingInfos { get; set; }
}

public class MultiplexingInfo
{

}

public class ConfirmPayRequestVm
{
    public List<int> Ids { get; set; } = new();
    public long RefNumber { get; set; }
    public DateTime? paidAt { get; set; }
}
