
namespace SlackBot.Controllers {

  /// <summary>
  /// APIs related to suburb data
  /// </summary>
  [ApiController]
  [Route("suburbs")]
  public class SuburbController : ControllerBase {

    private SlackClient _slackClient { get; }
    private FirestoreHandler _firestoreHandler { get; }
    private ESPHandler _espHandler { get; }
    private PubSubHandler _pubSubHandler { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="client">Api Client object</param>
    /// <param name="secret">Secret object</param>
    public SuburbController(ApiClient client, SlackClient slackClient, FirestoreDb firestore, PublisherServiceApiClient pubsub, MetadataServerClient meta, Secret secret) {
      _slackClient = slackClient;
      _firestoreHandler = new FirestoreHandler(firestore, "user_data");
      _espHandler = new ESPHandler(client, secret.EspToken);
      _pubSubHandler = new PubSubHandler(pubsub, meta.GetProjectID(), secret.PubSub);
    }

    /// <summary>
    /// Get available suburbs as select menu options filtered by search criteria
    /// </summary>
    /// <param name="info">Filter value</param>
    /// <returns>Select Menu option list of available suburbs</returns>
    [HttpPost("text")]
    [Consumes("application/x-www-form-urlencoded")]
    public ActionResult GetSuburbsByText([FromForm] Dictionary<string, string> info) {

      if (!info.TryGetValue("text", out string value) || !info.TryGetValue("response_url", out string responseUrl)) {
        return NotFound();
      }

      EspAreas matchingAreas = _espHandler.GetAreasByText(value);

      var message = new BlockKitMessage();
      message.blocks.Add(new HeaderBlock() {
        text = new PlainTextDescriptor() {
          text = $"Suburbs matching '{value}'"
        }
      });
      message.blocks.Add(new DividerBlock());

      message.blocks.Add(new SectionBlock() {
        text = new MarkdownTextDescriptor() {
          text = "Pick a suburb from the dropdown list"
        },
        accessory = new StaticSelectAccessory() {
          placeholder = new SelectPlaceHolder() {
            text = "Suburbs",
            emoji = true
          },
          options = matchingAreas.areas.Select(scheduleInfo => new SelectOption() {
            text = new SelectOptionText() {
              text = scheduleInfo.name,
              emoji = true
            },
            value = scheduleInfo.id
          }).ToList(),
          action_id = $"select_suburb"
        }
      });

      var response = new {
        message.blocks,
        response_type = "ephemeral"
      };

      _slackClient.MakeRequest(responseUrl, response);

      return Ok();
    }

    /// <summary>
    /// Get available suburbs as select menu options filtered by GPS coordinates
    /// </summary>
    /// <param name="info">Latitude coordinate</param>
    /// <returns>Select Menu option list of available suburbs</returns>
    [HttpPost("gps")]
    [Consumes("application/x-www-form-urlencoded")]
    public ActionResult<SelectMenuOptionList> GetSuburbsByLocation([FromForm] Dictionary<string, string> info) {

      if (!info.TryGetValue("text", out string value) || !info.TryGetValue("response_url", out string responseUrl)) {
        return NotFound();
      }

      string[] coordinates = value.Split(',');

      if (coordinates.Length != 2) {
        return BadRequest();
      }

      GPSAreas matchingAreas = _espHandler.GetAreasByGPS(coordinates[0], coordinates[1]);

      var message = new BlockKitMessage();
      message.blocks.Add(new HeaderBlock() {
        text = new PlainTextDescriptor() {
          text = $"Suburbs matching '{value}'"
        }
      });
      message.blocks.Add(new DividerBlock());

      message.blocks.Add(new SectionBlock() {
        text = new MarkdownTextDescriptor() {
          text = "Pick a suburb from the dropdown list"
        },
        accessory = new StaticSelectAccessory() {
          placeholder = new SelectPlaceHolder() {
            text = "Suburbs",
            emoji = true
          },
          options = matchingAreas.areas.Select(scheduleInfo => new SelectOption() {
            text = new SelectOptionText() {
              text = $"({scheduleInfo.count}) {scheduleInfo.id}",
              emoji = true
            },
            value = scheduleInfo.id
          }).ToList(),
          action_id = $"select_suburb_gps"
        }
      });

      var response = new {
        message.blocks,
        response_type = "ephemeral"
      };

      _slackClient.MakeRequest(responseUrl, response);

      return Ok();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPost("set")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<ActionResult> SetSuburb([FromForm] Dictionary<string, string> info) {

      Console.WriteLine(JsonSerialiser.Serialize(info));

      BlockKitResponse payload = JsonSerialiser.Deserialize<BlockKitResponse>(info["payload"]);

      Console.WriteLine(JsonSerialiser.Serialize(payload));

      List<KeyValuePair<DocumentReference, FirestoreEntry>> entries = _firestoreHandler.GetCompleteEntriesByUserId(payload.user.id);

      if (!entries.Any(entry => entry.Value.user_id == payload.user.id)) {
        var newUser = new FirestoreEntry() {
          user_id = payload.user.id,
          name = payload.user.name,
          area = payload.actions.FirstOrDefault().selected_option.value.ToString(),
          team_id = payload.user.team_id
        };

        _ = _firestoreHandler.CreateEntry(newUser);
      } else {
        DocumentReference document = entries.FirstOrDefault(entry => entry.Value.user_id == payload.user.id).Key;
        _ = _firestoreHandler.UpdateEntryField(document, new Dictionary<FieldPath, object>() {
            {
              new FieldPath("area"),
              payload.actions.FirstOrDefault().selected_option.value
            }
        });
      }

      _pubSubHandler.PublishMessage("{}");

      _slackClient.MakeRequest(payload.response_url, "Suburb updated.");

      return Ok();
    }
  }
}
