using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealisticDataGenerationWithBogus
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public BillingDetails BillingDetails { get; set; }

    }
}
