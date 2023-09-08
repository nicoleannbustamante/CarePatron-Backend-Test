using Application.Services.Interfaces;
using Core.Exceptions;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<EmailService> _logger; 
        public EmailService(IEmailRepository emailRepository, ILogger<EmailService> logger) 
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task Send(string email, string message)
        {
            try
            {
                await _emailRepository.Send(email, message);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in sending email: " + ex.Message);
                throw new EmailException("Error in sending email:", ex);
            }
        }
    }
}
