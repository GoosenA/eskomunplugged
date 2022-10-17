namespace SlackBot.Models {

  /// <summary>
  /// Eskom se push Areas
  /// </summary>
  public class GPSAreas {

    /// <summary>
    /// List of areas
    /// </summary>
    public List<GpsArea> areas { get; set; } = new List<GpsArea>();
  }

  /// <summary>
  /// Model describing a geographic area
  /// </summary>
  public class GpsArea {

    /// <summary>
    /// Area Id
    /// </summary>
    public string? id { get; set; }

    /// <summary>
    /// Name of the area
    /// </summary>
    public int count { get; set; }
  }
}
