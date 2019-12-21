using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class OwnerCat
    {

        public Cat Cat { get; set; }
        public Owner Owner { get; set; }

        public OwnerCat(Cat cat, Owner owner)
        {
            Cat = cat;
            Owner = owner;
        }
    }
}
