namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  public class UserSchedule {

    /// <summary>
    ///
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public List<SimplifiedSchedule> Schedule { get; set; } = new List<SimplifiedSchedule>();
  }
}
