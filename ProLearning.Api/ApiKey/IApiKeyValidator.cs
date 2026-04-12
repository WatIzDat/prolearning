namespace ProLearning.Api.ApiKey;

public interface IApiKeyValidator
{
    bool IsValid(string apiKey);
}