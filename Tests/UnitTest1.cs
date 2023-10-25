using Npgsql;
using Testcontainers.PostgreSql;

namespace Tests;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        var container = new PostgreSqlBuilder().Build();
        await container.StartAsync();

        await using var connection = new NpgsqlConnection(container.GetConnectionString());
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT 1", connection);
        await command.ExecuteScalarAsync();

        await container.StopAsync();
    }
}