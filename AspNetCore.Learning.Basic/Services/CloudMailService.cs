using AspNetCore.Learning.Basic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Learning.Basic.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "developer@qq.com";
        private string _mailFrom = "noreply@qq.com";

        public void Send(string subject, string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(CloudMailService)}发送了邮件");
        }
    }
}
