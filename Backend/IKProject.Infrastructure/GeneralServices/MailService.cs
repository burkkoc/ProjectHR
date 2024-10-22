using IKProject.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IKProject.Infrastructure.GeneralServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body + GetEmailSignature();
                message.IsBodyHtml = isBodyHtml;
                message.From = new MailAddress(_configuration["Mail:Username"], "IK BURADA");

                using (var client = new SmtpClient())
                {
                    client.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                    client.Host = _configuration["Mail:Host"];
                    client.Port = int.Parse(_configuration["Mail:Port"]);
                    client.EnableSsl = true;

                    try
                    {
                        await client.SendMailAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Email gönderim hatası: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            var url = $"{_configuration["ReactClientUrl"]}/reset-password?userId={userId}&token={resetToken}";
            var body = $"Please reset your password by clicking <a href=\"{url}\">here</a>";
            await SendMailAsync(to, "Password Reset", body);
        }

        public async Task SendVerificationCodeAsync(string to, string code)
        {
            var body = $"Doğrulama kodunuz: {code}. Lütfen bu kodu kullanarak parolanızı değiştiriniz.";
            await SendMailAsync(to, "IKBurda: Parola Resetleme Kodu", body);
        }

        public async Task SendRandomPasswordAsync(string to, string password)
        {
            var body = $"Hoş geldiniz!\r\n Bu parolayı kullanarak sisteme giriş yapabilirsiniz: {password}\r\n";
            await SendMailAsync(to, "IKBurda: Hoş geldiniz!", body);
        }

        public static string GetEmailSignature()
        {
            return @"
               <!DOCTYPE html>
               <html lang='en'>
               <head>
                 <meta charset='UTF-8'>
                 <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                 <style>
                   .email-signature {
                     font-family: Arial, sans-serif;
                     color: #333;
                     font-size: 11px;
                   }
                   .signature-table {
                     border-collapse: collapse;
                   }
                   .signature-table td {
                     padding: 3px; 
                     vertical-align: top;
                   }
                   .signature-info {
                     padding-left: 12px;
                   }
                   .signature-info p {
                     margin: 0;
                     line-height: 1.2;
                   }
                   .signature-info p.name {
                     font-size: 10px;
                     font-weight: bold;
                     color: #7286D3;
                   }
                   .signature-info p.title {
                     font-size: 10px;
                     color: #666;
                   }
                   .signature-info p.slogan {
                     font-size: 10px;
                     font-style: sans-serif;
                     color: #8EA7E9;
                   }
                   .signature-info p.contact {
                     color: #7286D3;
                     font-size: 10px;
                   }
                   .signature-info a {
                     text-decoration: none;
                     color: #7286D3;
                   }
                   .signature-info a:hover {
                     color: #8EA7E9;
                   }
                 </style>
               </head>
               <body>
                 <div class='email-signature'>
                   <table class='signature-table'>
                     <tr>
                       <td class='signature-info'>
                         <p class='name'>IK BURADA</p>
                         <p class='title'>İnsan Kaynakları Yönetimi</p>
                         <p class='slogan'>Kolay çözümler, Uzman görüşler için IK BURADA</p>
                         <p class='contact'>+90 123 456 7895</p>
                         <p class='contact'>Ankara, Turkey</p>
                         <p class='contact'><a href='https://projetakim2.azurewebsites.net'>ikprojesitakimiki.azurewebsites.net</a></p>
                       </td>
                     </tr>
                   </table>
                 </div>
               </body>
               </html>";
        }
    }
}
