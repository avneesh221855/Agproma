using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestingAgProMa.Services
{
    public class ProjectMemberServiceTest
    {
        [Fact]
        public void getMemberDetails_should_not_return_null()
        {
            //arrange
            List<Projectmembers> list = new List<Projectmembers>();
            Projectmembers member = new Projectmembers() { id = 1 };
            list.Add(member);
            var mockRepo = new Mock<ProjectmembersRepository>();
            mockRepo.Setup(m => m.getMemberDetails(1)).Returns(list);
            Projectmemberservice memberService = new Projectmemberservice(mockRepo.Object);
            //act
            var result = memberService.getMemberDetails(It.IsAny<int>());
            //assert
            Assert.NotNull(result);
        }
        [Fact]
        public void getMemberDetails_should_return_memberDetails()
        {
            //arrange
            List<Projectmembers> list = new List<Projectmembers>();
            Projectmembers member = new Projectmembers() { id = 1 };
            list.Add(member);
            var mockRepo = new Mock<IProjectmembersRepository>();
            mockRepo.Setup(m => m.getMemberDetails(It.IsAny<int>())).Returns(list);
            Projectmemberservice memberService = new Projectmemberservice(mockRepo.Object);
            //act
            var result = memberService.getMemberDetails(It.IsAny<int>());
            //assert
            Assert.IsType<List<Projectmembers>>(result);
            Assert.Equal(list, result);
        }
        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_NullReferenceException()
        {
            //arrange
            List<Projectmembers> projectmemberlist = new List<Projectmembers>();
            Projectmembers member = new Projectmembers() { id = 1 ,MemberId = 1,ProjectId = 1};
            var mockRepo = new Mock<IProjectmembersRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new NullReferenceException());
            mockRepo.Setup(m => m.getMemberDetails(1)).Returns(projectmemberlist);
            Projectmemberservice memberService = new Projectmemberservice(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<NullReferenceException>(ex);
        }
        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_Format_Exception()
        {
            //arrange
            List<Projectmembers> projectmemberlist = new List<Projectmembers>();
            Projectmembers member = new Projectmembers();
            member.id = 1;
            member.MemberId = 1;
            member.ProjectId = 1;
            var mockRepo = new Mock<IProjectmembersRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new FormatException());
            mockRepo.Setup(m => m.getMemberDetails(1)).Returns(projectmemberlist);
            Projectmemberservice memberService = new Projectmemberservice(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<FormatException>(ex);
        }
    }
}