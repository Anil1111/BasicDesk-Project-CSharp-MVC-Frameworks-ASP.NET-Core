﻿using BasicDesk.Common.Constants;
using BasicDesk.App.Controllers;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.Data;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Security.Claims;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;

namespace BasicDesk.Tests.RequestsControllerTests
{
    [TestClass]
    public class CreateTests
    {
        private BasicDeskDbContext dbContext;
        private RequestService requestService;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = TestsDbContext.GetDbContext();
        }

        [TestMethod]
        public void Create_AddsRequestToTheDatabase()
        {
            this.dbContext.RequestStatuses.Add(new RequestStatus
            {
                Id = 1,
                Name = "Open"
            });

            this.dbContext.SaveChanges();

            var mockUserManager = TestsUserManager.GetUserManager();

            var controller = new RequestsController(this.dbContext, TestsAutoMapper.GetMapper(), mockUserManager.Object, requestService);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
              {
                        new Claim(ClaimTypes.Role, WebConstants.AdminRole)
                    }))
                }
            };

            var model = new RequestCreationBindingModel
            {
                Subject = "Subject",
                Description = "Description"
            };

            var result = controller.Create(model);

            var lastRequest = this.dbContext.Requests.LastOrDefault();

            Assert.IsNotNull(lastRequest);
            Assert.IsTrue(lastRequest.Subject == model.Subject && lastRequest.Description == model.Description);
        }
    }
}
