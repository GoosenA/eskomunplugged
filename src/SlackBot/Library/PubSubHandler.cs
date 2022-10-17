using Google.Protobuf;

namespace SlackBot.Library {

  /// <summary>
  ///
  /// </summary>
  public class PubSubHandler : ControllerBase {
    private PublisherServiceApiClient _client { get; }
    private TopicName _topic { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="client"></param>
    /// <param name="projectId"></param>
    /// <param name="topic"></param>
    public PubSubHandler(PublisherServiceApiClient client, string projectId, string topic) {
      _client = client;
      _topic = new TopicName(projectId, topic);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="messageContent"></param>
    public void PublishMessage(string messageContent) {
      // Publish a message to the topic.
      var message = new PubsubMessage {
        // The data is any arbitrary ByteString. Here, we're using text.
        Data = ByteString.CopyFromUtf8(messageContent)
      };
      _client.Publish(_topic, new[] { message });
    }
  }
}
