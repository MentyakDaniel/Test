using Test_Server.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test_Server.Database
{
    public class AppDbContextGenerator
    {
        private readonly AppDbContext _context;

        public AppDbContextGenerator(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
        {
            if (!_context.Operators.Any())
            {
                await _context.Operators.AddRangeAsync(new Operator[]
                {
                    new()
                    {
                        Name = "First"
                    },
                    new()
                    {
                        Name = "Second"
                    },
                    new()
                    {
                        Name = "Third"
                    }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
