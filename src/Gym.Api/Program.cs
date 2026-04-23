using Gym.Api.Endpoints;
using Gym.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddAppDatabase();
builder.AddJwtAuthentication();
builder.AddDocumentation();
builder.AddNotificationsEvents();

#region Mediator

builder.AddMediator();

#endregion

#region Kafka

builder.AddSchemaRegistryClient();
builder.AddKafkaServices();

#endregion

#region Handlers

builder.AddAccountContextConfiguration();
builder.AddActivityContextConfiguration();
builder.AddPartnerContextConfiguration();

#endregion

builder.AddOpenTelemetryServices(); 
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

app.Run();
