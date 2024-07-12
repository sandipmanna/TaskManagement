using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Components.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(Task task);
    }
}
