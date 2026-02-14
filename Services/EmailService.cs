using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security; // <--- ADD THIS NAMESPACE

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
        
        // SAFETY: Force IPv4 to prevent the "Network Unreachable" error
        client.LocalDomain = "127.0.0.1";

        // CRITICAL CHANGE: Use Port 465 and SslOnConnect
        // This skips the "upgrade" handshake that is timing out
        await client.ConnectAsync(
            _config["Smtp:Host"], 
            465, 
            SecureSocketOptions.SslOnConnect
        );
        
        await client.AuthenticateAsync(_config["Smtp:User"], _config["Smtp:Pass"]);
        await client.SendAsync(email);
        await client.DisconnectAsync(true);
    }
}