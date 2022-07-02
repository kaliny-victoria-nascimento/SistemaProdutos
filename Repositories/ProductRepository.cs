using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Database;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repositories;


//nao ta rodando***
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

    public Boolean ExitsById(int id){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        return connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Products WHERE Id = @id;",new {id = id});       
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
    //rodando***
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

    // Retorna os produtos dentro de um intervalo de preço*********NAO TA RODANDO 
    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        var products = connection.Query<Product>("SELECT * FROM products WHERE price > @initialPrice AND price < @endPrice", new {initialPrice = initialPrice, endPrice = endPrice}).ToList();
        return products;
    }

    // Retorna os produtos com preço acima de um preço especificado
    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        var products = connection.Query<Product>("SELECT * FROM products WHERE price > @price", new {price = price}).ToList();
        return products;
    }

    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        var products = connection.Query<Product>("SELECT * FROM products WHERE price < @price", new {price = price}).ToList();
        return products;
    }

    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);                          
        double avarage = connection.ExecuteScalar<double>("SELECT AVG(price) FROM products");
        return avarage;
    }

}
