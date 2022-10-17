namespace SlackBot.Library {

  /// <summary>
  /// API Client to simplify making API requests
  /// </summary>
  public class MetadataServerClient {

    /// <summary>
    /// HttpClient object to use when making API requests
    /// </summary>
    private HttpClient _client { get; }
    private static string _localIdentifier => "LOCAL";

    /// <summary>
    /// Default constructor
    /// </summary>
    public MetadataServerClient() => _client = new HttpClient();

    /// <summary>
    /// Get the current GCP project ID
    /// </summary>
    /// <returns>GCP Project ID</returns>
    public string GetProjectID() {
      try {
        return MakeRequest("http://metadata.google.internal/computeMetadata/v1/project/project-id");
      } catch {
        return _localIdentifier;
      }
    }

    /// <summary>
    /// Make a REST API request with a specified method to a specified URL
    /// </summary>
    /// <param name="url">URL to which the request should be made</param>
    /// <returns>Response content as string</returns>
    private string MakeRequest(string url) {
      var message = new HttpRequestMessage() {
        RequestUri = new Uri(url),
        Method = HttpMethod.Get
      };

      message.Headers.Add("Metadata-Flavor", "Google");

      string responseContent = "";

      // Read response content into variable
      using (HttpContent resultContent = _client.SendAsync(message).Result.Content) {
        Task<string> responseString = resultContent.ReadAsStringAsync();

        responseContent = responseString.Result;
      }

      return responseContent;
    }
  }
}
