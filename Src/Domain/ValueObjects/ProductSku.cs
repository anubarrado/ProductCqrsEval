using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial record ProductSku
    {
        private const int DefaultLenght = 10;
        private const string Pattern = @"[a-zA-Z]{4}\d{6}";
        public string Value { get; init; }

        private ProductSku(string value) => Value = value;

        public static ProductSku? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !PhoneSkuRegex().IsMatch(value) || value.Length != DefaultLenght)
            {
                return null;
            }

            return new ProductSku(value);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneSkuRegex();
    }
}
