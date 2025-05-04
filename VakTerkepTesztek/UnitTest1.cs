using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Vak_Terkep.Implementations;
using Vak_Terkep.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Vak_Terkep.Models;
using Microsoft.EntityFrameworkCore;

namespace VakTerkepTesztek
{
    public class Tests
    {

        private RoutingService routingService;
        private AuthenticationService authenticationService;
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private Mock<IUserInterface> userInterfaceMock;
        private Mock<IEncryptService> encryptServiceMock;
        private Mock<ISession> sessionMock;
        private Mock<IRouting> routingMock;
        private Mock<HttpContext> httpContextMock;
        private Mock<Route> routeMock;
        private Mock<IDistributedCache> cacheMock;
        private Mock<VakTerkepDbContext> dbContextMock;


        [SetUp]
        public void Setup()
        {
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            userInterfaceMock = new Mock<IUserInterface>();
            encryptServiceMock = new Mock<IEncryptService>();
            sessionMock = new Mock<ISession>();
            routingMock = new Mock<IRouting>();
            routeMock = new Mock<Route> { CallBase = true };
            httpContextMock = new Mock<HttpContext>();

            httpContextMock.Setup(ctx => ctx.Session).Returns(sessionMock.Object);
            mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(httpContextMock.Object);

            
            cacheMock = new Mock<IDistributedCache>();
            dbContextMock = new Mock<VakTerkepDbContext>();

            
            routingService = new RoutingService(cacheMock.Object, dbContextMock.Object);

            authenticationService = new AuthenticationService(
                mockHttpContextAccessor.Object,
                userInterfaceMock.Object,
                encryptServiceMock.Object
            );
        }


        [Test]
        [TestCase("2-es terem")]
        [TestCase("24-es terem")]
        [TestCase("10-es terem")]
        public void SearchValidRoom(string room)
        {
            var expectedRoute = new Route { textName = room };

            var routeData = new List<Route> { expectedRoute }.AsQueryable();
            var mockDbSet = new Mock<DbSet<Route>>();
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(routeData.Provider);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(routeData.Expression);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(routeData.ElementType);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(routeData.GetEnumerator());

            dbContextMock.Setup(db => db.Routes).Returns(mockDbSet.Object);

            routingService = new RoutingService(cacheMock.Object, dbContextMock.Object);

            var result = routingService.GetRouteByName(room);

            Assert.IsNotNull(result);
            Assert.AreEqual(room, result.textName);
            dbContextMock.Verify(db => db.Routes, Times.Once);
        }

        [Test]
        [TestCase("")]
        public void SearchEmptyInput(string room)
        {
            Route expectedRoute = null; 

            var routeData = new List<Route>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Route>>();
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(routeData.Provider);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(routeData.Expression);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(routeData.ElementType);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(routeData.GetEnumerator());

            dbContextMock.Setup(db => db.Routes).Returns(mockDbSet.Object);

            routingService = new RoutingService(cacheMock.Object, dbContextMock.Object);

            var result = routingService.GetRouteByName(room);

            Assert.IsNull(result); 
            dbContextMock.Verify(db => db.Routes, Times.Never); 
        }
        [Test]
        [TestCase("fifjbfsjb")]
        public void SearchStupidInput(string room)
        {
            
            Route expectedRoute = null;

            
            var routeData = new List<Route>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Route>>();
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Provider).Returns(routeData.Provider);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.Expression).Returns(routeData.Expression);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.ElementType).Returns(routeData.ElementType);
            mockDbSet.As<IQueryable<Route>>().Setup(m => m.GetEnumerator()).Returns(routeData.GetEnumerator());

            dbContextMock.Setup(db => db.Routes).Returns(mockDbSet.Object);

            routingService = new RoutingService(cacheMock.Object, dbContextMock.Object);

            var result = routingService.GetRouteByName(room);

            Assert.IsNull(result);
            dbContextMock.Verify(db => db.Routes, Times.Once);
        }
    }
}