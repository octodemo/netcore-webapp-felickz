public class MyApiService
{
    private readonly ApiSettings _apiSettings;

    public MyApiService(IOptions<ApiSettings> apiSettings)
    {
        _apiSettings = apiSettings.Value;
    }

    public async Task<string> GetApiDataAsync()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiSettings.BearerToken);

            var response = await client.GetAsync("endpoint");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
