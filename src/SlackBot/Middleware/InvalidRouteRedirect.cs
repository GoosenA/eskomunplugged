namespace SlackBot.Middleware {
  /// <summary>
  /// Middleware that redirects invalid requests to the swagger documentation
  /// </summary>
  public class InvalidRouteRedirect {

    /// <summary>
    /// Request pipeline
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Middleware constructor
    /// </summary>
    /// <param name="next">Pipeline information</param>
    public InvalidRouteRedirect(RequestDelegate next) => _next = next;

    /// <summary>
    /// Main function that redirects invalid requests to the swagger documentation
    /// NB! The requests is completed and redirected afterwards
    /// </summary>
    /// <param name="context">Transaction context</param>
    /// <returns>
    ///   Response from endpoint if a valid request, redirect to documentation otherwise
    /// </returns>
    public async Task InvokeAsync(HttpContext context) {
      // Execute request
      await _next(context);

      context.Response.Redirect("/");
    }
  }
}
