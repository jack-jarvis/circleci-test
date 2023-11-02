using Npgsql;
using Testcontainers.PostgreSql;

namespace Tests;

public class Tests
{
    private static IEnumerable<TestCaseData> TestCases()
    {
        yield return new TestCaseData(new PostgreSqlBuilder().WithImage("postgres:10").Build()).SetName("Postgres v10");
        yield return new TestCaseData(new PostgreSqlBuilder().WithImage("postgres:15").Build()).SetName("Postgres v15");
        yield return new TestCaseData(new PostgreSqlBuilder().WithImage("postgres:16").Build()).SetName("Postgres v16");
    }
    
    [TestCaseSource(nameof(TestCases))]
    public async Task Test1(PostgreSqlContainer container)
    {
        await container.StartAsync();
        await using var connection = new NpgsqlConnection(container.GetConnectionString());
        Console.WriteLine("Connection string: " + connection.ConnectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT 1", connection);
        await command.ExecuteScalarAsync();
        await container.StopAsync();
    }
}