using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Pb.Common.Models;

public class Image
{
    public string? Url { get; set; }
    public bool? Default { get; set; }
}
