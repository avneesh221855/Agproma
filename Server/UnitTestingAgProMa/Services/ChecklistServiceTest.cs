using Microsoft.AspNetCore.Mvc;
using Moq;
using MyNeo4j.Controllers;
using MyNeo4j.model;
using MyNeo4j.Service;
using MyNeo4j.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyNeo4j_Test_Cases.Service
{
    public class ChecklistServiceUnitTest
    {
        [Fact]
        public void Checklist_Service_Get_Method_To_GetAll_Checklist()
        {
            //Arrange
            List<ChecklistBacklog> checklists = new List<ChecklistBacklog>();
            var checklist = new ChecklistBacklog();
            checklist.ChecklistId = 1;
            checklists.Add(checklist);
            var mockRepoCheck = new Mock<ICheckListRepository>(); //mocking ChecklistRepository 
            mockRepoCheck.Setup(x => x.Get()).Returns(checklists); //mocking Get() of ChecklistRepository
            ChecklistService obj = new ChecklistService(mockRepoCheck.Object);

            //Act
            var res = obj.Get();

            //Assert
            Assert.NotNull(res);
            Assert.Equal(checklists, res);
        }
        [Fact]
        public void Checklist_Service_GetById_To_Get_Checklist()
        {
            //Arrange
            List<ChecklistBacklog> checklists = new List<ChecklistBacklog>();
            var checklist = new ChecklistBacklog();
            checklist.ChecklistId = 1;
            checklists.Add(checklist);
            var mockRepoCheck = new Mock<ICheckListRepository>();
            mockRepoCheck.Setup(x => x.Get(1)).Returns(checklists);
            ChecklistService obj = new ChecklistService(mockRepoCheck.Object);
            //Act
            var res = obj.Get(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(checklists, res);
        }
        [Fact]
        public void Checklist_Serive_AddChecklist_method_throw_exception_with_invalid_value_type()
        {
            //Arrange
            ChecklistBacklog checklist1 = new ChecklistBacklog();
            checklist1.ChecklistId = 1;
            var mockrepo = new Mock<ICheckListRepository>();
            mockrepo.Setup(x => x.Add_Checklist(checklist1)).Throws(new NullReferenceException());
            ChecklistService obj = new ChecklistService(mockrepo.Object);
            //Act
            var exception = Record.Exception(() => obj.Add_Checklist(checklist1));
            //Assert
            Assert.IsType<NullReferenceException>(exception);
        }
        [Fact]
        public void Checklist_Service_GetTaskDetail()
        {
            //Arrange
            TaskBacklog tasks = new TaskBacklog();
            tasks.TaskId = 1;
            var mockRepoCheck = new Mock<ICheckListRepository>();
            mockRepoCheck.Setup(x => x.GetTaskDetail(1)).Returns(tasks);
            ChecklistService obj = new ChecklistService(mockRepoCheck.Object);
            //Act
            var res = obj.GetTaskDetail(1);
            //Assert
            Assert.NotNull(res);
            Assert.Equal(tasks, res);
        }

        [Fact]
        public void Checklist_Service_Delete_Method_To_delete_Checklist()
        {
            //Arrange
            ChecklistBacklog checklist = new ChecklistBacklog();
            var mockrepo = new Mock<ICheckListRepository>();
            mockrepo.Setup(x => x.Delete(1)).Throws(new NullReferenceException());
            ChecklistService obj = new ChecklistService(mockrepo.Object);
            //Act
            var exception = Record.Exception(() => obj.Delete(1));
            //Assert
            Assert.IsType<NullReferenceException>(exception);
        }


    }
}
