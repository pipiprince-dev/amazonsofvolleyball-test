namespace Backend.Infrastructure.Helpers;

public static class EmailSender
{
    private const string supportEmail = "support@amazonsofvolleyball.com";

    public static void SendEmail(string email, string subject, string message)
    {
        var client = SmtpClientConfiguration();

        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(supportEmail);
        mailMessage.To.Add(email);
        mailMessage.Body = message;
        mailMessage.Subject = subject;
        mailMessage.IsBodyHtml = true;

        client.Send(mailMessage);
    }

    public static async void SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var client = SmtpClientConfiguration();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(supportEmail);
            mailMessage.Body = message;
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            await client.SendMailAsync(mailMessage);
        }
        catch (SmtpException ex)
        {
            throw new Exception($"SMTP exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Send mail exception: {ex.Message}");
        }
    }

    public static async void SendEmailAsync(string email, string subject, string message, byte[] data)
    {
        try
        {
            var client = SmtpClientConfiguration();

            var attachment = new MemoryStream(data);

            // Create  the file attachment for this e-mail message.
            var pdfAttachment = new Attachment(attachment, "dokumentum.pdf", "application/pdf");

            // Add time stamp information for the file.
            var disposition = pdfAttachment.ContentDisposition;
            disposition.CreationDate = DateTime.Now;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(supportEmail);
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            // Add the file attachment to this e-mail message.
            mailMessage.Attachments.Add(pdfAttachment);

            await client.SendMailAsync(mailMessage);
        }
        catch (SmtpException ex)
        {
            throw new Exception($"SMTP exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Send mail exception: {ex.Message}");
        }
    }

    private static SmtpClient SmtpClientConfiguration()
    {
        var client = new SmtpClient(supportEmail);
        client.Port = 587;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(supportEmail, "YT@5j$QC@#-3");
        client.EnableSsl = true;

        return client;
    }
}
