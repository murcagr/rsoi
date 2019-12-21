using ApiGateway.Clients;
using ApiGateway.Exceptions;
using ApiGateway.Models;
using ApiGateway.Queue;
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
        private readonly CatQueue _catQueue;
        private readonly OwnerQueue _ownerQueue;

        public GwService(IOwnerClient ownerClient, ICatClient catClient, 
            IFoodClient foodClient, CatQueue catQueue, OwnerQueue ownerQueue)
        {
            _ownerClient = ownerClient;
            _catClient = catClient;
            _foodClient = foodClient;
            _ownerQueue = ownerQueue;
            _catQueue = catQueue;

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

            try
            {
                ownerCats.Cat.OwnerId = ownerResp.Id;
                var catResp = await _catClient.AddNewCat(ownerCats.Cat);
                var ownercatResp = new OwnerCat(catResp, ownerResp);
                return ownercatResp;
            }
            catch (InternalException e)
            {
                ownerResp = await _ownerClient.DeleteOwner(ownerResp.Id);
                throw e;
            }
            catch (RequestException e)
            {
                ownerResp = await _ownerClient.DeleteOwner(ownerResp.Id);
                throw e;
            }

        }

        public async Task<CatOwnerFood> AddFoodOwnerCat(CatOwnerFood cof)
        {
            cof.Cat.OwnerId = null;
            cof.Cat.FoodId = null;
            Food foodResp = new Food();
            Owner ownerResp = new Owner();

            ownerResp = await _ownerClient.AddOwner(cof.Owner);
            cof.Cat.OwnerId = ownerResp.Id;
           
            foodResp = await _foodClient.AddNewFood(cof.Food);
            cof.Cat.FoodId = foodResp.Id;
           
            var catResp = await _catClient.AddNewCat(cof.Cat);

            var catownerfoodResp = new CatOwnerFood(catResp, ownerResp, foodResp);

            return catownerfoodResp;
        }

        public async Task<IEnumerable<CatOwnerFood>> GetCatOwnerFood(int size, int pageSize)
        {
            var catResp = await _catClient.GetCats(size, pageSize);
            var catList = catResp.ToList();

            List<CatOwnerFood> catOwnerFoods = new List<CatOwnerFood>();
            for (int i = 0; i < catResp.Count(); i++)
            {
                Owner ownerResp = null;
                Food foodResp = null;

                if (catList[i].OwnerId != null)
                {
                    var ownerID = catList[i].OwnerId ?? default(int);
                    try
                    {
                        ownerResp = await _ownerClient.GetOwnerByIdAsync(ownerID);
                    }
                    catch (InternalException)
                    {
                        ownerResp = new Owner()
                        {
                            Id = ownerID,
                            Name = "Degraded",
                            Age = null,
                            City = null
                        };
                    }
                    catch (RequestException)
                    {
                        ownerResp = new Owner()
                        {
                            Id = ownerID,
                            Name = "Degraded",
                            Age = null,
                            City = null
                        };
                    }
                    catch (Exception)
                    {
                        ownerResp = new Owner()
                        {
                            Id = ownerID,
                            Name = "Degraded",
                            Age = null,
                            City = null
                        };
                    }
                    
                }
                

                if (catList[i].FoodId != null)
                {
                    var FoodID = catList[i].FoodId?? default(int);
                    try
                    {
                        foodResp = await _foodClient.GetFoodByIdAsync(FoodID);
                    }
                    catch (InternalException)
                    {
                        foodResp = new Food()
                        {
                            Id = FoodID,
                            Name = "Degraded",
                            Producer = null,
                            Doze = null
                        };
                    }
                    catch (RequestException)
                    {
                        foodResp = new Food()
                        {
                            Id = FoodID,
                            Name = "Degraded",
                            Producer = null,
                            Doze = null
                        };
                    }
                    catch (Exception)
                    {
                        foodResp = new Food()
                        {
                            Id = FoodID,
                            Name = "Degraded",
                            Producer = null,
                            Doze = null
                        };
                    }
                }
                
                catOwnerFoods.Add(new CatOwnerFood(catList[i], ownerResp, foodResp));
            }

            return catOwnerFoods;
        }

        public async Task<OwnerCats> DeleteOwnerandHisCats(int id)
        {
            Owner ownerResp = new Owner();
            IEnumerable<Cat> catResp = new List<Cat>();

            try
            {
                ownerResp = await _ownerClient.DeleteOwner(id);
            }
            catch
            {
                _ownerQueue.OwnerDeleteQueueTasks.Enqueue(id);
            }

            try
            {
                catResp = await _catClient.DeleteCatsByOwnerIdAsync(id);
            }
            catch
            {
                _catQueue.CatDeleteQueueTasks.Enqueue(id);
            }

            var resp = new OwnerCats(catResp, ownerResp);

            return resp;
        }

        public async Task<Owner> AttachCatToOwner(int id)
        {
            var resp = await _ownerClient.GetOwnerByIdAsync(id);

            return resp;
        }

    }
}
