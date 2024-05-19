using System;
using petConnection.FrontEnd.Shared.Responses;

namespace petConnection.Backend.Helpers
{
    public interface IMailHelper
    {
        ActionResponse<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}

