// using System.Reflection;

namespace SlackBot.Controllers {

  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [Route("schedules")]
  public class NotificationController : ControllerBase {
    private SlackClient _slackClient { get; }
    private FirestoreHandler _firestoreHandler { get; }
    private ESPHandler _espHandler { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="slackClient">Api Client object</param>
    /// <param name="firestore">Secret object</param>
    public NotificationController(ApiClient client, SlackClient slackClient, FirestoreDb firestore, Secret secret) {
      _slackClient = slackClient;
      _firestoreHandler = new FirestoreHandler(firestore, "user_data");
      _espHandler = new ESPHandler(client, secret.EspToken);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<ActionResult> PostSchedule([FromForm] Dictionary<string, string> info) {

      if (!info.TryGetValue("team_id", out string teamId) || !info.TryGetValue("response_url", out string responseUrl)) {
        return NotFound();
      }

      List<FirestoreEntry> entries = _firestoreHandler.GetEntriesByTeamId(teamId);

      if (!entries.Any()) {
        _slackClient.MakeRequest(responseUrl, "No users specified their locations.");
        return Ok();
      }

      Schedule schedule = _espHandler.GetSchedule();
      int stage = Convert.ToInt32(schedule.status.eskom.stage);
      string today = DateTime.Now.ToString("yyyy-MM-dd");
      var message = new BlockKitMessage();

      if (stage == 0) {
        message.blocks.Add(new HeaderBlock() {
          text = new PlainTextDescriptor() {
            text = $"Unplugged - No loadshedding schedules for {today} (Stage {stage})"
          }
        });
        var earlyResponse = new {
          message.blocks,
          response_type = "ephemeral"
        };

        _slackClient.MakeRequest(responseUrl, earlyResponse);
        return Ok();
      }

      var userSchedules = new List<UserSchedule>();

      entries.ForEach(entry => {

        List<string> stages = GetSchedules(stage, entry);
        var schedules = new List<SimplifiedSchedule>();

        for (int i = 0; i < stages.Count; i += 2) {
          schedules.Add(new SimplifiedSchedule() {
            start = stages[i],
            end = stages[i + 1]
          });
        }

        userSchedules.Add(new UserSchedule() {
          Name = entry.name,
          Schedule = schedules
        });
      });

      message.blocks.Add(new HeaderBlock() {
        text = new PlainTextDescriptor() {
          text = $"Unplugged - Loadshedding schedules {today} (Stage {schedule.status.eskom.stage})"
        }
      });
      message.blocks.Add(new DividerBlock());

      message.blocks.AddRange(userSchedules.Select(schedule => new SectionBlock() {
        text = new MarkdownTextDescriptor() {
          text = schedule.Name
        },
        accessory = new OverflowAccessory() {
          options = schedule.Schedule.Select(scheduleInfo => new OverflowOption() {
            text = new OverflowText() {
              text = $"Start at {scheduleInfo.start} and end at {scheduleInfo.end}",
              emoji = true
            },
            value = $"{scheduleInfo.start.Substring(0, 2)}_{scheduleInfo.end.Substring(0, 2)}_{DateTime.Now.Ticks}"
          }).ToList(),
          action_id = $"Acc_{schedule.Name}"
        }
      }));

      var response = new {
        message.blocks,
        response_type = "ephemeral"
      };

      _slackClient.MakeRequest(responseUrl, response);

      return Ok();
    }

    private static List<string> GetSchedules(int stage, FirestoreEntry entry) => stage switch {
      1 => entry.stage_one,
      2 => entry.stage_two,
      3 => entry.stage_three,
      4 => entry.stage_four,
      5 => entry.stage_five,
      6 => entry.stage_six,
      7 => entry.stage_seven,
      8 => entry.stage_eight,
      _ => new List<string>()
    };
  }
}
