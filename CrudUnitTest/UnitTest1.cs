using NUnit.Framework;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Crud.Models;
using Microsoft.EntityFrameworkCore;
using Crud.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CrudUnitTest
{
    public class Tests
    {
        private CrudExampleContext _context;
        private MessagesController _controller;
        [SetUp]
        public async Task Setup()
        {
            _context = await GetDatabaseContext();
            _controller = new MessagesController(_context);
        }

        [Test]
        public void GetReturnAtleastOneRecord()
        {
            var result = _controller.Get();
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<Messages>>(okResult.Value);
            var returnValue = (List<Messages>)okResult.Value;
            Assert.NotZero(returnValue.Count);
        }


        private async Task<CrudExampleContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<CrudExampleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new CrudExampleContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Messages.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Messages.Add(new Messages()
                    {
                        Id = i,
                        Message= $"test message {i}",
                        IsDeleted = i%2 == 0,
                        Date = DateTime.UtcNow
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}