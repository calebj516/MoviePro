using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace MoviePro.Services
{
    public class ConnectionService
    {
        // This method will determine if the app is running locally or in a production hosting environment like Heroku.
        // If the Environment Variable, DATABASE_URL is null or empty then we are running locally.
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        // This method will take the string from the Environment Variable DATABASE_URL. It is a specially formatted string.
        // We need to break it apart and reformat it into a format that our app understands. 
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }
    }
}
