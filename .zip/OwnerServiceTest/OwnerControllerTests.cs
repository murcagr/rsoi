using Microsoft.AspNetCore.Mvc;
using OwnerService.Controllers;
using OwnerService.Model;
using OwnerService.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace OwnerServiceTest
{
    public class OwnerControllerTests
    {
        private OwnerController ownerController;

        public OwnerControllerTests()
        {
            var ownerServiceMock = new Mock<IOwnerServ>();
            ownerServiceMock.Setup(x => x.GetOwnerByIdAsync(It.IsAny<int>())).ReturnsAsync((int x) =>
            new Owner()
            {
                Id = x,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            });

            ownerServiceMock.Setup(x => x.AddOwner(It.IsAny<Owner>())).ReturnsAsync(
                new Owner {
                    Id = 1,
                    Name = "Artem",
                    Age = 25,
                    City = "Moscow"
                }
                );

            ownerServiceMock.Setup(x => x.DeleteOwnerByID(It.IsAny<int>())).ReturnsAsync((int x) => 
                new Owner
                {
                    Id = x,
                    Name = "Artem",
                    Age = 25,
                    City = "Moscow"
                }
                );

            ownerController = new OwnerController(ownerServiceMock.Object);
        }

        [Fact]
        public async void GetOwnerById()
        {
            var result = await ownerController.GetOwnerByIdAsync(1);
            var okRes = result as OkObjectResult;

            var owner = new Owner()
            {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            };


            var ownerStr = JsonConvert.SerializeObject(owner);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);


            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(ownerStr, resultStr);
        }

        [Fact]
        public async void AddOwner()
        {
            var result = await ownerController.AddOwner(new Owner {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            });
            var okRes = result as OkObjectResult;

            var owner = new Owner()
            {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            };

            var ownerStr = JsonConvert.SerializeObject(owner);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(ownerStr, resultStr);
        }

        [Fact]
        public async void DeleteOwner()
        {
            var result = await ownerController.DeleteOwner(1);

            var okRes = result as OkObjectResult;

            var owner = new Owner()
            {
                Id = 1,
                Name = "Artem",
                Age = 25,
                City = "Moscow"
            };

            var ownerStr = JsonConvert.SerializeObject(owner);
            var resultStr = JsonConvert.SerializeObject(okRes.Value);

            Assert.NotNull(okRes);
            Assert.Equal(200, okRes.StatusCode);
            Assert.Equal(ownerStr, resultStr);
        }

    }
}