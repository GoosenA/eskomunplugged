namespace SlackBot.Library {

  /// <summary>
  /// API Client to simplify making API requests
  /// </summary>
  public class SlackClient {

    /// <summary>
    /// HttpClient object to use when making API requests
    /// </summary>
    private HttpClient _client { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public SlackClient() => _client = new HttpClient();

    /// <summary>
    /// Make a REST API request with a specified method to a specified URL
    /// </summary>
    /// <param name="url">URL to which the request should be made</param>
    /// <param name="content"></param>
    /// <returns>Response content as string</returns>
    public void MakeRequest(string url, object content) {

      string json = JsonSerialiser.Serialize(content);

      Console.WriteLine($"[RESPONSE] {json}");
      var messageContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

      var message = new HttpRequestMessage() {
        RequestUri = new Uri(url),
        Method = HttpMethod.Post,
        Content = messageContent
      };

      // Read response content into variable
      _client.SendAsync(message);
    }
  }
}
