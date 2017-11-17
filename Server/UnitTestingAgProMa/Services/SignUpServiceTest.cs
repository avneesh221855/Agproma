using AgProMa.Repository;
using AgProMa.Services;
using Moq;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Service;
using MyNeo4j.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestingAgProMa.Services
{
    public class SignUpServiceTest
    {
        [Fact]
        public void SignUp_Service_GetAllDetails_Method_To_Get_Details()
        {
            //Arrange
            List<User> requests = new List<User>();
            var request = new User();
            request.Id = 1;
            requests.Add(request);
            //mocking RequestRepository
            var mockRepoReq = new Mock<ISignUpRepository>();
            //mocking GetAll() of RequestRepository
            mockRepoReq.Setup(x => x.GetAllDetails()).Returns(requests);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetAllDetails();
            //Assert
            Assert.NotNull(res);
            Assert.Equal(requests, res);
        }
        [Fact]
        public void SignUp_Service_GetAllDetails_Method_To_Get_Details_Type_Object()
        {
            //Arrange
            List<User> requests = new List<User>();
            var request = new User();
            request.Id = 1;
            requests.Add(request);
            //mocking RequestRepository
            var mockRepoReq = new Mock<ISignUpRepository>();
            //mocking GetAll() of RequestRepository
            mockRepoReq.Setup(x => x.GetAllDetails()).Returns(requests);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetAllDetails();
            //Assert
            Assert.IsType<List<User>>(res);
        }
        [Fact]
        public void SignUp_Service_Get_by_Id_Method()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            var request = new User();
            request.Id = 1;
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetById(1)).Returns(request);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetById(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(request, res);
        }
        [Fact]
        public void SignUp_Service_Get_by_Id_Method_Type_Object()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            var request = new User();
            request.Id = 1;
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetById(1)).Returns(request);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetById(1);
            //Assert
            Assert.IsType<User>(res);
        }
        [Fact]
        public void SignUpService_GetId_Method()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            User request = new User();
            request.Id = 1;
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Get(It.IsAny<String>())).Returns(request);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetId(It.IsAny<String>());
            //Assert
            Assert.NotNull(res);
            // Assert.Equal(request, res);
        }
        [Fact]
        public void SignUpService_GetId_Method_Type_Object()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            User request = new User();
            request.Id = 1;
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Get(It.IsAny<String>())).Returns(request);
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.GetId(It.IsAny<String>());
            //Assert
            Assert.IsType<int>(res);
        }
        [Fact]
        public void SignUp_Service_UpdatePassword_Method_Throws_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            User master = new User();
            master.Id = 1;
            //TaskBacklog Backlog1 = new TaskBacklog();
            //master.Id = 2;
            var request = new User();
            var mockRepo = new Mock<ISignUpRepository>();
            mockRepo.Setup(x => x.UpdatePassword(It.IsAny<int>(), It.IsAny<User>())).Throws(new NullReferenceException());
            SignUpService obj = new SignUpService(mockRepo.Object);
            var exception = Record.Exception(() => obj.UpdatePassword(It.IsAny<int>(), It.IsAny<User>()));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void SignUp_Service_UpdatePassword_Method_Throws_FormatException_With_Invalid_ValueType()
        {
            //Arrange
            User master = new User();
            master.Id = 1;
            var request = new User();
            var mockRepo = new Mock<ISignUpRepository>();
            mockRepo.Setup(x => x.UpdatePassword(It.IsAny<int>(), It.IsAny<User>())).Throws(new FormatException());
            SignUpService obj = new SignUpService(mockRepo.Object);
            var exception = Record.Exception(() => obj.UpdatePassword(It.IsAny<int>(), It.IsAny<User>()));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void SignUp_Service_Update_Method_Throws_FormatException_With_Invalid_ValueType()
        {
            //Arrange
            User master = new User();
            master.Id = 1;
            var request = new User();
            var mockRepo = new Mock<ISignUpRepository>();
            mockRepo.Setup(x => x.Update(It.IsAny<String>(), It.IsAny<User>())).Throws(new FormatException());
            SignUpService obj = new SignUpService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Update(It.IsAny<string>(), It.IsAny<User>()));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void SignUp_Service_Update_Method_Throws_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            User master = new User();
            master.Id = 1;
            var request = new User();
            var mockRepo = new Mock<ISignUpRepository>();
            mockRepo.Setup(x => x.Update(It.IsAny<String>(), It.IsAny<User>())).Throws(new NullReferenceException());
            SignUpService obj = new SignUpService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Update(It.IsAny<string>(), It.IsAny<User>()));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void SignUpService_Add_User_Method_Throws_NullReference_Exception()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            User re = new User();
            re.Id = 1;
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Add_User(It.IsAny<User>())).Throws(new NullReferenceException());
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var exception = Record.Exception(() => obj.Add_User(It.IsAny<User>()));
            //Assert
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void SignUpService_Add_User_Method_Throws_Format_Exception()
        {
            //Arrange
            //List<Master> requests = new List<Master>();
            User re = new User();
            re.Id = 1;
            re.Email = "dhiru5195@gmail.com";
            var mockRepoReq = new Mock<ISignUpRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Add_User(re)).Throws(new FormatException());
            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var exception = Record.Exception(() => obj.Add_User(re));
            //Assert
            Assert.IsType<FormatException>(exception);
        }


        [Fact]
        public void SignUp_Service_Check_Method_To_Get_Details()
        {
            //Arrange
            var idPass = new IdPassword();
            idPass.Email = "preetam";
            idPass.Password = "1234";
            User master = new User();
            master.Email = "preetam";
            master.Password = "1234";
            master.FirstName = "preetam";
            master.Id = 1;
            var creadential = new Creadential();
            creadential.UserId = 1;
            creadential.Status = "success";
            creadential.UserName = "preetam";
            creadential.Email = "preetam";

            //mocking RequestRepository
            var mockRepoReq = new Mock<ISignUpRepository>();
            //mocking GetAll() of RequestRepository
            mockRepoReq.Setup(x => x.Get(creadential.Email)).Returns(master);
            mockRepoReq.Setup(x => x.SetLoggedIn(1, true)).Returns(true);

            SignUpService obj = new SignUpService(mockRepoReq.Object);
            //Act
            var res = obj.Check(idPass);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(creadential.Email, res.Email);
            Assert.Equal(creadential.Status,res.Status);
            Assert.Equal(creadential.UserName,res.UserName);
            Assert.Equal(creadential.UserId,res.UserId);
        }
    }
}
