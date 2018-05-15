using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Learning.Basic.Services.Interfaces
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }
}
