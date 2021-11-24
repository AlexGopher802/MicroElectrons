using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MicroElectronsManager.Logics
{
    class ApiBuilder
    {
        private static string rootUrl = "http://gopher-server.xyz:49158/api/";
        private static RestClient restClient;

        public static RestClient GetInstance()
        {
            if (restClient == null)
            {
                restClient = new RestClient(rootUrl);
            }

            return restClient;
        }
    }
}
