using ScrapDealer.Api.Extensions;
using ScrapDealer.Infrastructure;
using ScrapDealer.Infrastructure.Logging;
using ScrapDealer.Shared;
using ScrapDealer.Shared.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalRConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new PersianDateTimeConverter());
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJsFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
            {
                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                    return false;

                var host = uri.Host;
                var port = uri.Port;

                if (host == "zayron.ir" || host.EndsWith(".zayron.ir"))
                    return true;

                return (host == "localhost" || host == "127.0.0.1") &&
                       (port == 3000 || port == 3001);
            })
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwaggerConfig();

app.UseCors("AllowNextJsFrontend");

app.UseShared();
app.UseMiddleware<LoggingMiddleware>();
app.UseSignalR();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
