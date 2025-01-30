using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timezones.API.Shared.Models
{
    public class ErrorMessage
    {
        public string code { get; set; } = "000";
        public string? error_message { get; set; }

        public ErrorMessage()
        {
        }

        public ErrorMessage(string? message)
        {
            error_message = message;
        }
        public ErrorMessage(string code, string message)
        {
            this.code = code;
            error_message = message;
        }
    }
}