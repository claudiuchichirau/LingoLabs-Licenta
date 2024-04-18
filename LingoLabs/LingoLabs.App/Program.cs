using Blazored.LocalStorage;
using LingoLabs.App;
using LingoLabs.App.Auth;
using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services;
using LingoLabs.App.Services.AuthServices;
using LingoLabs.App.Services.LanguageServices;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<RoleAuthorizationService, RoleAuthorizationService>();

builder.Services.AddSingleton<StateService>();

builder.Services.AddHttpClient<IUserDataService, UserDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<IChoiceDataService, ChoiceDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<IQuestionDataService, QuestionDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<IListeningLessonDataService, ListeningLessonDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<ILessonDataService, LessonDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<IChapterDataService, ChapterDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<ILanguageCompetenceDataService, LanguageCompetenceDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<ILanguageLevelDataService, LanguageLevelDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});
builder.Services.AddHttpClient<ILanguageDataService, LanguageDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});

builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7210/");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddMatBlazor();

await builder.Build().RunAsync();
