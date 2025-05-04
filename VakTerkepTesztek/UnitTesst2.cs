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
using Vak_Terkep.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace VakTerkepTesztek
{
    public class Tests2
    {
        private AccountsController accountsController;
        private Mock<IEncryptService> encryptServiceMock;
        private Mock<IUserInterface> userInterfaceMock;
        private Mock<IAuthenticationServices> authenticationServiceMock;
        private Mock<IHttpContextAccessor> contextAccessorMock;

        [SetUp]
        public void Setup()
        {
            encryptServiceMock = new Mock<IEncryptService>();
            userInterfaceMock = new Mock<IUserInterface>();
            authenticationServiceMock = new Mock<IAuthenticationServices>();
            contextAccessorMock = new Mock<IHttpContextAccessor>();

            userInterfaceMock.Setup(ui => ui.GetAll()).Returns(new List<Account>().AsQueryable());

            contextAccessorMock.Setup(c => c.HttpContext).Returns(new DefaultHttpContext());

            accountsController = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );
        }
        [TearDown] public void TearDown()
        {
            if(accountsController != null) { 
            accountsController.Dispose();}
        }
        [Test]
        [TestCase("felhonev","felhonev@gmail.com","felhonevpassword14")]
        [TestCase("haraposrocky", "haraposrocky@gmail.com", "haraposrockyjelszo42")]
        public void RegisterUser_ValidUser_ReturnsSuccess(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            userInterfaceMock.Setup(ui => ui.GetAll()).Returns(new List<Account>().AsQueryable());

            var result = accountsController.RegisterUser(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignIn", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);

        }

        [Test]
        [TestCase("", "idonthaveaname@gmail.com", "noname10")]
        [TestCase("", "neitherme@gmail.com", "nonametest69")]
        public void RegisterUser_InvalidUser_EmptyName(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            userInterfaceMock.Setup(ui => ui.GetAll()).Returns(new List<Account>().AsQueryable());

            var result = accountsController.RegisterUser(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignIn", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmail_EmptyEmail))]
        public void RegisterUser_InvalidUser_EmptyEmail(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var result = controller.RegisterUser(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("null", controller.TempData["Message"]);
        }

        
        private static IEnumerable<TestCaseData> InvalidEmail_EmptyEmail()
        {
            yield return new TestCaseData("randomname", null, "verysecurepassword14!");
        }

        [Test]
        [TestCaseSource(nameof(InvalidPassword_EmptyPassword))]
        public void RegisterUser_InvalidUser_EmptyPassword(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var result = controller.RegisterUser(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("null", controller.TempData["Message"]);
        }

        
        private static IEnumerable<TestCaseData> InvalidPassword_EmptyPassword()
        {
            yield return new TestCaseData("verygoodname", "verybademail@hotmail.com", null);
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmail_NoAtSymbol))]
        public void RegisterUser_InvalidUser_NoAtSymbolInEmail(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            
            var result = controller.RegisterUser(account) as RedirectToActionResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("emailnoat", controller.TempData["Message"]);
        }

        
        private static IEnumerable<TestCaseData> InvalidEmail_NoAtSymbol()
        {
            yield return new TestCaseData("Kredilan", "wowplayercitromail.com", "mylife4azshara");
            yield return new TestCaseData("kelvin", "kelvinnpcfreemail.hu", "ilovevirginia420");
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmail_NoHuOrCom))]
        public void RegisterUser_InvalidUser_NoHuOrComAtEnd(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var result = controller.RegisterUser(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("emailnocomhu", controller.TempData["Message"]);
        }

        private static IEnumerable<TestCaseData> InvalidEmail_NoHuOrCom()
        {
            yield return new TestCaseData("randomperson", "randomperson@gmail.kp", "northkoreaisbest5");
            yield return new TestCaseData("spoodermanmain", "onlyspooderman@domain.ru", "Nerfvenom13");
            yield return new TestCaseData("Psylockemain15", "psylockefav@gmail", "Chō no mai o");
        }

        [Test]
        [TestCaseSource(nameof(InvalidPassword_PasswordTooShort))]
        public void RegisterUser_InvalidUser_PasswordTooShort(string username, string email, string password)
        {
        
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

       
            var result = controller.RegisterUser(account) as RedirectToActionResult;

        
            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("tosmall", controller.TempData["Message"]);
        }

        private static IEnumerable<TestCaseData> InvalidPassword_PasswordTooShort()
        {
            yield return new TestCaseData("string", "anotherstring@hotmail.com", "a3rdone");
            yield return new TestCaseData("text", "anothertext@gmail.com", "textpas");
            yield return new TestCaseData("content", "anothercontent@rambler.hu", "c0ntent");   
        }

        [Test]
        [TestCaseSource(nameof(InvalidPassword_NoNumber))]
        public void RegisterUser_InvalidUser_DoesNotContainNumber(string username, string email, string password)
        {
         
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

          
            var result = controller.RegisterUser(account) as RedirectToActionResult;

           
            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("tosmall", controller.TempData["Message"]);
        }
        private static IEnumerable<TestCaseData> InvalidPassword_NoNumber()
        {
            yield return new TestCaseData("magik", "magik@limbo.com", "Beholdthechild"); 
            yield return new TestCaseData("batman", "darkknight@gothamcity.com", "ILikeBats"); 
            yield return new TestCaseData("LunaSnow", "lunasnow@snow.com", "iamreadytoputonashow");  
        }

        [Test]
        [TestCaseSource(nameof(InvalidPassword_VeryEverythingIsBad))]
        public void RegisterUser_ShouldRedirectToSignUp_WhenAllFieldsAreEmpty(string username, string email, string password)
        {
            var account = new Account { userName = username, emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

          
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

           
            var result = controller.RegisterUser(account) as RedirectToActionResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("SignUp", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("null", controller.TempData["Message"]);
        }
        private static IEnumerable<TestCaseData> InvalidPassword_VeryEverythingIsBad()
        {
            yield return new TestCaseData(null, null, null);
        }

        [Test]
        [TestCase("wanda@ilikered.com", "PureChaooos")]
        public void TryLogIn_ValidCredentials(string email, string password)
        {
            var account = new Account { emailAddress = email, userPassword = password };

            authenticationServiceMock.Setup(auth => auth.TryLogIn(email, password)).Returns(true);

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var result = controller.TryLogIn(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Start", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
        }

        [Test]
        [TestCaseSource(nameof(TryLogin_InvalidCred_EmptyEmail))]
        public void TryLogIn_InvalidCredentials_EmptyEmail(string email, string password)
        {
            var account = new Account { emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var result = controller.TryLogIn(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignIn", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("blank", controller.TempData["Message"]);
        }

        private static IEnumerable<TestCaseData> TryLogin_InvalidCred_EmptyEmail()
        {
            yield return new TestCaseData(null, "verygoodpassword13!");
            yield return new TestCaseData(null, "evenbetterpassword129!@");
        }

        [Test]
        [TestCaseSource(nameof(TryLogin_InvalidCred_PasswordEmpty))]
        public void TryLogIn_InvalidCredentials_EmptyPassword(string email, string password)
        {
            var account = new Account { emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var result = controller.TryLogIn(account) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("SignIn", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("blank", controller.TempData["Message"]);
        }

        private static IEnumerable<TestCaseData> TryLogin_InvalidCred_PasswordEmpty()
        {
            yield return new TestCaseData("tester@gmail.com", null); 
            yield return new TestCaseData("testerke@hotmail.com", null);
        }

        [Test]
        [TestCaseSource(nameof(GetInvalidEmailAndPasswordForLoginCases))]
        public void TryLogIn_InvalidCredentials_EmptyEmailAndPassword(string email, string password)
        {
            
            var account = new Account { emailAddress = email, userPassword = password };

            var controller = new AccountsController(
                encryptServiceMock.Object,
                userInterfaceMock.Object,
                authenticationServiceMock.Object,
                contextAccessorMock.Object
            );

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            
            var result = controller.TryLogIn(account) as RedirectToActionResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("SignIn", result.ActionName);
            Assert.AreEqual("Map", result.ControllerName);
            Assert.AreEqual("blank", controller.TempData["Message"]); 
        }

        private static IEnumerable<TestCaseData> GetInvalidEmailAndPasswordForLoginCases()
        {
            yield return new TestCaseData(null, null); 
        }

    }
}