using AgProMa.Controllers;
using AgProMa.Services;
using AgpromaWebAPI.model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestingAgProMa.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void Test_Case_To_Check_Return_List_of_Members_in_Get_by_id_Function()
        {
            //Arrange
            User master = new User() { Id = 1 };
            var mockobj = new Mock<ISignUpService>();
            mockobj.Setup(x => x.GetById(It.IsAny<int>())).Returns(master);
            LoginController obj = new LoginController(mockobj.Object);
            //Act
            var result = obj.Details(5);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Test_Case_To_Check_Return_500_StatusCode_of_Get_by_id()
        {
            //Arrange
            User master = new User();
            var mockobj = new Mock<ISignUpService>();
            mockobj.Setup(x => x.GetById(It.IsAny<int>())).Throws(new Exception());
            LoginController obj = new LoginController(mockobj.Object);
            IActionResult result = obj.Details(It.IsAny<int>());
            //Act
            var result1 = (ObjectResult)result;
            //Assert
            Assert.Equal(400, result1.StatusCode);
        }        
        [Fact]
        public void Test_Case_To_Check_Return_200_StatusCode_of_Add_Function()
        {
            //Arrange
            User master = new User() { Id = 0 };
            var mockObj = new Mock<ISignUpService>();
            mockObj.Setup(x => x.GetById(It.IsAny<int>())).Returns(master);
            LoginController log = new LoginController(mockObj.Object);
            var result = log.Details(It.IsAny<int>());
            //Act
            var result1 = (OkObjectResult)result;
            //Assert
            Assert.Equal(200, result1.StatusCode);
        }
    }
}