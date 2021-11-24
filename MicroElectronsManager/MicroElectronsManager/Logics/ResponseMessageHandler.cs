using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MicroElectronsManager.Models;

namespace MicroElectronsManager.Logics
{
    public static class ResponseMessageHandler
    {
        public static string GetMessage(string jsonMessage)
        {
            try
            {
                ResponseMessageData responseMessage = JsonSerializer.Deserialize<ResponseMessageData>(jsonMessage);
                return responseMessage.message;
            }
            catch
            {
                return "Not Found 404";
            }
        }
    }
}
