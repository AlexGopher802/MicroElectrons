using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MicroElectronsManager.Models
{
    public class ResponseMessageData
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
