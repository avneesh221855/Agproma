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
   public class EpicServiceTest
    {


        [Fact]
        public void Epic_Service_GetAll_Method_To_GetAll_Epics()
        {
            //Arrange
            List<EpicMaster> requests = new List<EpicMaster>();
            var request = new EpicMaster();
            request.EpicId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IEpicRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests); //mocking GetAll() of RequestRepository
            EpicService obj = new EpicService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);

            //Assert
            Assert.NotNull(res);
            Assert.Equal(requests, res);
        }

        [Fact]
        public void Epic_Service_GetAll_Method_should_return_productbacklog_type_object()
        {
            //Arrange
            List<EpicMaster> requests = new List<EpicMaster>();
            var request = new EpicMaster();
            request.EpicId = 1;
            requests.Add(request);
            var mockRepoReq = new Mock<IEpicRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.GetAll(1)).Returns(requests); //mocking GetAll() of RequestRepository
            EpicService obj = new EpicService(mockRepoReq.Object);
            //Act
            var res = obj.GetAll(1);

            //Assert
            Assert.IsType<List<EpicMaster>>(res);
        }
        [Fact]
        public void Epic_Service_Add_Method_should_throw_nullRefrenceException()
        {
            //Arrange

            var request = new EpicMaster();
            request.EpicId = 1;
            var mockRepoReq = new Mock<IEpicRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Add(request)).Throws(new NullReferenceException()); //mocking GetAll() of RequestRepository
            EpicService obj = new EpicService(mockRepoReq.Object);
            //Act
            var ex = Record.Exception(() => obj.Add(request));
            //Assert
     ;
            Assert.IsType<NullReferenceException>(ex);
        }

        [Fact]
        public void Epic_Service_Add_Method_should_throw_FormatException_with_invalid_input()
        {
            //Arrange

            var request = new EpicMaster();
            request.EpicId = 1;
            var mockRepoReq = new Mock<IEpicRepository>(); //mocking RequestRepository
            mockRepoReq.Setup(x => x.Add(request)).Throws(new FormatException()); //mocking GetAll() of RequestRepository
            EpicService obj = new EpicService(mockRepoReq.Object);
            //Act
            var ex = Record.Exception(() => obj.Add(request));
            //Assert
            // Assert.NotNull(res);
            Assert.IsType<FormatException>(ex);
        }
        [Fact]
        public void Epic_service_setConnection_method_should_throw_Format_Exception_with_invalid_input()
        {
            EpicMaster backlog = new EpicMaster();
            backlog.EpicId = 1;

            var mockrepo = new Mock<IEpicRepository>();
            mockrepo.Setup(x => x.SetConnectId(It.IsAny<int>(), It.IsAny<string>())).Throws(new FormatException());
            EpicService obj = new EpicService(mockrepo.Object);

            var exception = Record.Exception(() => obj.SetConnectId(It.IsAny<int>(), It.IsAny<string>()));
            Assert.IsType<FormatException>(exception);
        }

        [Fact]
        public void Epic_service_setConnection_method_should_throw_nullRefrenceException()
        {
            EpicMaster backlog = new EpicMaster();
            backlog.EpicId = 1;

            var mockrepo = new Mock<IEpicRepository>();
            mockrepo.Setup(x => x.SetConnectId(It.IsAny<int>(), It.IsAny<string>())).Throws(new NullReferenceException());
            EpicService obj = new EpicService(mockrepo.Object);

            var exception = Record.Exception(() => obj.SetConnectId(It.IsAny<int>(), It.IsAny<string>()));
            Assert.IsType<NullReferenceException>(exception);
        }

    }
}
