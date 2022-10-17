namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  public class BlockKitResponse {

    /// <summary>
    ///
    /// </summary>
    public List<BlockKitResponseActions> actions { get; set; } = new List<BlockKitResponseActions>();

    /// <summary>
    ///
    /// </summary>
    public BlockKitResponseUser user { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string response_url { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class BlockKitResponseUser {

    /// <summary>
    ///
    /// </summary>
    public string? id { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? username { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? team_id { get; set; }
  }

  /// <summary>
  ///
  /// </summary>
  public class BlockKitResponseActions {

    /// <summary>
    ///
    /// </summary>
    public string action_ts { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string type { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string action_id { get; set; }

    /// <summary>
    ///
    /// </summary>
    public BlockKitResponseActionOption selected_option { get; set; } = new BlockKitResponseActionOption();
  }

  /// <summary>
  ///
  /// </summary>
  public class BlockKitResponseActionOption {

    /// <summary>
    ///
    /// </summary>
    public string value { get; set; }
  }
}
