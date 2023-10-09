namespace Pb.Common.Models;

public class Hotel
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Description { get; set; }

    public Address? Address { get; set; }

    public IList<Image?>? Images { get; set; }
    
    public Feature[]? Features { get; set; }

    public string? Type { get; set; }
}