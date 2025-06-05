using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using projeto_giga.Domain.Entities;
using projeto_giga.Domain.Interfaces;

namespace projeto_giga.Infra.Data.Repositories;

public class AllocationRepository : IAllocationRepository
{
    private readonly string _connectionString;

    public AllocationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("Não foi possível conectar com o banco de dados");
    }

    private MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open();
        return connection;
    }

    private static Allocation MapAllocation(MySqlDataReader reader)
    {
        return new Allocation(
            reader.GetInt32("ChairId"),
            reader.GetDateTime("StartTime"),
            reader.GetDateTime("EndTime")
        )
        {
            Id = reader.GetInt32("Id")
        };
    }

    public void Add(Allocation allocation)
    {
        using var connection = GetConnection();

        using var command = new MySqlCommand(@"
            INSERT INTO Allocations (ChairId, StartTime, EndTime)
            VALUES (@ChairId, @StartTime, @EndTime)", connection);

        command.Parameters.AddWithValue("@ChairId", allocation.ChairId);
        command.Parameters.AddWithValue("@StartTime", allocation.StartTime);
        command.Parameters.AddWithValue("@EndTime", allocation.EndTime);

        command.ExecuteNonQuery();
    }

    public List<Allocation> GetAllocationsInPeriod(DateTime start, DateTime end)
    {
        var allocations = new List<Allocation>();

        using var connection = GetConnection();

        using var command = new MySqlCommand(@"
            SELECT Id, ChairId, StartTime, EndTime 
            FROM Allocations
            WHERE NOT (EndTime <= @StartTime OR StartTime >= @EndTime)", connection);

        command.Parameters.AddWithValue("@StartTime", start);
        command.Parameters.AddWithValue("@EndTime", end);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            allocations.Add(MapAllocation(reader));
        }

        return allocations;
    }

    public List<Allocation> GetAllocationsInLast7Days()
    {
        var allocations = new List<Allocation>();
        var since = DateTime.UtcNow.AddDays(-7);

        using var connection = GetConnection();

        using var command = new MySqlCommand(@"
            SELECT Id, ChairId, StartTime, EndTime
            FROM Allocations
            WHERE StartTime >= @Since", connection);

        command.Parameters.AddWithValue("@Since", since);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            allocations.Add(MapAllocation(reader));
        }

        return allocations;
    }

    public List<Allocation> GetAllAllocations()
    {
        var allocations = new List<Allocation>();

        using var connection = GetConnection();

        var query = @"
            SELECT 
                a.Id,
                a.ChairId,
                c.Number AS ChairNumber,
                a.StartTime,
                a.EndTime
            FROM 
                Allocations a
            INNER JOIN 
                DentistChairs c ON c.Id = a.ChairId
            ORDER BY a.StartTime ASC";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            allocations.Add(MapAllocation(reader));
        }

        return allocations;
    }
}
