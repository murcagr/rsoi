using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }

        public int? OwnerId { get; set; }

        public int? FoodId { get; set; }

    }
}
