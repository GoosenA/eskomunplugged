namespace SlackBot.Library {

  /// <summary>
  ///
  /// </summary>
  public class FirestoreHandler : ControllerBase {

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    private CollectionReference _collection { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="firestore"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public FirestoreHandler(FirestoreDb firestore, string collection) =>
        _collection = firestore.Collection(collection);

    /// <summary>
    ///
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    public List<FirestoreEntry> GetEntriesByTeamId(string teamId) {

      // But we can apply filters, perform ordering etc too.
      Query userQuery = _collection.WhereEqualTo("team_id", teamId);

      QuerySnapshot userResults = userQuery.GetSnapshotAsync().Result;

      var entries = new List<FirestoreEntry>();
      foreach (DocumentSnapshot documentSnapshot in userResults.Documents) {
        entries.Add(documentSnapshot.ConvertTo<FirestoreEntry>());
      }

      return entries;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<FirestoreEntry> GetEntriesByUserId(string userId) {

      // But we can apply filters, perform ordering etc too.
      Query userQuery = _collection.WhereEqualTo("user_id", userId);

      QuerySnapshot userResults = userQuery.GetSnapshotAsync().Result;

      var entries = new List<FirestoreEntry>();
      foreach (DocumentSnapshot documentSnapshot in userResults.Documents) {
        entries.Add(documentSnapshot.ConvertTo<FirestoreEntry>());
      }

      return entries;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public List<KeyValuePair<DocumentReference, FirestoreEntry>> GetCompleteEntriesByUserId(string userId) {

      // But we can apply filters, perform ordering etc too.
      Query userQuery = _collection.WhereEqualTo("user_id", userId);

      QuerySnapshot userResults = userQuery.GetSnapshotAsync().Result;

      var entries = new List<KeyValuePair<DocumentReference, FirestoreEntry>>();
      foreach (DocumentSnapshot documentSnapshot in userResults.Documents) {
        entries.Add(new KeyValuePair<DocumentReference, FirestoreEntry>(documentSnapshot.Reference, documentSnapshot.ConvertTo<FirestoreEntry>()));
      }

      return entries;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public List<KeyValuePair<DocumentReference, FirestoreEntry>> GetCompleteEntries() {

      QuerySnapshot userResults = _collection.GetSnapshotAsync().Result;

      var entries = new List<KeyValuePair<DocumentReference, FirestoreEntry>>();
      foreach (DocumentSnapshot documentSnapshot in userResults.Documents) {
        entries.Add(new KeyValuePair<DocumentReference, FirestoreEntry>(documentSnapshot.Reference, documentSnapshot.ConvertTo<FirestoreEntry>()));
      }

      return entries;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public DocumentReference CreateEntry(FirestoreEntry entry) =>
        _collection.AddAsync(entry).Result;

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task<WriteResult> UpdateField(DocumentReference document, string field, object value) =>
        await document.UpdateAsync(field, value).ConfigureAwait(false);

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <param name="changes"></param>
    /// <returns></returns>
    public async Task<WriteResult> UpdateEntryField(DocumentReference document, Dictionary<FieldPath, object> changes) =>
      await document.UpdateAsync(changes).ConfigureAwait(false);

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    public async Task<DocumentReference> UpdateEntry(DocumentReference document, FirestoreEntry entry) {
      _ = await document.DeleteAsync().ConfigureAwait(false);
      return CreateEntry(entry);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public WriteResult DeleteEntry(DocumentReference document) =>
        document.DeleteAsync().Result;
  }
}
