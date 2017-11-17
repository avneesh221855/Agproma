using Microsoft.AspNetCore.Mvc;
using Moq;
using MyNeo4j.Controllers;
using MyNeo4j.model;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyNeo4j_Test_Cases.Controllers
{
    public class ChecklistUnitTest
    {
        [Fact]
        public void Get_Method_Should_Return_OKObjectResult()
        {
            //Arrange
            var mockService = new Mock<ICheckListService>();
            var controller = new ChecklistController(mockService.Object);
            // Act
            IActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkObjectResult;
            // Assert
            Assert.Equal(200, contentResult.StatusCode);
            Assert.NotNull(contentResult);

        }
        [Fact]
        public void Get_should_Return_BadRequestResult()
        {
            //Arrange
            var mockservice = new Mock<ICheckListService>();
            mockservice.Setup(m => m.Get()).Throws(new Exception());
            ChecklistController checklistController = new ChecklistController(mockservice.Object);
            //Act
            var result = checklistController.Get();
            var contentResult = result as StatusCodeResult;

            //Assert
            Assert.Equal(400, contentResult.StatusCode);
            Assert.NotNull(result);

        }
        [Fact]
        public void GetChecklistById_Should_Return_Object()
        {
            //Arrange
            List<ChecklistBacklog> checklist = new List<ChecklistBacklog>();
            ChecklistBacklog check = new ChecklistBacklog() { ChecklistId = 1, Status = false };
            checklist.Add(check);
            var mockService = new Mock<ICheckListService>();
            mockService.Setup(m => m.Get(5)).Returns(checklist);
            var controller = new ChecklistController(mockService.Object);

            // Act
            var actionResult = controller.Get(5);
            var contentResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal(200, contentResult.StatusCode);
            Assert.NotNull(contentResult);

        }
        [Fact]
        public void GetChecklistById_ShouldReturn_Exception()
        {
            //Arrange
            List<ChecklistBacklog> checklist = new List<ChecklistBacklog>();
            checklist = null;
            var mockservice = new Mock<ICheckListService>();
            mockservice.Setup(m => m.Get(It.IsAny<int>())).Returns(checklist);
            ChecklistController checklistController = new ChecklistController(mockservice.Object);

            //Act
            var result = checklistController.Get(It.IsAny<int>());
            var contentResult = result as StatusCodeResult;

            //Assert
            Assert.Equal(500, contentResult.StatusCode);
            Assert.NotNull(result);

        }
        [Fact]
        public void PostMethod_should_return_OkResult()
        {
            //Arrange
            ChecklistBacklog checklist = new ChecklistBacklog() { ChecklistId = 2, Status = false, ChecklistName = "Login" };
            var mockService = new Mock<ICheckListService>();
            mockService.Setup(m => m.Add_Checklist(checklist));
            var controller = new ChecklistController(mockService.Object);

            // Act
            IActionResult actionResult = controller.Post(checklist);
            var contentResult = actionResult as ObjectResult;

            // Assert
            Assert.Equal(200, contentResult.StatusCode);
            Assert.NotNull(contentResult);
        }
        [Fact]
        public void Post_Should_Return_BadRequest_When_Exception()
        {
            //Arrange
            ChecklistBacklog checklist = new ChecklistBacklog() { ChecklistId = 123 };
            var mockservice = new Mock<ICheckListService>();
            mockservice.Setup(m => m.Add_Checklist(checklist)).Throws(new Exception());
            ChecklistController floorController = new ChecklistController(mockservice.Object);

            //Act
            var result = floorController.Post(checklist);
            var contentResult = result as StatusCodeResult;

            //Assert
            Assert.Equal(500, contentResult.StatusCode);
            Assert.NotNull(result);
        }
        [Fact]
        public void Delete_Should_Return_OKResult()
        {
            //Arrange
            var mockService = new Mock<ICheckListService>();
            var controller = new ChecklistController(mockService.Object);
            //Act
            IActionResult actionResult = controller.Delete(4);
            var contentResult = actionResult as StatusCodeResult;
            //Assert
            Assert.Equal(204, contentResult.StatusCode);
            Assert.NotNull(contentResult);
        }
    }
}

