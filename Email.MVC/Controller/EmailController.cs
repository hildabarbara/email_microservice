using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Email.MVC
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task<IActionResult> ListEmails()
        {
            List<EmailModel> model = await emailService.ListEmailsAsync();

            return base.View(model);
        }

        public async Task<IActionResult> SaveEmail(EmailModel email)
        {
            EmailModel model = await emailService.SaveEmailAsync(email);

            return base.View(model);
        }
    }
}