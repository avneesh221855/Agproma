using Moq;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTestingAgProMa.Services
{
   public class BacklogServiceTest
    {
        [Fact]
        public void Backlog_Service_GetAll_Method_To_GetAll_Request()
        {
            //Arrange
            List<UserStory> requests = new List<UserStory>();
            var request = new UserStory();
            request.StoryId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests); //mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(requests, res);
        }

        [Fact]
        public void Backlog_Service_GetAll_Method_should_return_productbacklog_type_object()
        {
            //Arrange
            List<UserStory> requests = new List<UserStory>();
            var request = new UserStory();
            request.StoryId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests); //mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);

            //Assert
            Assert.IsType<List<UserStory>>(res);
        }
        [Fact]
        public void Backlog_Service_Update_Method_To_Update_userStory()
        {
            //Arrange
            List<UserStory> requests = new List<UserStory>();
            var request = new UserStory();
            request.StoryId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Update(It.IsAny<int>(), request)).Returns(request);//mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.Update(It.IsAny<int>(), request);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(request, res);
            Assert.IsType<UserStory>(res);
        }

        [Fact]
        public void Backlog_Service_Update_Method_should_return_ProductBacklog_Type_object()
        {
            //Arrange
            List<UserStory> requests = new List<UserStory>();
            var request = new UserStory();
            request.StoryId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Update(It.IsAny<int>(), request)).Returns(request);//mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.Update(It.IsAny<int>(), request);

            //Assert
            Assert.IsType<UserStory>(res);
        }
        [Fact]
        public void Backlog_Service_JoinGroup_Method()
        {
            //Arrange
            List<SignalRMaster> requests = new List<SignalRMaster>();
            var request = new SignalRMaster();
            request.MemberId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.JoinGroup(It.IsAny<int>())).Returns(requests);//mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.JoinGroup(It.IsAny<int>());

            //Assert
            Assert.NotNull(res);
            Assert.Equal(requests, res);
        }

        [Fact]
        public void Backlog_Service_JoinGroup_Method_should_return_SignalIRMaster_type_object()
        {
            //Arrange
            List<SignalRMaster> requests = new List<SignalRMaster>();
            var request = new SignalRMaster();
            request.MemberId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IBacklogRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.JoinGroup(It.IsAny<int>())).Returns(requests);//mocking GetAll() of RequestRepository
            BacklogService obj = new BacklogService(mockRepoReq.Object);
            //Act
            var res = obj.JoinGroup(It.IsAny<int>());

            //Assert
            Assert.IsType<List<SignalRMaster>>(res);
        }


        [Fact]
        public void Backlog_serive_Add_method_throw_exception_with_invalid_value_type()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;
            UserStory backlog2 = new UserStory();
            backlog.StoryId = 2;
            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Add(backlog)).Throws(new FormatException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Add(backlog));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Backlog_service_Add_method_throw_nullrefrence_Exception()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;

            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Add(backlog)).Throws(new NullReferenceException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Add(backlog));
            Assert.IsType<NullReferenceException>(exception);
        }

        [Fact]
        public void Backlog_serive_Delete_method_throw_exception_with_invalid_value_type()
        {
            UserStory backlog = new UserStory();
           


            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Delete(It.IsAny<int>())).Throws(new FormatException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Delete(It.IsAny<int>()));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Backlog_serive_Delete_method_throw_nullReferenceException()
        {
            UserStory backlog = new UserStory();
            
            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Delete(It.IsAny<int>())).Throws(new NullReferenceException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Delete(It.IsAny<int>()));
            Assert.IsType<NullReferenceException>(exception);
        }

        [Fact]
        public void Backlog_serive_update_method_throw_nullrefrence_Exception()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;

            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Update(It.IsAny<int>(), backlog)).Throws(new NullReferenceException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Update(It.IsAny<int>(), backlog));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Backlog_serive_update_method_throw_Format_Exception_with_invalid_input()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;

            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.Update(It.IsAny<int>(), backlog)).Throws(new FormatException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.Update(It.IsAny<int>(), backlog));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Backlog_service_setConnection_method_should_throw_Format_Exception_with_invalid_input()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;
            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.setConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new FormatException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.setConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Backlog_service_setConnection_method_should_throw_Argument_Null_Exception_with_invalid_input()
        {
            UserStory backlog = new UserStory();
            backlog.StoryId = 1;
            var mockrepo = new Mock<IBacklogRepository>();
            mockrepo.Setup(x => x.setConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new ArgumentNullException());
            BacklogService obj = new BacklogService(mockrepo.Object);

            var exception = Record.Exception(() => obj.setConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
