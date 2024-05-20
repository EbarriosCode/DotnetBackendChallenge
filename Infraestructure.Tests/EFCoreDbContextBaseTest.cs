using EFCoreImplementation.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Tests
{
    public class EFCoreDbContextBaseTest : IDisposable
    {
        protected readonly EFCoreImplementationDbContext _context;

        public EFCoreDbContextBaseTest()
        {
            var options = new DbContextOptionsBuilder<EFCoreImplementationDbContext>()
                .UseInMemoryDatabase(databaseName: "db_for_tests")
                .Options;

            this._context = new EFCoreImplementationDbContext(options);
            this._context.Database.EnsureCreated();

            DbInitializerForTests.Initialize(this._context);
        }
        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
