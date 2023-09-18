using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.API
{
    public interface IEmailRepository
    {
        Task<EmailModel> SaveEmailAsync(string email);

        Task<IEnumerable<EmailModel>> ListEmailsAsync();
    }
}
