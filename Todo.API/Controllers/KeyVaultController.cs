using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services.Interfaces;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class KeyVaultController : ControllerBase
{
    private readonly IKeyVaultService _keyVaultService;

    public KeyVaultController(IKeyVaultService keyVaultService)
    {
        _keyVaultService = keyVaultService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var secretValue = await _keyVaultService.GetKeyVaultSecret("ConnectionString");
        return Ok(secretValue);
    }
    
}