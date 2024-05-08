using Test_Server.Services.Dto;
using Test_Server.Database.Entities;

namespace Test_Server.Services
{
    public interface ITestService
    {
        public Task<ICollection<OperatorDto>> GetAllOperators();

        public Task<OperatorDto?> GetOperatorById(Guid id);
        public Task<OperatorDto> GetOperatorByCode(int code);

        public Task<OperatorDto> AddOperator(string name);
        public Task<OperatorDto?> UpdateOperator(OperatorDto dto);
        public Task<bool> DeleteOperator(Guid id);
    }
}
