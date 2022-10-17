namespace SlackBot {

  /// <summary>
  /// Application constants
  /// </summary>
  public static class Constants {

    /// <summary>
    /// The current and next loadshedding statuses for South Africa and (Optional) municipal overrides
    /// </summary>
    public static readonly string EspStatus = @"https://developer.sepush.co.za/business/2.0/status";

    /// <summary>
    /// This single request has everything you need to monitor upcoming loadshedding events for the chosen suburb.
    /// </summary>
    public static readonly string EspAreaInformation = @"https://developer.sepush.co.za/business/2.0/area";

    /// <summary>
    /// Find areas based on GPS coordinates (latitude and longitude).
    /// </summary>
    public static readonly string EspAreasNearby = @"https://developer.sepush.co.za/business/2.0/areas_nearby";

    /// <summary>
    /// Search area based on text
    /// </summary>
    public static readonly string EspAreasSearch = @"https://developer.sepush.co.za/business/2.0/areas_search";

    /// <summary>
    /// Find topics created by users based on GPS coordinates (latitude and longitude).
    /// </summary>
    public static readonly string EspTopicsNearby = @"https://developer.sepush.co.za/business/2.0/topics_nearby";

    /// <summary>
    /// Check allowance allocated for token
    /// </summary>
    public static readonly string EspCheckAllowance = @"https://developer.sepush.co.za/business/2.0/api_allowance";
  }
}
