using DnD_API.Data;
using DnD_API.Models;
using DnD_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnD_API.Tests
{
    public class RunStoreTests
    {
        private DnDDbContext CreateContext()
        {
            var options =  new DbContextOptionsBuilder<DnDDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DnDDbContext(options);
        }

        [Fact]
        public void Can_Create_and_Get_Run()
        {
            var db = CreateContext(); var store = new RunStore(db);
            var charId = new Guid();
            
            var run = new Run
            {
                Id = "test-run",
                CharacterId = charId,
                StartedAt = DateTime.UtcNow,
                Status = "in_progress",
                Seed = 123,
                CurrentRoomId = new Guid().ToString()
            };

            store.Create(run);

            var fetched = store.Get("test-run");
            Assert.NotNull(fetched);
            Assert.Equal(charId, fetched.CharacterId);
        }
    }
}
