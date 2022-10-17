
using System.Reflection;

using Microsoft.OpenApi.Models;

using SlackBot;
using SlackBot.Middleware;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddSecret<Secret>("SECRET_NAME");
builder.Services.AddApiClient();
builder.Services.AddMetadataServerClient();
builder.Services.AddSlackClient();
builder.Services.AddFireStore();

// Only add PubSub for production as it relies on the production environment
if (!builder.Environment.IsDevelopment()) {
  builder.Services.AddPubSub();
}

builder.Services.AddHttpContextAccessor();

// Add controller mapping
builder.Services.AddControllers()
        // Force the backend to send responses as-is and not lowercase everything
        .AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);

if (builder.Environment.IsDevelopment()) {

  // Create swagger documentation
  builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo() {
      Version = "v1",
      Title = "Slack app",
      Description = "Slack app"
    });

    // Include function xml comments in generated document
    string? xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  });
}

// Create web application
WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment()) {

  // Configure the HTTP request pipeline.
  app.UseSwagger(options => options.SerializeAsV2 = true);
  app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
  });
}

app.UseRouting();

app.UseAuthentication().UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

if (app.Environment.IsDevelopment()) {
  // Redirect invalid requests to the documentation
  app.UseMiddleware<InvalidRouteRedirect>();
}

// Start application
app.Run();
