using Microsoft.AspNetCore.Mvc;
using FoodService.Model;
using FoodService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;

namespace FoodServiceTest
{
    public class FoodServTests
    {
        private FoodServ foodServ;

        public FoodServTests()
        {
            
            var foodContextMock = new Mock<AppDBContext>(new DbContextOptions<AppDBContext>());

            List<Food> foods = new List<Food>
            {
                new Food()
                {
                    Id = 1,
                    Name = "whiskas",
                    Producer = "Mars",
                    Doze = "One per day"
                },
                new Food()
                {
                    Id = 2,
                    Name = "whiskas2",
                    Producer = "Mars2",
                    Doze = "One per day"
                }
            };


            foodContextMock.Object.Foods = GetQueryableMockDbSet(foods);


            foodServ = new FoodServ(foodContextMock.Object);
        }

        [Fact]
        public async void GetFoods()
        {
            var result = await foodServ.GetFoodsAsync();
            List<Food> foods = new List<Food>
            {
                new Food()
                {
                    Id = 1,
                    Name = "whiskas",
                    Producer = "Mars",
                    Doze = "One per day"
                },
                new Food()
                {
                    Id = 2,
                    Name = "whiskas2",
                    Producer = "Mars2",
                    Doze = "One per day"
                }
            };

            var foodsStr = JsonConvert.SerializeObject(foods);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(foodsStr, resultStr);
        }

        [Fact]
        public async void DeleteFood()
        {
            var result = await foodServ.DeleteFoodById(1);
            var food = new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            };

            var foodsStr = JsonConvert.SerializeObject(food);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(foodsStr, resultStr);
        }

        [Fact]
        public async void AddFood()
        {
            var result = await foodServ.AddFood(new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            });

            var food = new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            };

            var foodsStr = JsonConvert.SerializeObject(food);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(foodsStr, resultStr);
        }

        [Fact]
        public async void GetFoodById()
        {
            var result = await foodServ.GetFoodByIdAsync(2);
            var food = new Food()
            {
                Id = 2,
                Name = "whiskas2",
                Producer = "Mars2",
                Doze = "One per day"
            };

            var foodsStr = JsonConvert.SerializeObject(food);
            var resultStr = JsonConvert.SerializeObject(result);

            Assert.NotNull(result);
            Assert.Equal(foodsStr, resultStr);
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