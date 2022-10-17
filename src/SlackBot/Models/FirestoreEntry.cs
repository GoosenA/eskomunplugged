namespace SlackBot.Models {

  /// <summary>
  ///
  /// </summary>
  [FirestoreData]
  public class FirestoreEntry {

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public string? user_id { get; set; }

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public string? name { get; set; }

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public string? team_id { get; set; }

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public string? area { get; set; }

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_one { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_two { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_three { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_four { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_five { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_six { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_seven { get; set; } = new List<string>();

    /// <summary>
    ///
    /// </summary>
    [FirestoreProperty]
    public List<string> stage_eight { get; set; } = new List<string>();
  }
}
