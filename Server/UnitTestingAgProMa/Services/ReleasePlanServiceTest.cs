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
    public class ReleasePlanServiceTest
    {
        //First Test Case
        [Fact]
        public void Test_For_Checking_The_GetReleasePlan_Should_Be_NotNull()
        {
            //Arrange
            List<ReleasePlanMaster> releasePlan = new List<ReleasePlanMaster>();
            var releaseModel = new ReleasePlanMaster()
            {
                ReleasePlanId = 1
            };
            releasePlan.Add(releaseModel);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(x => x.GetAllRelease(It.IsAny<int>())).Returns(releasePlan);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);

            //Act
            List<ReleasePlanMaster> result = service.GetAllReleasePlan(1);

            //Assert
            Assert.NotNull(result);
        }

        //Second Test Case
        [Fact]
        public void Test_For_Checking_The_GetReleasePlan_Is_Of_ReleasePlanMaster()
        {
            //Arrange
            List<ReleasePlanMaster> releasePlan = new List<ReleasePlanMaster>();
            var releaseModel = new ReleasePlanMaster()
            {
                ReleasePlanId = 1
            };
            releasePlan.Add(releaseModel);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(x => x.GetAllRelease(It.IsAny<int>())).Returns(releasePlan);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);

            //Act
            List<ReleasePlanMaster> result = service.GetAllReleasePlan(1);

            //Assert
            Assert.IsType<List<ReleasePlanMaster>>(result);
        }

        //Third Test Case
        [Fact]
        public void Test_For_Checking_The_GetReleasePlan_Is_Not_Of_Any_Other_Model()
        {
            //Arrange
            List<ReleasePlanMaster> releasePlan = new List<ReleasePlanMaster>();
            var releaseModel = new ReleasePlanMaster()
            {
                ReleasePlanId = 1
            };
            releasePlan.Add(releaseModel);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(x => x.GetAllRelease(It.IsAny<int>())).Returns(releasePlan);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);

            //Act
            List<ReleasePlanMaster> result = service.GetAllReleasePlan(1);

            //Assert
            Assert.IsNotType<Sprint>(result);
            Assert.IsNotType<EpicMaster>(result);
            Assert.IsNotType<UserStory>(result);
            Assert.IsNotType<ChecklistBacklog>(result);
            Assert.IsNotType<TaskBacklog>(result);
            Assert.IsNotType<TeamMaster>(result);
        }

        //Fourth Test Case
        [Fact]
        public void Test_For_GetAllSprints()
        {
            //Arrange
            List<Sprint> sprintList = new List<Sprint>();
            var sprint = new Sprint()
            {
                SprintId = 1
            };
            sprintList.Add(sprint);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockSprintRepository.Setup(x => x.GetAll(It.IsAny<int>())).Returns(sprintList);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object,mockSprintRepository.Object);

            //Act
            List<Sprint> result = service.GetAllSprints(It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
        }

        //Fifth Test Case
        [Fact]
        public void Test_For_SignalR_Should_Be_Not_Null()
        {
            //Arrange
            List<SignalRMaster> signal = new List<SignalRMaster>();
            var signalR = new SignalRMaster()
            {
                SignalId = 1
            };
            signal.Add(signalR);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(x => x.CreateGroup(It.IsAny<int>())).Returns(signal);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);

            //Act
            List<SignalRMaster> result = service.CreateGroup(1);

            //Assert
            Assert.NotNull(result);
        }

        //Sixth Test Case
        [Fact]
        public void Test_For_SignalR_Should_Be_Of_SignalRMaster()
        {
            //Arrange
            List<SignalRMaster> signal = new List<SignalRMaster>();
            var signalR = new SignalRMaster()
            {
                SignalId = 1
            };
            signal.Add(signalR);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(x => x.CreateGroup(It.IsAny<int>())).Returns(signal);
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);

            //Act
            List<SignalRMaster> result = service.CreateGroup(1);

            //Assert
            Assert.IsType<List<SignalRMaster>>(result);
        }

        //Seventh Test Case
        [Fact]
        public void Test_for_Checking_AddRelease()
        {
            //arrange
            List<ReleasePlanMaster> releasePlan = new List<ReleasePlanMaster>();
            var releaseModel = new ReleasePlanMaster()
            {
                ReleasePlanId = 1
            };
            releasePlan.Add(releaseModel);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.AddRelease(It.IsAny<ReleasePlanMaster>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.AddRelease(releaseModel));
            //assert
            Assert.IsType<NullReferenceException>(ex);
        }

        //Eight Test Case
        [Fact]
        public void Test_for_Checking_AddRelease_Should_Be_NotNull()
        {
            //arrange
            List<ReleasePlanMaster> releasePlan = new List<ReleasePlanMaster>();
            var releaseModel = new ReleasePlanMaster()
            {
                ReleasePlanId = 1
            };
            releasePlan.Add(releaseModel);
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.AddRelease(It.IsAny<ReleasePlanMaster>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.AddRelease(releaseModel));
            //assert
            Assert.NotNull(ex);
        }

        //Ninth Test Case
        [Fact]
        public void Test_for_Checking_UpdateConnectionId()
        {
            //arrange
            SignalRMaster signalR = new SignalRMaster()
            {
                SignalId = 1
            };
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            //assert
            Assert.IsType<NullReferenceException>(ex);
        }

        //Tenth Test Case
        [Fact]
        public void Test_for_Checking_UpdateConnectionId_Should_Be_NotNull()
        {
            //arrange
            SignalRMaster signalR = new SignalRMaster()
            {
                SignalId = 1
            };
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>()));
            //assert
            Assert.NotNull(ex);
        }

        //Eleventh Test Case
        [Fact]
        public void Test_for_Checking_UpdateReleaseInSprint_Should_Be_NotNull()
        {
            //arrange
            Sprint sprint = new Sprint()
            {
                SprintId = 1
            };
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.UpdateReleaseInSprint(It.IsAny<int>(), It.IsAny<int>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.UpdateReleaseInSprint(It.IsAny<Sprint>(), It.IsAny<int>()));
            //assert
            Assert.NotNull(ex);
        }
        //Twelfth Test Case
        [Fact]
        public void Test_for_Checking_UpdateReleaseInSprint()
        {
            //arrange
            Sprint sprint = new Sprint()
            {
                SprintId = 1
            };
            var mockReleasePlanRepo = new Mock<IReleasePlanRepo>();
            var mockSprintRepository = new Mock<ISprintRepository>();
            mockReleasePlanRepo.Setup(m => m.UpdateReleaseInSprint(It.IsAny<int>(), It.IsAny<int>())).Throws(new NullReferenceException());
            ReleasePlanService service = new ReleasePlanService(mockReleasePlanRepo.Object, mockSprintRepository.Object);
            //act
            var ex = Record.Exception(() => service.UpdateReleaseInSprint(It.IsAny<Sprint>(), It.IsAny<int>()));
            //assert
            Assert.IsType<NullReferenceException>(ex);
        }
    }
}