using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Storage
{
    public class TokenStorage
    {
        public List<string> activeTokens { get; set; } = new List<string>();
    }
}
