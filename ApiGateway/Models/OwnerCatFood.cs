using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class CatOwnerFood
    {

        public Cat Cat { get; set; }
        public Owner Owner { get; set; }

        public Food Food { get; set; }

        public CatOwnerFood(Cat cat, Owner owner, Food food)
        {
            Cat = cat;
            Owner = owner;
            Food = food;
        }
    }
}
