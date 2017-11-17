using AgpromaWebAPI.model;
using ForgetPassword.Controllers;
using ForgetPassword.service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestingAgProMa.Controllers
{
    public class ForgetPasswordControllerTest
    {
        [Fact]
        public void Test_Case_To_Check_post_giving_true_sand_200status_code()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Returns(true);
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (OkObjectResult)obj.post("password");
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void Test_Case_To_Check_post_giving_OkObjectResult_type()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Returns(true);
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (OkObjectResult)obj.post("password");
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Test_Case_To_Check_post_giving_false_sand_400status_code()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Throws(new ArgumentNullException());
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (BadRequestResult)obj.post("password");
            //Assert
            Assert.Equal(400, result.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void Test_Case_To_Check_post_giving_BadRequestResult_type()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Throws(new ArgumentNullException());
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (BadRequestResult)obj.post("password");
            //Assert
            Assert.Equal(400, result.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void Test_Case_To_Check_post_giving_false_sand_500status_code()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Throws(new Exception());
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (StatusCodeResult)obj.post("password");
            //Assert
            Assert.Equal(500, result.StatusCode);
        }
        [Fact]
        public void Test_Case_To_Check_post_giving_StatusCodeResult_type()
        {
            //Arrange
            var mockobj = new Mock<IforgetPassword>();
            mockobj.Setup(x => x.EmailForResetPassword(It.IsAny<string>())).Throws(new Exception());
            ForgetPasswordController obj = new ForgetPasswordController(mockobj.Object);
            //Act
            var result = (StatusCodeResult)obj.post("password");
            //Assert
            Assert.IsType<StatusCodeResult>(result);
        }
    }
}