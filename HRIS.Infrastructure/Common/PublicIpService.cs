using HRIS.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Common
{
    public class PublicIpService : IPublicIpService
    {
        private readonly HttpClient _httpClient;

        public PublicIpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetPublicIpAsync()
        {
            return await _httpClient.GetStringAsync("https://api.ipify.org");
        }
    }
}
