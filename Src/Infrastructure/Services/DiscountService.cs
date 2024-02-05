using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int?> GetDiscountByProductId(int productId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                int? discount;
                string baseUrl = _configuration.GetValue<string>("DiscountServiceUrl");
                //string url = $"https://65c03c0d25a83926ab961a81.mockapi.io/api/discount/{productId}";

                string url = $"{baseUrl}/api/discount/{productId}";

                DiscountResponse? response = await httpClient.GetFromJsonAsync<DiscountResponse>(url);
                //response.EnsureSuccessStatusCode();

                if (response == null)
                    discount = (int?)null;
                else
                    discount = response.Discount;

                return discount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}
