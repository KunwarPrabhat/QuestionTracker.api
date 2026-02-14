using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendVerificationCode(string to, string code)
    {
        var email = new MimeMessage();
        // IMPORTANT: The "From" email MUST be the email you used to sign up for Brevo
        email.From.Add(new MailboxAddress("Question Tracker", _config["Smtp:User"]));
        email.To.Add(new MailboxAddress("", to));
        email.Subject = "Your Verification Code";

        email.Body = new TextPart("html")
        {
            Text = $@"
                <h2>Your Verification Code</h2>
                <h1>{code}</h1>
                <p>Expires in 10 minutes.</p>"
        };

        using var client = new SmtpClient();
        
        // Brevo works best on Port 587 with StartTls
        await client.ConnectAsync(
            _config["Smtp:Host"], 
            int.Parse(_config["Smtp:Port"] ?? "587"), 
            MailKit.Security.SecureSocketOptions.StartTls
        );
        
        await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Pass"]);
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
    }
}