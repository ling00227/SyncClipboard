using SyncClipboard.Server.Core;
using SyncClipboard.Server.Core.Controller;
using SyncClipboard.Server.Core.CredentialChecker;

namespace SyncClipboard.Server;

public class Program
{
    private const string ENV_VAR_USERNAME = "SYNCCLIPBOARD_USERNAME";
    private const string ENV_VAR_PASSWORD = "SYNCCLIPBOARD_PASSWORD";
    private const string ENV_VAR_RELOAD_ON_CHANGE = "ASPNETCORE_hostBuilder__reloadConfigOnChange";

    public static void Main(string[] args)
    {
        var reloadOnChangeEnvStr = Environment.GetEnvironmentVariable(ENV_VAR_RELOAD_ON_CHANGE);
        if (string.IsNullOrEmpty(reloadOnChangeEnvStr))
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_hostBuilder__reloadConfigOnChange", "false");
        }

        var builder = WebApplication.CreateBuilder(
            new WebApplicationOptions
            {
                Args = args,
                WebRootPath = "server"
            }
        );

        var envUsername = Environment.GetEnvironmentVariable(ENV_VAR_USERNAME);
        var envPassword = Environment.GetEnvironmentVariable(ENV_VAR_PASSWORD);
        if (!string.IsNullOrEmpty(envUsername) && !string.IsNullOrEmpty(envPassword))
        {
            builder.Services.AddSingleton<ICredentialChecker, StaticCredentialChecker>(_ =>
                new StaticCredentialChecker(envUsername, envPassword)
            );
        }
        else
        {
            builder.Services.AddSingleton<ICredentialChecker, FileCredentialChecker>();
        }
        var app = Web.Configure(builder);
        app.UseSyncCliboardServer();
        app.Run();
    }
}