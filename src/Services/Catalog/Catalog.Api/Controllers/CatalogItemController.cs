using Catalog.Api.Application.DTO;
using Catalog.Api.Infrastructure;
using Catalog.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    /// <summary>
    /// 商品目录
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class CatalogItemController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly ILogger<CatalogItemController> _logger;

        public CatalogItemController(CatalogContext context, ILogger<CatalogItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            return await _context.CatalogItems.ToListAsync();
        }

        /// <summary>
        /// 根据ID获取商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<CatalogItem?> GetCatalogItemById(int id)
        {
            return await _context.CatalogItems.FindAsync(id);
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="catalogItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CatalogItem> CreateCatalogItem(CatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
            await _context.SaveChangesAsync();
            return catalogItem;
        }

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="decreases">扣减集合</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        public async Task<bool> DecreaseStock(List<DecreaseStockDTO> decreases)
        {
            if (decreases == null || !decreases.Any())
            {
                _logger.LogError("扣减库存的时候参数为空");
                throw new ArgumentNullException(nameof(decreases));
            }

            foreach (var decrease in decreases)
            {
                var catalogItem = await _context.CatalogItems.FindAsync(decrease.CatalogItemId);
                if (catalogItem == null)
                {
                    _logger.LogError("CatalogItem with id {catalogItemId} not found 商品没有找到", decrease.CatalogItemId);
                    return false;
                }
                // 判断库存是否足够
                if (catalogItem.Stock < decrease.Quantity)
                {
                    _logger.LogError("CatalogItem with id {catalogItemId} 库存不足", decrease.CatalogItemId);
                    return false;
                }

                catalogItem.Stock -= decrease.Quantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="catalogItemId">商品Id</param>
        /// <param name="quantity">商品数量</param>
        /// <returns></returns>
        [HttpPost]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<bool> DecreaseStock([FromForm] int catalogItemId, [FromForm] int quantity)
        {
            var catalogItem = await _context.CatalogItems.FindAsync(catalogItemId);
            if (catalogItem == null)
            {
                _logger.LogError("CatalogItem with id {catalogItemId} not found.", catalogItem);
                return false;
            }
            // 判断库存是否足够
            if (catalogItem.Stock < quantity)
            {
                return false;
            }

            catalogItem.Stock -= quantity;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
