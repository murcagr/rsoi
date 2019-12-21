using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Settings
{
    public class AuthService
    {
        public class ServiceCred
        {
            public int AppId { get; set; }
            public string AppSecret { get; set; }
        }
        public class AuthToService
        {
            public ServiceCred OwnerCred { get; set; } = new ServiceCred() { AppId = 1, AppSecret = "ownerApp" };
            public string OwnerToken { get; set; } = "";
            public ServiceCred CatCred { get; set; } = new ServiceCred() { AppId = 2, AppSecret = "catApp" };
            public string CatToken { get; set; } = "";
            public ServiceCred FoodCred { get; set; } = new ServiceCred() { AppId = 3, AppSecret = "foodApp" };
            public string FoodToken { get; set; } = "";
        }
    }
}
