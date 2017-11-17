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
   public class TaskBacklogServiceTest
    {
        [Fact]
        public void SignUpServiceUnitTest_to_GetAllTaskDetail_for_NotNull()
        {
            //Arrange
            List<TaskBacklog> tasks = new List<TaskBacklog>();
            List<TaskBacklogView> taskv = new List<TaskBacklogView>();
            var task = new TaskBacklog() { TaskId = 1 };
            tasks.Add(task);
            var mockRepoTask = new Mock<ITaskBacklogReposiory>();
            mockRepoTask.Setup(x => x.GetAllTaskDetail(1)).Returns(tasks);
            TaskBacklogService obj = new TaskBacklogService(mockRepoTask.Object);
            //Act
            var res = obj.GetAllTask(1);
            //Assert
            Assert.Equal(1, res.Count);

        }
        [Fact]
        public void SignUpServiceUnitTest_to_GetAllTaskDetail_for_Null()
        {
            //Arrange
            List<TaskBacklog> tasks = new List<TaskBacklog>();
            var mockRepoTask = new Mock<ITaskBacklogReposiory>();
            mockRepoTask.Setup(x => x.GetAllTaskDetail(1)).Returns(tasks);
            TaskBacklogService obj = new TaskBacklogService(mockRepoTask.Object);
            //Act
            var res = obj.GetAllTask(1);
            //Assert
            Assert.Equal(0, res.Count);
        }
        [Fact]
        public void SignUpServiceUnitTest_to_GetTeamMember_for_NotNull()
        {
            //Arrange
            List<AvailTeamMember> teams = new List<AvailTeamMember>();
            var team = new AvailTeamMember();
            List<TeamMember> members = new List<TeamMember>();
            var member = new TeamMember() { TeamId = 1 };
            teams.Add(team);
            var mockRepoTask = new Mock<ITaskBacklogReposiory>();
            mockRepoTask.Setup(x => x.AllTeamMember()).Returns(members);
            TaskBacklogService obj = new TaskBacklogService(mockRepoTask.Object);
            //Act
            var res = obj.getTeamMember(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(teams.ToString(), res.ToString());
        }
        [Fact]
        public void SignUpServiceUnitTest_to_GetTeamMember_for_Null()
        {
            //Arrange
            List<AvailTeamMember> teams = new List<AvailTeamMember>();
            teams = null;
            List<TeamMember> members = new List<TeamMember>();
            var member = new TeamMember() { TeamId = 1 };
            member = null;
            var mockRepoTask = new Mock<ITaskBacklogReposiory>();
            mockRepoTask.Setup(x => x.AllTeamMember()).Returns(members);
            TaskBacklogService obj = new TaskBacklogService(mockRepoTask.Object);
            //Act
            var res = obj.getTeamMember(1);
            res = teams;
            //Assert
            Assert.Null(res);

        }

        [Fact]
        public void Backlog_serive_Add_method_throw_exception_with_invalid_value_type()
        {
            TaskBacklog task = new TaskBacklog();
            task.PersonId = 1;
            var mockrepo = new Mock<ITaskBacklogReposiory>();
            mockrepo.Setup(x => x.Update(1, 1)).Throws(new NullReferenceException());
            TaskBacklogService obj = new TaskBacklogService(mockrepo.Object);
            var exception = Record.Exception(() => obj.UpdateTask(1, 1));
            Assert.IsType<NullReferenceException>(exception);

        }
        [Fact]
        public void SignUpServiceUnitTest_to_GetProjectId_for_NotNull()
        {
            ////Arrange
            var sprint = new Sprint() { SprintId = 1 };
            var mockRepo = new Mock<ITaskBacklogReposiory>();
            mockRepo.Setup(x => x.GetProjectId(1)).Returns(sprint);
            TaskBacklogService obj = new TaskBacklogService(mockRepo.Object);
            //Act
            var res = obj.GetProjectId(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(sprint.ProjectId, res);

        }

    }
}
