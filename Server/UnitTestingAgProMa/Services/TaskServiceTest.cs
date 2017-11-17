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
    public class TaskServiceTest
    {
        [Fact]
        public void Task_Service_GetAll_Method_To_GetAll_Request()
        {
            //Arrange
            List<TaskBacklogView> taskv = new List<TaskBacklogView>() { new TaskBacklogView() { SprintId = 1 } };
            List<TaskBacklog> requests = new List<TaskBacklog>();
            var request = new TaskBacklog();
            request.SprintId = 1;
            requests.Add(request);
            //mocking RequestRepository
            var mockRepoReq = new Mock<ITaskRepository>();
            //mocking GetAll() of RequestRepository
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(taskv[0].SprintId, res[0].SprintId);

        }

        [Fact]
        public void Task_Service_GetAll_Method_To_GetAll_Request_Type_Object()
        {
            //Arrange
            List<TaskBacklog> requests = new List<TaskBacklog>();
            var request = new TaskBacklog();
            request.SprintId = 1;
            requests.Add(request);
            //mocking RequestRepository
            var mockRepoReq = new Mock<ITaskRepository>();
            //mocking GetAll() of RequestRepository                              
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);

            //Assert
            Assert.IsType<List<TaskBacklogView>>(res);
        }
        [Fact]
        public void Task_Service_JoinGroup_Method_To_See_Changes_Made()
        {
            //Arrange
            List<SignalRMaster> requests = new List<SignalRMaster>();
            var request = new SignalRMaster();
            request.MemberId = 1;
            requests.Add(request);
            //mocking RequestRepository
            var mockRepoReq = new Mock<ITaskRepository>();
            mockRepoReq.Setup(x => x.JoinGroup(It.IsAny<int>())).Returns(requests);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.JoinGroup(It.IsAny<int>());
            //Assert
            Assert.NotNull(res);
            Assert.Equal(requests, res);
        }

        [Fact]
        public void Task_Service_JoinGroup_Method_To_See_Changes_Made_Type_Object()
        {
            //Arrange
            List<SignalRMaster> requests = new List<SignalRMaster>();
            var request = new SignalRMaster();
            request.MemberId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<ITaskRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.JoinGroup(It.IsAny<int>())).Returns(requests);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.JoinGroup(It.IsAny<int>());
            //Assert
            Assert.IsType<List<SignalRMaster>>(res);
        }
        [Fact]
        public void Task_Service_Get_Method_To_Get_ProjectId()
        {
            //Arrange
            List<TaskBacklog> requests = new List<TaskBacklog>();
            var request = new TaskBacklog();
            request.TaskId = 1;
            requests.Add(request);
            var r = new Sprint();
            var mockRepoReq = new Mock<ITaskRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetProjectId(It.IsAny<int>())).Returns(r);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.GetProjectId(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(It.IsAny<int>(), res);
        }

        [Fact]
        public void Task_Service_GetProjectId_Method_Should_Return_Type_Object()
        {
            //Arrange
            List<TaskBacklog> requests = new List<TaskBacklog>();
            var request = new TaskBacklog();
            request.TaskId = 1;
            requests.Add(request);
            var r = new Sprint();
            //mocking RequestRepository
            var mockRepoReq = new Mock<ITaskRepository>();
            mockRepoReq.Setup(x => x.GetProjectId(It.IsAny<int>())).Returns(r);
            TaskService obj = new TaskService(mockRepoReq.Object);
            //Act
            var res = obj.GetProjectId(1);
            //Assert
            Assert.IsType<int>(res);
        }
        [Fact]
        public void Task_Service_Add_Method_Throws_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Add(Backlog)).Throws(new NullReferenceException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Add(Backlog));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Task_Service_Add_Method_Throws_FormatException_With_InvalidValue_Type()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Add(Backlog)).Throws(new FormatException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Add(Backlog));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Task_Service_Update_Method_Throws_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Update(It.IsAny<int>(), Backlog)).Throws(new NullReferenceException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Update(It.IsAny<int>(), Backlog));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Task_Service_Update_Method_Throws_FormatException_With_Invalid_ValueType()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.Update(It.IsAny<int>(), Backlog)).Throws(new FormatException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.Update(It.IsAny<int>(), Backlog));
            Assert.IsType<FormatException>(exception);
        }
        [Fact]
        public void Task_Service_SetConnetionId_Method_Throws_NullReferenceException()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.SetConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new NullReferenceException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.SetConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Task_Service_SetConnetionId_Method_Throws_FormatException_With_Invalid_ValueType()
        {
            //Arrange
            TaskBacklog Backlog = new TaskBacklog();
            Backlog.SprintId = 1;
            TaskBacklog Backlog1 = new TaskBacklog();
            Backlog.SprintId = 2;
            var request = new TaskBacklog();
            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.SetConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new FormatException());
            TaskService obj = new TaskService(mockRepo.Object);
            var exception = Record.Exception(() => obj.SetConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            Assert.IsType<FormatException>(exception);
        }
    }
}
