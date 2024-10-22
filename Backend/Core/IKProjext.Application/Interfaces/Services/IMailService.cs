using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.Services
{
    public interface IMailService
    {
        //Task SendMailAsync(string to,string subject, string body,bool isBodyHtml=true);
        //Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);

        //Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
        

        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
        Task SendVerificationCodeAsync(string to, string code);
        Task SendRandomPasswordAsync(string to, string password);
    }
}
