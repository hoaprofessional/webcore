using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCore.Entities;
using WebCore.EntityFramework.Data;
using WebCore.EntityFramework.Repositories;
using WebCore.Services.Share.AppMenus;
using Xunit;

namespace Webcore.Tests
{
    public class WebcoreBaseTest
    {
        protected void Start()
        {
            var builder = new DbContextOptionsBuilder<WebCoreDbContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            builder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var options = builder.Options;
        }
        [Fact]
        public void Test1()
        {
            var builder = new DbContextOptionsBuilder<WebCoreDbContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            builder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var options = builder.Options;

            using (var context = new WebCoreDbContext(options))
            {
                var adminMenus = new List<AdminMenu>
            {
                new AdminMenu { Id= 1, Name = "a"},
                new AdminMenu { Id= 2, Name = "Fiyaz Hasan" },
                new AdminMenu { Id= 3, Name = "Jon Doe" },
                new AdminMenu { Id= 4, Name = "Jane DOe" }
            };

                context.AddRange(adminMenus);
                context.SaveChanges();
            }

            using (var context = new WebCoreDbContext(options))
            {
                var repository = new RepositoryImpl<AdminMenu,int>(context);
                var menus = repository.GetAll();
                Assert.Equal(4, menus.Count());
            }
        }
    }
}
