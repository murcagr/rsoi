using Microsoft.AspNetCore.Mvc;
using CatService.Model;
using CatService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;

namespace CatServiceTest
{
    public class CatServTests
    {
        private CatServ catServ;

        public CatServTests()
        {
            
            var catContextMock = new Mock<AppDBContext>(new DbContextOptions<AppDBContext>());

            List<Cat> cats = new List<Cat>
            {
                new Cat()
                {
                    Id = 1,
                    Name = "Olsi",
                    Breed = "Scottish",
                    FoodId = 1,
                    OwnerId = 1
                },
                new Cat()
                {
                    Id = 2,
                    Name = "Murzik",
                    Breed = "Dvor",
                    FoodId = 1,
                    OwnerId = 1
                }
            };


            catContextMock.Object.Cats = GetQueryableMockDbSet(cats);


            catServ = new CatServ(catContextMock.Object);
        }

        [Fact]
        public async void GetCats()
        {
            var result = await catServ.GetCatsAsync(0,10);
            List<Cat> cats = new List<Cat>
            {
                new Cat()
                {
                    Id = 1,
                    Name = "Olsi",
                    Breed = "Scottish",
                    FoodId = 1,
                    OwnerId = 1
                },
                new Cat()
                {
                    Id = 2,
                    Name = "Murzik",
                    Breed = "Dvor",
                    FoodId = 1,
                    OwnerId = 1
                }
            };

            var catsStr = JsonConvert.SerializeObject(cats);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(catsStr, resultStr);
        }


        [Fact]
        public async void GetCatsByOwner()
        {
            var result = await catServ.GetCatByOwnerIdAsync(1);
            List<Cat> cats = new List<Cat>
            {
                new Cat()
                {
                    Id = 1,
                    Name = "Olsi",
                    Breed = "Scottish",
                    FoodId = 1,
                    OwnerId = 1
                },
                new Cat()
                {
                    Id = 2,
                    Name = "Murzik",
                    Breed = "Dvor",
                    FoodId = 1,
                    OwnerId = 1
                }
            };

            var catsStr = JsonConvert.SerializeObject(cats);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(catsStr, resultStr);
        }


        [Fact]
        public async void DeleteCat()
        {
            var result = await catServ.DeleteCatById(1);
            var cat = new Cat()
            {
                Id = 1,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            };

            var catsStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(catsStr, resultStr);
        }

        [Fact]
        public async void AddCat()
        {
            var result = await catServ.AddCat(new Cat()
            {
                Id = 1,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            });

            var cat = new Cat()
            {
                Id = 1,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            };

            var catsStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(catsStr, resultStr);
        }

        [Fact]
        public async void GetCatById()
        {
            var result = await catServ.GetCatByIdAsync(2);
            var cat = new Cat()
            {
                Id = 2,
                Name = "Murzik",
                Breed = "Dvor",
                FoodId = 1,
                OwnerId = 1
            };

            var catsStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(catsStr, resultStr);
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