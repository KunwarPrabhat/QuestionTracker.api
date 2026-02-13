using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public EmailService(IConfiguration config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task SendVerificationCode(string to, string code)
    {
        var apiKey = _config["RESEND_API_KEY"];

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var body = new
        {
            from = "Question Tracker <onboarding@resend.dev>", 
            to = to,
            subject = "Your Verification Code",
            html = $@"
                <h2>Your Verification Code</h2>
                <p>Your verification code is:</p>
                <h1>{code}</h1>
                <p>This code expires in 10 minutes.</p>
            "
        };

        var content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            "https://api.resend.com/emails",
            content
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Email sending failed: {error}");
        }
    }
}