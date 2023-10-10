// See https://aka.ms/new-console-template for more information

using MongoDB.Driver;

// PoC połączenia z MongoDB

MongoClient dbClient = new MongoClient("mongodb://localhost:27017");

var dbList = dbClient.ListDatabases().ToList();

Console.WriteLine("The list of databases on this server is: ");
foreach (var db in dbList)
{
	Console.WriteLine(db);
}

Console.WriteLine("Press enter to exit.");
Console.ReadLine();