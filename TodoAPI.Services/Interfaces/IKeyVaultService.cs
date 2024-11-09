namespace TodoAPI.Services.Interfaces;

public interface IKeyVaultService
{ 
    Task<string> GetKeyVaultSecret(string key);
}