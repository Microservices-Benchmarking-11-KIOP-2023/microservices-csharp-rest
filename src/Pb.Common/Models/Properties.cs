namespace Pb.Common.Models;

public class Feature
{
    public Geometry? Geometry { get; set; }

    public string? Id { get; set; }

    public Properties? Properties { get; set; }

    public string? Type { get; set; }
}

public partial class Geometry
{
    public double[]? Coordinates { get; set; }

    public string? Type { get; set; }
}

public partial class Properties
{
    public string? Name { get; set; }
    
    public string? PhoneNumber { get; set; }
}

public class GeoJsonResponse
{
    public string? Type { get; set; }

    public IEnumerable<Feature?>? Features { get; set; }
}