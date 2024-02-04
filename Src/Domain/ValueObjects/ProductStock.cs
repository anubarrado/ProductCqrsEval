using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial record ProductStock
    {
        private const int MinValue = 0;
        public int Value { get; init; }
        
        private ProductStock(int value) => Value = value;

        public static ProductStock? Create(int value)
        {
            if (value < MinValue)
                return null;
            return new ProductStock(value);
        }
    }
}
