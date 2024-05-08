using Microsoft.AspNetCore.Mvc;
using Test_Server.Database;
using Test_Server.Services;
using Test_Server.Services.Dto;

namespace Test_Server.Controllers
{
    [ApiController]
    [Route("operators")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _service;
        private readonly AppDbContext _context;

        public TestController(ITestService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<OperatorDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<ICollection<OperatorDto>>> GetAllOperators()
        {
            try
            {
                ICollection<OperatorDto> result = await _service.GetAllOperators();

                return Ok(result);
            }
            catch (Exception exp)
            {
                await _context.Log(exp.Message);

                return BadRequest(exp.Message);
            }
        }

        [HttpGet("{code:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(OperatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<OperatorDto>> GetOperatorByCode([FromRoute(Name ="code")] int code)
        {
            try
            {
                OperatorDto result = await _service.GetOperatorByCode(code);

                return Ok(result);
            }
            catch (Exception exp)
            {
                await _context.Log(exp.Message);

                return BadRequest(exp.Message);
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<OperatorDto>> AddOperator([FromBody] string name)
        {
            try
            {
                OperatorDto result = await _service.AddOperator(name);

                return Ok(result);
            }
            catch (Exception exp)
            {
                _context.Log(exp.Message).Wait();

                return BadRequest(exp.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<OperatorDto>> UpdateOperator([FromBody] OperatorDto dto)
        {
            try
            {
                OperatorDto? result = await _service.UpdateOperator(dto);

                return result is null
                    ? BadRequest("Не удалось обновить")
                    : Ok(result);
            }
            catch (Exception exp)
            {
                _context.Log(exp.Message).Wait();

                return BadRequest(exp.Message);
            }
        }
        
        [HttpDelete("delete/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<OperatorDto>> DeleteOperator([FromRoute(Name ="id")] Guid id)
        {
            try
            {
                bool result = await _service.DeleteOperator(id);

                return result
                    ? Ok(result)
                    : BadRequest("Не удалось удалить");
            }
            catch (Exception exp)
            {
                _context.Log(exp.Message).Wait();

                return BadRequest(exp.Message);
            }
        }
    }
}
