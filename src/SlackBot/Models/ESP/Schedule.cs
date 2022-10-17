namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  public class Stage {

    /// <summary>
    ///
    /// </summary>
    public string? stage { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? stage_start_timestamp { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class Region {

    /// <summary>
    ///
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public List<Stage> next_stages { get; set; } = new List<Stage>();

    /// <summary>
    ///
    /// </summary>
    public string? stage { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? stage_updated { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class Grids {

    /// <summary>
    ///
    /// </summary>
    public Region capetown { get; set; } = new Region();

    /// <summary>
    ///
    /// </summary>
    public Region eskom { get; set; } = new Region();
  }

  /// <summary>
  ///
  /// </summary>
  public class Schedule {

    /// <summary>
    ///
    /// </summary>
    public Grids status { get; set; } = new Grids();
  }
}
