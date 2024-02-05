using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DiscountResponse
    {
        public int Id { get; set; }
        public int Discount { get; set; }
    }
}
