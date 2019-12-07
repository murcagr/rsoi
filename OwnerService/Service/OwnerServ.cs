using Newtonsoft.Json;
using OwnerService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OwnerService.Services
{
    public class OwnerServ : IOwnerServ
    {
        private readonly ILogger<OwnerServ> _logger;
        private readonly AppDBContext _AppDBContext;

        public OwnerServ(AppDBContext appDBContext)
        {
            _AppDBContext = appDBContext;
        }

        public async Task<Owner> AddOwner(Owner owner)
        {


            _AppDBContext.Owners.Add(owner);
            await _AppDBContext.SaveChangesAsync();

            return owner;

        }

        public async Task<IEnumerable<Owner>> GetOwners()
        {
            var owner = _AppDBContext.Owners.ToList();

            return owner;
        }



        public async Task<Owner> DeleteOwnerByID(int id)
        {

            var owner = _AppDBContext.Owners.SingleOrDefault(c => c.Id == id);
            if (owner != null)
            {
                _AppDBContext.Owners.Remove(owner);
                await _AppDBContext.SaveChangesAsync();
                return owner;
            }

            return null;
            //return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            var owner = _AppDBContext.Owners.Where(m => m.Id == id).SingleOrDefault();

            return owner;
        }
    }
}
