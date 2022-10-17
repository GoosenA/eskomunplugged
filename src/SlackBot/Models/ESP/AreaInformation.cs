namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  public class AreaScheduleInformation {

    /// <summary>
    ///
    /// </summary>
    public List<Event> events { get; set; } = new List<Event>();

    /// <summary>
    ///
    /// </summary>
    public AreaInformation info { get; set; } = new AreaInformation();

    /// <summary>
    ///
    /// </summary>
    public ScheduleDayInformation schedule { get; set; } = new ScheduleDayInformation();
  }

  /// <summary>
  ///
  /// </summary>
  public class Event {

    /// <summary>
    ///
    /// </summary>
    public string? end { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? note { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? start { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class AreaInformation {

    /// <summary>
    ///
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? region { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class ScheduleDayInformation {

    /// <summary>
    ///
    /// </summary>
    public List<ScheduleDay> days { get; set; } = new List<ScheduleDay>();
  }

  /// <summary>
  ///
  /// </summary>
  public class ScheduleDay {

    /// <summary>
    ///
    /// </summary>
    public string? date { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public List<List<string>> stages { get; set; } = new List<List<string>>();
  }
}
