using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timezones.API.Shared.Enums;
using Timezones.API.Shared.Models;

namespace Timezones.API.Shared.Handlers
{

    public record HandlerResponse
    {
        /// <summary>
        /// Status da requisição
        /// </summary>
        public StatusResponseEnum Status { get; set; }
        /// <summary>
        /// Http status code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Lista de mensagens
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();
        /// <summary>
        /// Indica o sucesso da requisição
        /// </summary>
        public bool IsSuccess { get => StatusCode >= 200 && StatusCode <= 299; }
        /// <summary>
        /// Indica falha de validação na requição
        /// </summary>
        public bool IsBadRequest { get => StatusCode >= 400 && StatusCode < 500; }
    }

    public record HandlerResponse<T> : HandlerResponse
    {
        public List<T>? Result { get; set; }
    }
}