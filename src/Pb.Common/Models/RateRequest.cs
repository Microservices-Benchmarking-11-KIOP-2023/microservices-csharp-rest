namespace Pb.Common.Models;

public class RateRequest
{
    public IEnumerable<string>? HotelIds { get; set; }
    public string? InDate { get; set; }
    public string? OutDate { get; set; }
}
