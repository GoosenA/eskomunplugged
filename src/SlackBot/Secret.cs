namespace SlackBot {

  /// <summary>
  /// Application secret
  /// </summary>
  public class Secret {

    /// <summary>
    /// PubSub credentials
    /// </summary>
    public string? PubSub { get; set; }

    /// <summary>
    /// Eskom se push Authentication token for API
    /// </summary>
    public string? EspToken { get; set; }
  }
}
