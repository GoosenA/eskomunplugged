namespace SlackBot.Library {

  /// <summary>
  ///
  /// </summary>
  public class ESPHandler {

    private ApiClient _client { get; }
    private string _authToken { get; }
    private string _areaSearchUrl => Constants.EspAreasSearch;
    private string _areaCoordinateUrl => Constants.EspAreasNearby;
    private string _statusUrl => Constants.EspStatus;
    private string _informationUrl => Constants.EspAreaInformation;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="client">Api Client object</param>
    /// <param name="token">Secret object</param>
    public ESPHandler(ApiClient client, string token) {
      _client = client;
      _authToken = token;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public EspAreas GetAreasByText(string value) {
      string areas = _client.MakeRequest($"{_areaSearchUrl}?text={Uri.EscapeUriString(value)}", _authToken);

      return string.IsNullOrWhiteSpace(areas) ?
        new EspAreas() :
        JsonSerialiser.Deserialize<EspAreas>(areas);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public GPSAreas GetAreasByGPS(string lat, string lon) {
      string areas = _client.MakeRequest($"{_areaCoordinateUrl}?lat={Uri.EscapeUriString(lat)}&lon={Uri.EscapeUriString(lon)}", _authToken);

      return string.IsNullOrWhiteSpace(areas) ?
        new GPSAreas() :
        JsonSerialiser.Deserialize<GPSAreas>(areas);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public Schedule GetSchedule() {
      string scheduleInformation = _client.MakeRequest(_statusUrl, _authToken);

      return string.IsNullOrWhiteSpace(scheduleInformation) ?
        new Schedule() :
        JsonSerialiser.Deserialize<Schedule>(scheduleInformation);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AreaScheduleInformation GetInformationForAreaById(string id) {
      string areaInfo = _client.MakeRequest($"{_informationUrl}?id={Uri.EscapeUriString(id)}", _authToken);
      return string.IsNullOrWhiteSpace(areaInfo) ?
        new AreaScheduleInformation() :
        JsonSerialiser.Deserialize<AreaScheduleInformation>(areaInfo);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public FirestoreEntry GenerateScheduleForAreaById(FirestoreEntry entry) {

      AreaScheduleInformation info = GetInformationForAreaById(entry.area);
      string today = DateTime.Now.Date.ToString("yyyy-MM-dd");

      var schedule = new ScheduleDay();
      if (info.schedule.days.Any(entry => entry.date == today)) {
        schedule = info.schedule.days.First(entry => entry.date == today);
      }

      entry.stage_one = schedule.stages[0].SelectMany(info => info.Split('-')).ToList();
      entry.stage_two = schedule.stages[1].SelectMany(info => info.Split('-')).ToList();
      entry.stage_three = schedule.stages[2].SelectMany(info => info.Split('-')).ToList();
      entry.stage_four = schedule.stages[3].SelectMany(info => info.Split('-')).ToList();
      entry.stage_five = schedule.stages[4].SelectMany(info => info.Split('-')).ToList();
      entry.stage_six = schedule.stages[5].SelectMany(info => info.Split('-')).ToList();
      entry.stage_seven = schedule.stages[6].SelectMany(info => info.Split('-')).ToList();
      entry.stage_eight = schedule.stages[7].SelectMany(info => info.Split('-')).ToList();

      return entry;
    }
  }
}
