using Microsoft.Extensions.Logging;
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

        //public async Task<bool> DecreaseStock(int catalogItemId, int quantity)
        //{
        //    var request = new RestRequest($"/CatalogItem/DecreaseStock", Method.Post);
        //    // 发送一个请求格式为multipart/form-data的请求
        //    request.AddHeader("Content-Type", "multipart/form-data");
        //    request.AddParameter("catalogItemId", catalogItemId);
        //    request.AddParameter("quantity", quantity);

        //    var response = await _client.ExecuteAsync(request);
        //    if (response.IsSuccessful)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        _logger.LogError($"调用商品服务扣减库存失败，错误信息：{response.ErrorMessage}");
        //        return false;
        //    }
        //}

        public async Task<bool> DecreaseStock(List<DecreaseStockDTO> decreases)
        {
            var request = new RestRequest($"/CatalogItem/DecreaseStock", Method.Post);
            // 发送Json格式的请求
            request.AddJsonBody(decreases);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            else
            {
                _logger.LogError($"调用商品服务扣减库存失败，错误信息：{response.ErrorMessage}");
                return false;
            }
        }
    }
}
