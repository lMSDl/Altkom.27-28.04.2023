using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Test.xUnit.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_OkWithAllUsers()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedList = new Fixture().CreateMany<User>();
            service.Setup(x => x.ReadAsync())
                .ReturnsAsync(expectedList);
            
            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<User>>(actionResult.Value);
            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public async Task Get_ExistingId_OkWithUser()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedUser = new Fixture().Create<User>();
            service.Setup(x => x.ReadAsync(expectedUser.Id))
                .ReturnsAsync(expectedUser);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get(expectedUser.Id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultUser = Assert.IsAssignableFrom<User>(actionResult.Value);
            Assert.Equal(expectedUser, resultUser);
        }

        [Fact]
        public Task Get_NotExistingId_NotFound()
        {
            return ReturnsNotFound((controller, id) => controller.Get(id));
        }

        private static async Task ReturnsNotFound(Func<UsersController, int, Task<IActionResult>> func)
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            int id = new Fixture().Create<int>();

            var controller = new UsersController(service.Object);

            //Act
            var result = await func(controller, id);

            //Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public Task Delete_NotExistingId_NotFound()
        {
            return ReturnsNotFound((controller, id) => controller.Delete(id));
        }

        [Fact]
        public Task Put_NotExistingId_NotFound()
        {
            User user = null;
            return ReturnsNotFound((controller, id) => controller.Put(id, user));
        }

        [Fact]
        public async Task Delete_ExistingId_NoContent()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedUser = new Fixture().Create<User>();
            service.Setup(x => x.ReadAsync(expectedUser.Id))
                .ReturnsAsync(expectedUser);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Delete(expectedUser.Id);

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
