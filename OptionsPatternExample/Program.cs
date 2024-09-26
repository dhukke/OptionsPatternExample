using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<AppOptions>()
    .BindConfiguration("AppOptions")
    .Validate(
        settings => {

            if (string.IsNullOrWhiteSpace(settings.Value))
            {
                return false;
            }

            return true;
        },
        "AppOptions.Value must have a value other than null or empty or white space."
    )
    .ValidateOnStart();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet(
    "/options-pattern",
    (
        IOptions<AppOptions> options,
        IOptionsSnapshot<AppOptions> optionsSnapshot,
        IOptionsMonitor<AppOptions> optionsMonitor
    )
    => Results.Ok(
        new
        {
            IOptions = options.Value.Value,
            IOptionsSnapshot = optionsSnapshot.Value.Value,
            IOptionsMonitorCurrentValue = optionsMonitor.CurrentValue.Value
        }
    )
)
.WithName("GetOptionsPattern")
.WithOpenApi();

app.Run();