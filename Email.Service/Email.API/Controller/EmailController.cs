using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Email.API
{
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _repository;
        private readonly IConfiguration _configuration;

        public EmailController(IEmailRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListEmails()
        {
            IEnumerable<EmailModel> result = await _repository.ListEmailsAsync();

            return base.Ok(result);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveEmail(string email)
        {
            EmailModel result = await _repository.SaveEmailAsync(email);

            return base.Ok(result);
        }

    }
}
