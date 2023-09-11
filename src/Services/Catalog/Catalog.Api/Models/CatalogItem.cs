using System;
using System.Collections.Generic;

namespace Catalog.Api.Models
{
    public partial class CatalogItem
    {
        public int Id { get; set; }
        /// <summary>
        /// 商品名字
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int? Stock { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
