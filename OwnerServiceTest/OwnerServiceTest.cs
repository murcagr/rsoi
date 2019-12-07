using Microsoft.AspNetCore.Mvc;
using OwnerService.Model;
using OwnerService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;

namespace OwnerServiceTest
{
    public class OwnerServTests
    {
        private OwnerServ ownerServ;

        public OwnerServTests()
        {
            
            var ownerContextMock = new Mock<AppDBContext>(new DbContextOptions<AppDBContext>());

            List<Owner> owners = new List<Owner>
            {
                  new Owner()
                  {
                      Id = 1,
                      Name = "Artem",
                      Age = 25,
                      City = "Moscow"
                  },
                  new Owner()
                  {
                      Id = 2,
                      Name = "Vasya",
                      Age = 30,
                      City = "Moscow"
                  }
            };


            ownerContextMock.Object.Owners = GetQueryableMockDbSet(owners);


            ownerServ = new OwnerServ(ownerContextMock.Object);
        }

        [Fact]
        public async void GetOwners()
        {
            var result = await ownerServ.GetOwners();
            List<Owner> owners = new List<Owner>
            {
                  new Owner()
                  {
                      Id = 1,
                      Name = "Artem",
                      Age = 25,
                      City = "Moscow"
                  },
                  new Owner()
                  {
                      Id = 2,
                      Name = "Vasya",
                      Age = 30,
                      City = "Moscow"
                  }
            };

            var ownersStr = JsonConvert.SerializeObject(owners);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(ownersStr, resultStr);
        }

        [Fact]
        public async void DeleteOwner()
        {
            var result = await ownerServ.DeleteOwnerByID(1);
            var owner = new Owner()
            {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            };

            var ownersStr = JsonConvert.SerializeObject(owner);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(ownersStr, resultStr);
        }

        [Fact]
        public async void GetOwnerById()
        {
            var result = await ownerServ.GetOwnerByIdAsync(1);
            var owner = new Owner()
            {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            };

            var ownersStr = JsonConvert.SerializeObject(owner);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(ownersStr, resultStr);
        }



        private DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}