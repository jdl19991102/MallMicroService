using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.DTO
{
    public class DecreaseStockDTO
    {
        public int CatalogItemId { get; set; }
        public int Quantity { get; set; }
    }
}
