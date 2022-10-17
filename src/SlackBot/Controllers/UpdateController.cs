namespace SlackBot.Controllers {

  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [Route("updates")]
  public class UpdateController : ControllerBase {
    private FirestoreHandler _firestoreHandler { get; }
    private ESPHandler _espHandler { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="client"></param>
    /// <param name="firestore"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public UpdateController(ApiClient client, FirestoreDb firestore, Secret secret) {
      _firestoreHandler = new FirestoreHandler(firestore, "user_data");
      _espHandler = new ESPHandler(client, secret.EspToken);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpPost("schedule")]
    public ActionResult UpdateSchedule() {
      List<KeyValuePair<DocumentReference, FirestoreEntry>> entries = _firestoreHandler.GetCompleteEntries();

      entries.ForEach(entry => {
        var newEntry = new FirestoreEntry() {
          user_id = entry.Value.user_id,
          name = entry.Value.name,
          team_id = entry.Value.team_id,
          area = entry.Value.area
        };

        newEntry = _espHandler.GenerateScheduleForAreaById(newEntry);

        _ = _firestoreHandler.DeleteEntry(entry.Key);
        _ = _firestoreHandler.CreateEntry(newEntry);
      });

      return Ok();
    }
  }
}
