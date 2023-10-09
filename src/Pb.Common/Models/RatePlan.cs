namespace Pb.Common.Models;

public class RatePlan
{
    public string? HotelId { get; set; }
    public string? Code { get; set; }
    public string? InDate { get; set; }
    public string? OutDate { get; set; }
    public RoomTypeModel? RoomType { get; set; }
}
