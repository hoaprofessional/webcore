using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebCore;
using WebCore.EntityFramework.Data;

namespace Webcore.Tests
{
    public class DbFixture
    {
        public DbFixture()
        {
            var connectionString = "Server=localhost; Database=WebcoreDbTest_" + Guid.NewGuid().ToString("N") + "; Trusted_Connection=True;";
            var webhost = Program.CreateWebHostBuilder(null).Build();
            ServiceProvider = webhost.Services;
        }

        public IServiceProvider ServiceProvider { get; private set; }
    }
}
