using ApiGateway.Clients;
using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Services
{
    public class GwService
    {
        private readonly IOwnerClient _ownerClient;
        private readonly ICatClient _catClient;
        private readonly IFoodClient _foodClient;

        public GwService(IOwnerClient ownerClient, ICatClient catClient, IFoodClient foodClient)
        {
            _ownerClient = ownerClient;
            _catClient = catClient;
            _foodClient = foodClient;

        }

        // CATS

        public async Task<IEnumerable<Cat>> GetCatsByOwnerIdAsync(int ownerId)
        {
            var resp = await _catClient.GetCatsByOwnerIdAsync(ownerId);

            return resp;
        }


        public async Task<IEnumerable<Cat>> GetCats()
        {
            var resp = await _catClient.GetCats();

            return resp;
        }

        public async Task<Cat> GetCatsByIdAsync(int id)
        {
            var resp = await _catClient.GetCatByIdAsync(id);

            return resp;
        }

        public async Task<Cat> AddCat(Cat cat)
        {
            var resp = await _catClient.AddNewCat(cat);

            return resp;
        }

        public async Task<bool> DeleteCat(int id)
        {
            var resp = await _catClient.DeleteCat(id);

            return resp;
        }

        // OWNERS


        public async Task<IEnumerable<Owner>> GetOwners()
        {
            var resp = await _ownerClient.GetOwners();

            return resp;
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {

            var resp = await _ownerClient.GetOwnerByIdAsync(id);

            return resp;
        }

        public async Task<Owner> AddUser(Owner owner)
        {
            var resp = await _ownerClient.AddOwner(owner);

            return resp;
        }

        // Food

        public async Task<IEnumerable<Food>> GetFood()
        {
            var resp = await _foodClient.GetFoods();

            return resp;
        }

        public async Task<bool> DeleteFood(int id)
        {

            var resp = await _foodClient.DeleteFood(id);

            return resp;
        }

        public async Task<Food> GetFoodById(int id)
        {
            var resp = await _foodClient.GetFoodByIdAsync(id);

            return resp;
        }

        public async Task<OwnerCats> GetOwnerAndCats(int id)
       {
            var ownerResp = await _ownerClient.GetOwnerByIdAsync(id);
            var catResp = await _catClient.GetCatsByOwnerIdAsync(id);
            OwnerCats ownerCats = new OwnerCats(catResp, ownerResp);

            return ownerCats;
       }

       public async Task<OwnerCat> AddOwnerAndCat(OwnerCat ownerCats)
       {
            
            var ownerResp = await _ownerClient.AddOwner(ownerCats.Owner);
            ownerCats.Cat.OwnerId = ownerResp.Id;
            var catResp = await _catClient.AddNewCat(ownerCats.Cat);

            var ownercatResp = new OwnerCat(catResp, ownerResp);

            return ownercatResp;
       }

        public async Task<CatOwnerFood> AddFoodOwnerCat(CatOwnerFood cof)
        {

            var ownerResp = await _ownerClient.AddOwner(cof.Owner);
            var foodResp = await _foodClient.AddNewFood(cof.Food);

            cof.Cat.OwnerId = ownerResp.Id;
            cof.Cat.FoodId = foodResp.Id;

            var catResp = await _catClient.AddNewCat(cof.Cat);

            var catownerfoodResp = new CatOwnerFood(catResp, ownerResp, foodResp);

            return catownerfoodResp;
        }


        public async Task<Owner> AttachCatToOwner(int id)
        {
            var resp = await _ownerClient.GetOwnerByIdAsync(id);

            return resp;
        }

    }
}
