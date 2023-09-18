using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.MVC
{
    public interface IEmailService
    {
        Task<List<EmailModel>> ListEmailsAsync();

        Task<EmailModel> SaveEmailAsync(EmailModel model);
    }
}
