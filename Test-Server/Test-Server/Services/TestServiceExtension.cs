using Test_Server.Database.Entities;
using Test_Server.Services.Dto;

namespace Test_Server.Services
{
    public static class TestServiceExtension
    {
        public static OperatorDto ToDto(this Operator model)
            => new(model.Id, model.Name, model.Code);

        public static Operator ToModel(this OperatorDto dto)
            => new()
            {
                Code = dto.Code,
                Name = dto.Name,
                Id = dto.Id ?? Guid.Empty
            };
    }
}
