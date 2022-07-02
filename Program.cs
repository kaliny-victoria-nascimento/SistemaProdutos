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
        if(productRepository.GetAll().Any()) {
            foreach (var product in productRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
            } 
        } else {
            Console.WriteLine("Nenhum produto cadastrado.");
        }
    }

    if(modelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        var name = args[3];
        var price = Convert.ToDouble(args[4]);
        var active = Convert.ToBoolean(args[5]);
        Console.WriteLine("Product New");
        
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
        Console.WriteLine("Product Delete");

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
        Console.WriteLine("Product Enable");
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
        Console.WriteLine("Product Disable");
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

    if(modelAction == "PriceBetween")
    {
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice = Convert.ToDouble(args[3]);
        Console.WriteLine("Product PriceBetween");

        if(productRepository.GetAllWithPriceBetween(initialPrice,endPrice).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceBetween(initialPrice,endPrice))
            {
                Console.WriteLine($" {product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço R$ {initialPrice} e R$ {endPrice}");
        }
    }

    if(modelAction == "PriceHigherThan")
    {   
        var price = Convert.ToDouble(args[2]);
        Console.WriteLine("Product PriceHigherThan");
        
        if(productRepository.GetAllWithPriceHigherThan(price).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine($" {product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto encontrado com preço maior que R$ {price}.");
        }

    }

    if(modelAction == "PriceLowerThan")
    {   
        var price = Convert.ToDouble(args[2]);
        Console.WriteLine("Product PriceLowerThan.");
        
        if(productRepository.GetAllWithPriceLowerThan(price).Any())
        {
            foreach(var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine($" {product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto encontrado com preço menor que R$ {price}."); 
        }

    }

    if(modelAction == "AveragePrice")
    {
        Console.WriteLine("Product AveragePrice");
        if(productRepository.GetAll().Any())
        {
            Console.WriteLine($"A média dos preços é R$ {productRepository.GetAveragePrice()}");
        }
        else
        {
            Console.WriteLine($"Nenhum produto cadastrado.");
        }
    }
}