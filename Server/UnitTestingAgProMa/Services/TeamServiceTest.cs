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
    public class TeamServiceTest
    {
            [Fact]
            public void TestForAddMembers()
            {
                //arrange
                TeamMember memberObject = new TeamMember() { Id = 101, MemberId = 11, TeamId = 1 };
                List<TeamMember> member = new List<TeamMember>();
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.AddMembers(memberObject));
                TeamService serObj = new TeamService(mockRepo.Object);
                //Act
                serObj.AddMembers(memberObject);
                //Assert

            }
            [Fact]
            public void TestForGetAvailableMembers()
            {
                //Arrange
                List<AvailTeamMember> member = new List<AvailTeamMember>();
                var mem = new AvailTeamMember()
                {
                    Id = 1,
                };
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.GetProjectMember(It.IsAny<int>()));
                mockRepo.Setup(m => m.GetTeamMember(It.IsAny<int>()));
                TeamService serObj = new TeamService(mockRepo.Object);


                //Act
                var result =  Record.Exception(()=>serObj.GetAvailableMember(101));

            //Assert
            Assert.IsType<NullReferenceException>(result);

            }
            [Fact]
            public void TestForGetTeam()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.GetTeam()).Returns(team);
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var result = teamService.GetTeam(It.IsAny<int>());

                //assert
                Assert.NotNull(result);
            }

            [Fact]
            public void GetTeam_should_not_return_null()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.GetTeam()).Returns(team);
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var result = teamService.GetTeam(It.IsAny<int>());

                //assert
                Assert.NotNull(result);
            }

            [Fact]
            public void GetTeam_should_return_List_of_TeamMaster()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.GetTeam()).Returns(team);
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var result = teamService.GetTeam(It.IsAny<int>());

                //assert
                Assert.IsType<List<TeamMaster>>(result);
                Assert.Equal(team, result);
            }

            [Fact]
            public void Checking_return_type_of_GetTeam()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.GetTeam()).Returns(team);
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var result = teamService.GetTeam(It.IsAny<int>());

                //assert
                Assert.IsNotType<List<TeamMember>>(result);
            }

            [Fact]
            public void TeamsService_addTeam_should_Throw_NullReferenceException()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.AddTeam(It.IsAny<TeamMaster>())).Throws(new NullReferenceException());
                TeamService teamService = new TeamService(mockRepo.Object);
                //act
                var ex = Record.Exception(() => teamService.AddTeam(master));
                //assert
                Assert.IsType<NullReferenceException>(ex);

            }

            [Fact]
            public void TeamsService_addTeam_should_Throw_FormatException()
            {
                //arrange
                TeamMaster master = new TeamMaster() { TeamId = 1 };
                List<TeamMaster> team = new List<TeamMaster>();
                team.Add(master);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.AddTeam(It.IsAny<TeamMaster>())).Throws(new FormatException());
                TeamService teamService = new TeamService(mockRepo.Object);
                //act
                var ex = Record.Exception(() => teamService.AddTeam(master));
                //assert
                Assert.IsType<FormatException>(ex);

            }

            [Fact]
            public void TeamsService_addMembers_should_Throw_FormatException()
            {
                //arrange
                TeamMember member = new TeamMember() { TeamId = 1 };
                List<TeamMember> team = new List<TeamMember>();
                team.Add(member);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.AddMembers(It.IsAny<TeamMember>())).Throws(new FormatException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.AddMembers(It.IsAny<TeamMember>()));

                //assert
                Assert.IsType<FormatException>(ex);

            }

            [Fact]
            public void TeamsService_addMember_should_Throw_NullReferenceException()
            {
                //arrange
                TeamMember member = new TeamMember() { TeamId = 1 };
                List<TeamMember> team = new List<TeamMember>();
                team.Add(member);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.AddMembers(It.IsAny<TeamMember>())).Throws(new NullReferenceException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.AddMembers(member));

                //assert
                Assert.IsType<NullReferenceException>(ex);

            }
            [Fact]
            public void TeamsService_deleteMember_should_Throw_NullReferenceException()
            {
                //arrange
                TeamMember member = new TeamMember() { TeamId = 1 };
                List<TeamMember> team = new List<TeamMember>();
                team.Add(member);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.DeleteMember(It.IsAny<int>())).Throws(new NullReferenceException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.DeleteMember(It.IsAny<int>()));

                //assert
                Assert.IsType<NullReferenceException>(ex);

            }
            [Fact]
            public void TeamsService_deleteMember_should_Throw_FormatException()
            {
                //arrange
                TeamMember member = new TeamMember() { TeamId = 1 };
                List<TeamMember> team = new List<TeamMember>();
                team.Add(member);
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.DeleteMember(It.IsAny<int>())).Throws(new FormatException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.DeleteMember(It.IsAny<int>()));

                //assert
                Assert.IsType<FormatException>(ex);

            }

            [Fact]
            public void TeamsService_updateConnection_should_Throw_FormatException()
            {
                //arrange
                SignalRMaster master = new SignalRMaster() { SignalId = 1 };
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new FormatException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>()));

                //assert
                Assert.IsType<FormatException>(ex);

            }
            [Fact]
            public void TeamsService_upadateConnection_should_Throw_NullReferenceException()
            {
                //arrange
                SignalRMaster master = new SignalRMaster() { SignalId = 1 };
                var mockRepo = new Mock<ITeamRepo>();
                mockRepo.Setup(m => m.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>())).Throws(new NullReferenceException());
                TeamService teamService = new TeamService(mockRepo.Object);

                //act
                var ex = Record.Exception(() => teamService.UpdateConnectionId(It.IsAny<string>(), It.IsAny<int>()));

                //assert
                Assert.IsType<NullReferenceException>(ex);

            }

        }
    }