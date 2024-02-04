using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial record ProductPrice
    {
        private const decimal MinValue = 0;
        public decimal Value { get; init; }
        
        private ProductPrice(decimal value) => Value = value;

        public static ProductPrice? Create(decimal value)
        {
            if (value < MinValue)
                return null;
            return new ProductPrice(value);
        }
    }
}
