using System.Data.SqlClient;

namespace APBD5.Animal;

public interface IAnimalRepository
{
    public IEnumerable<Animal> GetAll(string orderBy);
    public bool Create(AnimalDTO animalDto);
    public bool Update(int id, AnimalDTO animalDto);
    public bool Delete(int id);
}

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> GetAll(string orderBy)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        string[] allowedColumns = ["idanimal", "name", "description", "category", "area"];
        int orderColumn = Array.IndexOf(allowedColumns, orderBy.ToLower());
        if (orderColumn < 0)
            orderColumn = 1;
        using var command = new SqlCommand($"SELECT IdAnimal, Name, Description, Category, Area FROM s29143.Animal ORDER BY {allowedColumns[orderColumn]}", connection);

        var animals = new List<Animal>();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var animal = new Animal
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!
            };

            animals.Add(animal);
        }

        return animals;
    }

    public bool Create(AnimalDTO animalDto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO s29143.Animal (Name, Description, Category, Area) " +
                                           "VALUES (@name, @description, @category, @area)", connection);
        command.Parameters.AddWithValue("name", animalDto.Name);
        command.Parameters.AddWithValue("description", animalDto.Description);
        command.Parameters.AddWithValue("category", animalDto.Category);
        command.Parameters.AddWithValue("area", animalDto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Update(int id, AnimalDTO animalDto)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("UPDATE s29143.Animal SET Name = @name, Description = @description," +
                                           "Category = @category, Area = @area WHERE IdAnimal = @id", connection);
        command.Parameters.AddWithValue("id", id);
        command.Parameters.AddWithValue("name", animalDto.Name);
        command.Parameters.AddWithValue("description", animalDto.Description);
        command.Parameters.AddWithValue("category", animalDto.Category);
        command.Parameters.AddWithValue("area", animalDto.Area);

        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int id)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString"));
        connection.Open();
        
        using var command = new SqlCommand("DELETE FROM s29143.Animal WHERE IdAnimal = @id", connection);
        command.Parameters.AddWithValue("id", id);

        return command.ExecuteNonQuery() == 1;
    }
}