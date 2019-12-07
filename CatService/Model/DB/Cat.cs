using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.Model
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Breed { get; set; }

        public int? OwnerId { get; set; }

        public int? FoodId { get; set; }

    }
}
  