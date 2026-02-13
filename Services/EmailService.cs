using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendVerificationCode(string to, string code)
    {
        var msg = new MailMessage();
        msg.From = new MailAddress(_config["Smtp:User"]!, "Question Tracker");
        msg.To.Add(to);
        msg.Subject = "Your Verification Code";
        msg.Body = $"Your verification code is: {code}\n\nIt expires in 10 minutes.";

        using var client = new SmtpClient(
            _config["Smtp:Host"],
            int.Parse(_config["Smtp:Port"]!)
        )
        {
            Credentials = new NetworkCredential(
                _config["Smtp:User"],
                _config["Smtp:Pass"]
            ),
            EnableSsl = true
        };

        await client.SendMailAsync(msg);
    }
}
