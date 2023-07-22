using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbWithNetCoreWebApi.configuration;
using MongoDbWithNetCoreWebApi.models;

namespace MongoDbWithNetCoreWebApi.services;

public class DriverService
{
    private readonly IMongoCollection<Driver> _driverCollection;

    public DriverService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _driverCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<Driver>> GetAllDriversAsync()
    {
        return await _driverCollection.Find(driver => true).ToListAsync();
    }

    public async Task<Driver> GetDriverByIdAsync(string id)
    {
        return await _driverCollection.Find(driver => driver.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Driver> CreateDriverAsync(Driver driver)
    {
        await _driverCollection.InsertOneAsync(driver);
        return driver;
    }

    public async Task<Driver> UpdateDriverAsync(Driver driver)
    {
        await _driverCollection.ReplaceOneAsync(d => d.Id == driver.Id, driver);
        return driver;
    }

    public async Task<Driver> DeleteDriverAsync(string id)
    {
        var driver = await GetDriverByIdAsync(id);
        await _driverCollection.DeleteOneAsync(driver => driver.Id == id);
        return driver;
    }
}
