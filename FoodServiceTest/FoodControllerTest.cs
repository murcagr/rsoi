using Microsoft.AspNetCore.Mvc;
using FoodService.Controllers;
using FoodService.Model;
using FoodService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace FoodServiceTest
{
    public class FoodControllerTests
    {
        private FoodController foodController;

        public FoodControllerTests()
        {
            var foodServiceMock = new Mock<IFoodServ>();
            foodServiceMock.Setup(x => x.GetFoodByIdAsync(It.IsAny<int>())).ReturnsAsync((int x) =>
            new Food()
            {
                Id = x,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            });

            foodServiceMock.Setup(x => x.AddFood(It.IsAny<Food>())).ReturnsAsync(
                new Food()
                {
                    Id = 1,
                    Name = "whiskas",
                    Producer = "Mars",
                    Doze = "One per day"
                });

            foodServiceMock.Setup(x => x.DeleteFoodById(It.IsAny<int>())).ReturnsAsync((int x) =>
                new Food()
                {
                    Id = x,
                    Name = "whiskas",
                    Producer = "Mars",
                    Doze = "One per day"
                });



            foodController = new FoodController(foodServiceMock.Object);
        }

        

        [Fact]
        public async void AddFood()
        {
            var result = await foodController.AddFood(new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            });
            var okRes = result as OkObjectResult;

            var food = new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            };

            var foodStr = JsonConvert.SerializeObject(food);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(foodStr, resultStr);
        }

        [Fact]
        public async void DeleteFood()
        {
            var result = await foodController.DeleteFood(1);

            var okRes = result as OkObjectResult;

            var food = new Food()
            {
                Id = 1,
                Name = "whiskas",
                Producer = "Mars",
                Doze = "One per day"
            };

            var foodStr = JsonConvert.SerializeObject(food);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(foodStr, resultStr);
        }

    }
}