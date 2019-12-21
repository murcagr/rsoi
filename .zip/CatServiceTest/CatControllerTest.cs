using Microsoft.AspNetCore.Mvc;
using CatService.Controllers;
using CatService.Model;
using CatService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace CatServiceTest
{
    public class CatControllerTests
    {
        private CatController catController;

        public CatControllerTests()
        {
            var catServiceMock = new Mock<ICatServ>();
            catServiceMock.Setup(x => x.GetCatByIdAsync(It.IsAny<int>())).ReturnsAsync((int x) =>
            new Cat()
            {
                Id = x,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            });

            catServiceMock.Setup(x => x.AddCat(It.IsAny<Cat>())).ReturnsAsync(
                new Cat()
                {
                    Id = 20,
                    Name = "Olsi",
                    Breed = "Scottish",
                    FoodId = 1,
                    OwnerId = 1
                });

            catServiceMock.Setup(x => x.DeleteCatById(It.IsAny<int>())).ReturnsAsync((int x) =>
                new Cat()
                {
                    Id = x,
                    Name = "Olsi",
                    Breed = "Scottish",
                    FoodId = 1,
                    OwnerId = 1
                });



            catController = new CatController(catServiceMock.Object);
        }

        [Fact]
        public async void GetCatById()
        {
            var result = await catController.GetCatById(1);
            var okRes = result as OkObjectResult;

            var cat = new Cat()
            {
                Id = 1,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            };


            var catStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);


            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(catStr, resultStr);
        }

        [Fact]
        public async void AddCat()
        {
            var result = await catController.AddCat(new Cat()
            {
                Id = 20,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            });
            var okRes = result as OkObjectResult;

            var cat = new Cat()
            {
                Id = 20,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            };

            var catStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(catStr, resultStr);
        }

        [Fact]
        public async void DeleteCat()
        {
            var result = await catController.DeleteCat(1);

            var okRes = result as OkObjectResult;

            var cat = new Cat()
            {
                Id = 1,
                Name = "Olsi",
                Breed = "Scottish",
                FoodId = 1,
                OwnerId = 1
            };

            var catStr = JsonConvert.SerializeObject(cat);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(catStr, resultStr);
        }

    }
}