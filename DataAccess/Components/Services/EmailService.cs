using DataAccess.Components.Interface;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Components.Services
{
    internal class EmailService : IEmailService
    {
        private readonly SendGridClient _sendGridClient;

        public EmailService()
        {
            
        }
        public Task SendEmailAsync(Task task)
        {
            return null;
        }
    }
}
