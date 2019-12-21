using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class TokenM
    {
        public string Token { get; set; }
        public int Exp_in { get; set; }
    }
}
