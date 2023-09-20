using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orders.Application.DTO;
using Orders.Application.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class ProductClient : IProductClient
    {

        private readonly RestClient _client;
        private readonly ILogger<ProductClient> _logger;

        public ProductClient(ILogger<ProductClient> logger)
        {
            _client = new RestClient("https://localhost:7147");
            _logger = logger;
        }

        public async Task<bool> DecreaseStock(List<DecreaseStockDTO> decreases)
        {
            var request = new RestRequest("CatalogItem/DecreaseStock", Method.Post);
            request.AddJsonBody(decreases);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<ApiResponse>(response.Content!);
                var result = content!.Data;
                if (result == null)
                {
                    return false;
                }
                var flag = Convert.ToBoolean(result.ToString());
                return flag;
            }
            else
            {
                _logger.LogError($"调用商品服务扣减库存失败，错误信息：{response.ErrorMessage}");
                return false;
            }
        }
    }
}
