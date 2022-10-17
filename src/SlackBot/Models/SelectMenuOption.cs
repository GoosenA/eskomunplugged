namespace SlackBot.Models {

  /// <summary>
  /// Select Menu option list items
  /// </summary>
  public class SelectMenuOptionList {

    /// <summary>
    /// Select menu option list
    /// </summary>
    public List<SelectMenuOption> options { get; set; } = new List<SelectMenuOption>();
  }

  /// <summary>
  /// Select Menu option item
  /// </summary>
  public class SelectMenuOption {

    /// <summary>
    /// Select menu option text object
    /// </summary>
    public SelectMenuOptionText text { get; set; } = new SelectMenuOptionText();

    /// <summary>
    /// Select Menu Option Value
    /// </summary>
    public string? value { get; set; }
  }

  /// <summary>
  /// Select Menu Option text
  /// </summary>
  public class SelectMenuOptionText {

    /// <summary>
    /// Data type
    /// </summary>
    public string? type { get; set; }

    /// <summary>
    /// Test to display in the Select Menu dropdown
    /// </summary>
    public string? text { get; set; }
  }
}
