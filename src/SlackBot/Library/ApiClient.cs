namespace SlackBot.Library {

  /// <summary>
  /// API Client to simplify making API requests
  /// </summary>
  public class ApiClient {

    /// <summary>
    /// HttpClient object to use when making API requests
    /// </summary>
    private HttpClient _client { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public ApiClient() => _client = new HttpClient();

    /// <summary>
    /// Make a REST API request with a specified method to a specified URL
    /// </summary>
    /// <param name="url">URL to which the request should be made</param>
    /// <param name="authToken">Authorisation token used in the "key" header</param>
    /// <returns>Response content as string</returns>
    public string MakeRequest(string url, string authToken) {
      var message = new HttpRequestMessage() {
        RequestUri = new Uri(url),
        Method = HttpMethod.Get
      };

      message.Headers.Add("Token", authToken);

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
