using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services.Concrete;

public class KeyVaultService : IKeyVaultService
{
    private readonly IConfiguration _configuration;

    public KeyVaultService(
        IConfiguration  configuration)
    {
        _configuration = configuration;
    }
    
    
    public async Task<string> GetKeyVaultSecret(string key)
    {
        try
        {
            string keyVaultUrl = _configuration.GetSection("Azure:KeyVaultUrl").Value!;
        
            // Use DefaultAzureCredential to authenticate using Managed Identity
            var client = new SecretClient(
                new Uri(keyVaultUrl), new DefaultAzureCredential());

            // Replace with the name of the secret you want to access
            string secretName = "ConnectionString";
            
            // Retrieve the secret
            KeyVaultSecret secret =  await client.GetSecretAsync(secretName);

            return secret.Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}