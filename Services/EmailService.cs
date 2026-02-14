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
        var apiKey = _config["Brevo:ApiKey"];
        var senderName = "Question Tracker";
        var senderEmail = _config["Brevo:SenderEmail"]; // Must be your login email

        var payload = new
        {
            sender = new { name = senderName, email = senderEmail },
            to = new[] { new { email = to } },
            subject = "Your Verification Code",
            htmlContent = $@"
                <h2>Your Verification Code</h2>
                <h1>{code}</h1>
                <p>Expires in 10 minutes.</p>"
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Add the special Brevo header
        if (!_httpClient.DefaultRequestHeaders.Contains("api-key"))
        {
            _httpClient.DefaultRequestHeaders.Add("api-key", apiKey);
        }

        var response = await _httpClient.PostAsync("https://api.brevo.com/v3/smtp/email", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Brevo API Failed: {error}");
        }
    }
}