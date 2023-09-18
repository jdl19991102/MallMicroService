using Orders.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Interfaces
{
    public interface IProductClient
    {
        /// <summary>
        /// 调用商品服务扣减库存
        /// </summary>
        /// <param name="catalogItemId">商品Id</param>
        /// <param name="quantity">商品数量</param>
        /// <returns></returns>
        //Task<bool> DecreaseStock(int catalogItemId, int quantity);

        /// <summary>
        /// 调用商品服务扣减库存
        /// </summary>
        /// <param name="decreases"></param>
        /// <returns></returns>
        Task<bool> DecreaseStock(List<DecreaseStockDTO> decreases);
    }
}
