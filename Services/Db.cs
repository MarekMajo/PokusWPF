using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MSK_PC_Controller.Services;

internal static class Db
{
    private static readonly Lazy<string> _cs = new(LoadConnectionString);

    public static SqlConnection Conn() => new SqlConnection(_cs.Value);

    private static string LoadConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        return config.GetConnectionString("MainDb")
               ?? throw new InvalidOperationException("Missing ConnectionStrings:MainDb in appsettings.json");
    }
}