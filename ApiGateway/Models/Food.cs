using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class Food
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Doze { get; set; }

    }
}
