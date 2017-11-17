using Moq;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using Microsoft.Extensions.Configuration;

using MyNeo4j.Viewmodel;
using AgProMa.Services;

namespace UnitTestingAgProMa.Services
{
    public class InviteMemberServiceTest
    { 
        [Fact]
        public void Invite_Member_Service_GetMemberName_Method_To_Get_Member_Name()
        {
            List<ProjectMember> list = new List<ProjectMember>();
            var ProjectMember = new ProjectMember();
            ProjectMember.MemberId = 1;
            list.Add(ProjectMember);
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.GetMemberName()).Returns(list);
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object,mockSignUpService.Object);
            //Act
            var result = obj.GetMemberName(1);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void Invite_Member_Service_GetMemberName_Method_To_Get_Member_Name_Type_Object()
        {
            List<ProjectMember> list = new List<ProjectMember>();
            var ProjectMember = new ProjectMember();
            ProjectMember.MemberId = 1;
            list.Add(ProjectMember);
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.GetMemberName()).Returns(list);
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object,mockSignUpService.Object);
            //Act
            var result = obj.GetMemberName(1);
            //Assert
            Assert.IsType<List<InviteExistingMember>>(result);
        }
        [Fact]
        public void Invite_Member_Service_EmailForInvitation_Throws_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            InvitePeople people = new InvitePeople();
            people.ProjectId = "1";
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.AllData(It.IsAny<int>())).Throws(new NullReferenceException());
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object,mockSignUpService.Object);
            //Act
            var exception = Record.Exception(() => obj.EmailForInvitation(It.IsAny<InvitePeople>()));
            //Assert
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Invite_Member_Service_EmailForInvitation_Should_Be_Of_NullReferenceException_With_Invalid_ValueType()
        {
            //Arrange
            InvitePeople people = new InvitePeople();
            people.ProjectId = "1";
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.AllData(It.IsAny<int>())).Throws(new NullReferenceException());
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object,mockSignUpService.Object);
            //Act
            var exception = Record.Exception(() => obj.EmailForInvitation(It.IsAny<InvitePeople>()));
            //Assert
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Invite_Member_Service_EmailForInvitation_Should_Not_Be_Of_FormatException_With_Invalid_ValueType()
        {
            //Arrange
            InvitePeople people = new InvitePeople();
            people.ProjectId = "1";
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.AllData(It.IsAny<int>())).Throws(new FormatException());
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object,mockSignUpService.Object);
            //Act
            var exception = Record.Exception(() => obj.EmailForInvitation(It.IsAny<InvitePeople>()));
            //Assert
            Assert.IsNotType<FormatException>(exception);
        }
        [Fact]
        public void Invite_Member_Service_EmailForInvitation_Should_Not_Be_Of_ArgumentNullException_With_Invalid_ValueType()
        {
            //Arrange
            InvitePeople people = new InvitePeople();
            people.ProjectId = "1";
            //mocking Repository
            var mockInviteRepo = new Mock<IInviteRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockSignUpService = new Mock<ISignUpService>();
            mockInviteRepo.Setup(x => x.AllData(It.IsAny<int>())).Throws(new ArgumentNullException());
            InviteMembersService obj = new InviteMembersService(mockInviteRepo.Object, mockConfiguration.Object, mockSignUpService.Object);
            //Act
            var exception = Record.Exception(() => obj.EmailForInvitation(It.IsAny<InvitePeople>()));
            //Assert
            Assert.IsNotType<ArgumentNullException>(exception);
        }
    }
}
