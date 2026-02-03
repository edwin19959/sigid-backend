using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int StockMin { get; set; }
        public int CurrStock { get; set; }
        public string Status { get; set; }
    }
}
