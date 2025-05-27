using System.Text;
using System.Text.Json;

public class AuthApiService
{
    private readonly HttpClient _httpClient;
    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(string userName, string password)
    {
        var loginModel = new
        {
            userName,
            password
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(loginModel),
            Encoding.UTF8,
            "application/json"
        );
        //TODO: url ჩემი აპის შემთხვევაში
        var response = await _httpClient.PostAsync("https://localhost:7010/api/auth/login", jsonContent);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(responseContent);

        return json.GetProperty("accessToken").GetString();
    }
}
