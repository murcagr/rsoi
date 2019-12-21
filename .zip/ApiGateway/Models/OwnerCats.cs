using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class OwnerCats
    {

        public IEnumerable<Cat> Cats { get; set; }
        public Owner Owner { get; set; }

        public OwnerCats(IEnumerable<Cat> cats, Owner owner)
        {
            Cats = cats;
            Owner = owner;
        }
    }
}
