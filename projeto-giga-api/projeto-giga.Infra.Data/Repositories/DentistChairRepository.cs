using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using projeto_giga.Domain.Entities;
using projeto_giga.Domain.Interfaces;

namespace projeto_giga.Infra.Data.Repositories;

public class DentistChairRepository : IDentistChairRepository
{
    private readonly string _connectionString;

    public DentistChairRepository(IConfiguration configuration)
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

    private static DentistChair MapDentistChair(MySqlDataReader reader)
    {
        return new DentistChair(
            reader.GetInt32("Id"),
            reader.GetInt32("Number"),
            reader.GetString("Description"),
            reader.GetBoolean("IsActive")
        );
    }

    public List<DentistChair> GetAll()
    {
        var chairs = new List<DentistChair>();

        using var connection = GetConnection();

        using var command = new MySqlCommand("SELECT Id, Number, Description, IsActive FROM DentistChairs", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            chairs.Add(MapDentistChair(reader));
        }

        return chairs;
    }

    public void Create(DentistChair chair)
    {
        using var connection = GetConnection();

        using var command = new MySqlCommand(@"
            INSERT INTO DentistChairs (Number, Description, IsActive) 
            VALUES (@Number, @Description, @IsActive)", connection);

        command.Parameters.AddWithValue("@Number", chair.Number);
        command.Parameters.AddWithValue("@Description", chair.Description);
        command.Parameters.AddWithValue("@IsActive", chair.IsActive);

        command.ExecuteNonQuery();
    }

    public DentistChair? GetById(int id)
    {
        using var connection = GetConnection();

        using var command = new MySqlCommand("SELECT Id, Number, Description, IsActive FROM DentistChairs WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return MapDentistChair(reader);
        }

        return null;
    }

    public void Update(DentistChair chair)
    {
        using var connection = GetConnection();

        using var command = new MySqlCommand(@"
            UPDATE DentistChairs 
            SET Number = @Number, Description = @Description, IsActive = @IsActive 
            WHERE Id = @Id", connection);

        command.Parameters.AddWithValue("@Number", chair.Number);
        command.Parameters.AddWithValue("@Description", chair.Description);
        command.Parameters.AddWithValue("@IsActive", chair.IsActive);
        command.Parameters.AddWithValue("@Id", chair.Id);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = GetConnection();

        using var command = new MySqlCommand("DELETE FROM DentistChairs WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}
