using MailKit.Net.Smtp; // IMPORTANT: Use MailKit, not System.Net.Mail
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
        // connect to smtp.gmail.com on port 587
        await client.ConnectAsync(_config["Smtp:Host"], _config.GetValue<int>("Smtp:Port"), MailKit.Security.SecureSocketOptions.StartTls);
        
        // authenticate
        await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Pass"]);
        
        // send
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
    }
}