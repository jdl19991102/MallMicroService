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

        public CatalogItemController(CatalogContext context)
        {
            _context = context;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
    }
}
