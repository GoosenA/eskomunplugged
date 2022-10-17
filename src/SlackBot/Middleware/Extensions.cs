namespace SlackBot.Middleware {

  /// <summary>
  /// Extensions used for dependency injection
  /// </summary>
  public static class Extensions {
    /// <summary>
    /// Maps a secret from file.
    /// Expects the path to the secret to be in an env variable named SECRET_FILE
    /// </summary>
    /// <param name="services"></param>
    /// <param name="variableName">
    ///   [Optional] Name used for additional secrets that need to be mapped
    /// </param>
    /// <typeparam name="T">The type of secret model</typeparam>
    public static IServiceCollection AddSecret<T>(this IServiceCollection services, string variableName) where T : class, new() {

      // Get the secret name
      string? secretName = Environment.GetEnvironmentVariable(variableName);

      return services.AddSingleton(serviceProvider => string.IsNullOrWhiteSpace(secretName) || !File.Exists(secretName)
        ? new T()
        : JsonSerialiser.Deserialize<T>(File.ReadAllText(secretName)));
    }

    /// <summary>
    /// Add an API client that can be used throughout the application
    /// </summary>
    /// <param name="services">Existing IServiceCollection</param>
    /// <returns>Updated IServiceCollection with API client</returns>
    public static IServiceCollection AddApiClient(this IServiceCollection services) =>
      services.AddSingleton(new ApiClient());

    /// <summary>
    /// Add a Metadata Server API client that can be used throughout the application
    /// </summary>
    /// <param name="services">Existing IServiceCollection</param>
    /// <returns>Updated IServiceCollection with Metadata Server API client</returns>
    public static IServiceCollection AddMetadataServerClient(this IServiceCollection services) =>
      services.AddSingleton(new MetadataServerClient());

    /// <summary>
    /// Add a Metadata Server API client that can be used throughout the application
    /// </summary>
    /// <param name="services">Existing IServiceCollection</param>
    /// <returns>Updated IServiceCollection with Metadata Server API client</returns>
    public static IServiceCollection AddSlackClient(this IServiceCollection services) =>
      services.AddSingleton(new SlackClient());

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFireStore(this IServiceCollection services) =>
      services.AddSingleton(provider => {
        string projectId = provider.GetRequiredService<MetadataServerClient>().GetProjectID();
        return FirestoreDb.Create(projectId);
      });

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPubSub(this IServiceCollection services) =>
      services.AddSingleton(PublisherServiceApiClient.Create());
  }
}
