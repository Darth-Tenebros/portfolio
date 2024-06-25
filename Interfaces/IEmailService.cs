using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfolio.Interfaces
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string message, string key);
    }
}
