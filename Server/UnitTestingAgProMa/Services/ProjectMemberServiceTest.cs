using Moq;
using MyNeo4j.model;
using MyNeo4j.Repository;
using MyNeo4j.Service;
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
            List<ProjectMember> list = new List<ProjectMember>();
            ProjectMember member = new ProjectMember() { id = 1 };
            list.Add(member);
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.getMemberDetails(It.IsAny<int>())).Returns(list);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);

            //act
            var result = memberService.getMemberDetails(It.IsAny<int>());

            //assert
            Assert.NotNull(result);

        }

        [Fact]
        public void getMemberDetails_should_return_memberDetails()
        {
            //arrange
            List<ProjectMember> list = new List<ProjectMember>();
            ProjectMember member = new ProjectMember() { id = 1 };
            list.Add(member);
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.getMemberDetails(It.IsAny<int>())).Returns(list);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);

            //act
            var result = memberService.getMemberDetails(It.IsAny<int>());

            //assert
            Assert.IsType<List<ProjectMember>>(result);
            Assert.Equal(list, result);

        }

        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_NullReferenceException()
        {
            //arrange
            List<ProjectMember> projectmemberlist = new List<ProjectMember>();
            ProjectMember member = new ProjectMember() { id = 1 ,MemberId = 1,ProjectId = 1};
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new NullReferenceException());
            mockRepo.Setup(m => m.getMemberDetails(1)).Returns(projectmemberlist);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<NullReferenceException>(ex);

        }
        [Fact]
        public void ProjectMemberService_addMemberDetails_should_Throw_Format_Exception()
        {
            //arrange
            List<ProjectMember> projectmemberlist = new List<ProjectMember>();
            ProjectMember member = new ProjectMember();
            member.id = 1;
            member.MemberId = 1;
            member.ProjectId = 1;
            var mockRepo = new Mock<IProjectMemberRepository>();
            mockRepo.Setup(m => m.Add_MemberDetails(member)).Throws(new FormatException());
            mockRepo.Setup(m => m.getMemberDetails(1)).Returns(projectmemberlist);
            ProjectMemberService memberService = new ProjectMemberService(mockRepo.Object);
            //act
            var ex = Record.Exception(() => memberService.Add_MemberDetails(member));
            //assert
            Assert.IsType<FormatException>(ex);

        }

    }
}
