// See https://aka.ms/new-console-template for more information
using EntityFrameworkLINQ.Configuration;
using EntityFrameworkLINQ.Database;
using Microsoft.Extensions.Configuration;
using System.Linq;

Console.WriteLine("Entity Framework and LINQ");

// Getting user secrets
var builder = new ConfigurationBuilder().AddUserSecrets<ConnectionStrings>().Build();
var secret = builder.GetSection("ConnectionStrings:SQLServer").Value;
Console.WriteLine($"Connection String: {secret}");

// Getting DB Context
using (var db = new DatabaseContext(secret))
{
    var carsmodels = db.CarsModels.ToList();
    Console.WriteLine($"CarsModels count: {carsmodels.Count}");

    // Query and get all cars
    Console.WriteLine($"1. Query and get all cars");
    var cars = db.CarsModels.ToList();
    cars.ForEach(car => Console.WriteLine($"Make: {car.make} - Model: {car.model} - Year: {car.year}"));

    // Query and get all cars of make BMW
    Console.WriteLine($"2. Query and get all cars of make BMW");
    var selectedCars = db.CarsModels.Where(x => x.make.Equals("BMW"))
                                    .Select( car => new { make = car.make, model = car.model })
                                    .Distinct().ToList();
    selectedCars.ForEach(car => Console.WriteLine($"{car.model}"));

    // Query and get all cars and group by make BMW
    Console.WriteLine($"3. Query and get all cars and group by make BMW");
    var carsG = db.CarsModels.GroupBy(car => car.make)
                                .Select(car => new { car.Key, NumberOfModels = car.Count() }).ToList();
    carsG.ForEach(car => Console.WriteLine($"Make: {car.Key} - Count: {car.NumberOfModels}"));
}

