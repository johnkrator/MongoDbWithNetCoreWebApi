using Microsoft.AspNetCore.Mvc;
using MongoDbWithNetCoreWebApi.models;
using MongoDbWithNetCoreWebApi.services;

namespace MongoDbWithNetCoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] Driver driver)
    {
        await _driverService.CreateDriverAsync(driver);
        return CreatedAtAction(nameof(CreateDriver), new { id = driver.Id }, driver);
    }

    [HttpGet]
    public async Task<List<Driver>> GetAllDrivers()
    {
        return await _driverService.GetAllDriversAsync();
    }

    [HttpGet("{id}")]
    public async Task<Driver> GetDriverById(string id)
    {
        return await _driverService.GetDriverByIdAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver(string id, [FromBody] Driver driver)
    {
        var existingDriver = await _driverService.GetDriverByIdAsync(id);

        if (existingDriver is null)
            return NotFound();

        driver.Id = existingDriver.Id;

        await _driverService.UpdateDriverAsync(driver);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(string id)
    {
        await _driverService.DeleteDriverAsync(id);
        return Ok();
    }
}
