using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_ServiceNetCore7.Application.Interfaces
{
    public interface IEmail
    {
        void SendEmail(string to, string subject, string body);
    }
}
