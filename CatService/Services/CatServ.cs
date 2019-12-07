using CatService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.Services
{
    public class CatServ : ICatServ
    {
        private readonly AppDBContext _AppDBContext;
        public CatServ(AppDBContext appDBContext)
        {
            _AppDBContext = appDBContext;
        }



        public async Task<IEnumerable<Cat>> GetCatsAsync(int page, int pageSize)
        {
            List<Cat> cats = _AppDBContext.Cats.ToList();
            

            return cats.Skip(page * pageSize).Take(pageSize);
        }

        public async Task<Cat> AddCat(Cat cat)
        {
            var resp = _AppDBContext.Cats.Add(cat);
            await _AppDBContext.SaveChangesAsync();

            return cat;

        }

        public async Task<Cat> DeleteCatById(int id)
        {

            var cat = _AppDBContext.Cats.SingleOrDefault(c => c.Id == id);
            if (cat != null)
            {
                _AppDBContext.Cats.Remove(cat);
                await _AppDBContext.SaveChangesAsync();
                return cat;
            }

            return null;
            //return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        public async Task<Cat> GetCatByIdAsync(int id)
        {
            var cat = _AppDBContext.Cats.SingleOrDefault(m => m.Id == id);

            return cat;
        }


        public async Task<IEnumerable<Cat>> GetCatByOwnerIdAsync(int ownerId)
        {
            var cat = _AppDBContext.Cats.Where(m => m.OwnerId == ownerId).ToList();

            return cat;
        }
    }
}
