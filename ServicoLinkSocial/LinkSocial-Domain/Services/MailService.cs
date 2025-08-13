using MimeKit;

namespace LinkSocial_Domain.Services
{
    public class MailService
    {
        public async Task SendEmailAsync(
      string fromEmail,
      string toEmail,
      string subject,
      string htmlBody,
      string pdfFilePath,
      string appPassword)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            // Create the body with HTML
            var builder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            // Attach PDF
            if (!string.IsNullOrEmpty(pdfFilePath) && File.Exists(pdfFilePath))
            {
                builder.Attachments.Add(pdfFilePath);
            }

            message.Body = builder.ToMessageBody();

            //using var smtp = new SmtpClient();
            //await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //await smtp.AuthenticateAsync(fromEmail, appPassword);
            //await smtp.SendAsync(message);
            //await smtp.DisconnectAsync(true);
        }
    }
}
