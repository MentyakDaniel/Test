using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Test_Server.Database;
using Test_Server.Database.Entities;
using Test_Server.Services.Dto;

namespace Test_Server.Services
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _context;

        public TestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperatorDto> AddOperator(string name)
        {
            EntityEntry<Operator> result =
                await _context.Operators.AddAsync(new Operator() { Name = name });

            await _context.SaveChangesAsync();

            return result.Entity.ToDto();
        }

        public async Task<bool> DeleteOperator(Guid id)
        {
            Operator? result = await _context.Operators.FindAsync(id);

            if (result is not null)
                _context.Operators.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<OperatorDto>> GetAllOperators()
            => await _context.Operators.Select(item => item.ToDto()).ToListAsync();

        public async Task<OperatorDto> GetOperatorByCode(int code)
        {
            Operator result = await _context.Operators
                .SingleAsync(item => item.Code == code);

            return result.ToDto();
        }

        public async Task<OperatorDto?> GetOperatorById(Guid id)
        {
            Operator? result = await _context.Operators.FindAsync(id);

            return result is null
                ? null
                : result.ToDto();
        }

        public async Task<OperatorDto?> UpdateOperator(OperatorDto dto)
        {
            Operator? result = await _context.Operators.FindAsync(dto.Id);

            if (result is not null)
            {
                result.Name = dto.Name;

                _context.Operators.Update(result);

                _context.SaveChanges();

                return result.ToDto();
            }

            return null;
        }
    }
}
