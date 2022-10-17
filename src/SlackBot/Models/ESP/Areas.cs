namespace SlackBot.Models {

  /// <summary>
  /// Eskom se push Areas
  /// </summary>
  public class EspAreas {

    /// <summary>
    /// List of areas
    /// </summary>
    public List<area> areas { get; set; } = new List<area>();
  }

  /// <summary>
  /// Model describing a geographic area
  /// </summary>
  public class area {

    /// <summary>
    /// Area Id
    /// </summary>
    public string? id { get; set; }

    /// <summary>
    /// Name of the area
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// Region in which the area falls
    /// </summary>
    public string? region { get; set; }
  }
}
