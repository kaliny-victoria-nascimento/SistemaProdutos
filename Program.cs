using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var productRepository = new ProductRepository(databaseConfig);

//Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Product List");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", product.Id, product.Name, product.Price, product.Active);
        }
    }

    if(modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        var name = args[3];
        var price = Convert.ToDouble(args[4]);
        var active = Convert.ToBoolean(args[5]);
        
        var product = new Product(id, name, price, active);

        if(productRepository.ExitsById(id))
        {
            Console.WriteLine($"O produto com Id {id} já existe.");
        }
        else
        {
            productRepository.Save(product);
            Console.WriteLine($"Produto {name} cadastrado com sucesso.");            
        } 
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if(productRepository.ExitsById(id))
        {
            productRepository.Delete(id);
            Console.WriteLine($"Produto {id} removido com sucesso!");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado!");
        }        
    }

    if(modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);
        if(productRepository.ExitsById(id))
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto {id} habilitado com sucesso.");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado.");
        }
    }

    if(modelAction == "Disable")
    {
        var id = Convert.ToInt32(args[2]);
        if(productRepository.ExitsById(id))
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto {id} desabilitado com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado");
        }
    }
}