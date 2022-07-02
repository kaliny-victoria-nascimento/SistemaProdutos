using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Database;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Execute("INSERT INTO Product VALUES(@id, @name, @price, @active)", product);

        return product;
    }


    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);

        connection.Execute("DELETE FROM Product WHERE id = $id", new { Id = id });
    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Execute("UPDATE products SET active = True WHERE Id = @id", new {id = id});
    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Execute("UPDATE products SET active = False WHERE Id = @id", new {id = id});
    }

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        var product = connection.Query<Product>("SELECT * FROM Product").ToList();

        return product;
    }
}
