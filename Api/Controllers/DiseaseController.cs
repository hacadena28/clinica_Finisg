using Api.Examples.DiseaseExamples;
using Api.Filters;
using Application;
using Application.Common.Exceptions;
using Application.Common.Helpers.Pagination;
using Application.UseCases.Diseases.Commands.DiseaseCreate;
using Application.UseCases.Diseases.Commands.DiseaseDelete;
using Application.UseCases.Diseases.Commands.DiseaseUpdate;
using Application.UseCases.Diseases.Queries.GetDisease;
using Application.UseCases.Diseases.Queries.GetDiseaseByID;
using Application.UseCases.Diseases.Queries.GetDiseaseByName;
using Application.UseCases.Diseases.Queries.GetDiseaseNormal;
using Application.UseCases.Users.Queries.GetPaginationUser;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiseaseController
{
    private readonly IMediator _mediator;

    public DiseaseController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [SwaggerRequestExample(typeof(DiseaseCreateCommand), typeof(DiseaseCreateCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DiseaseCreateResponseExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DiseaseDto), StatusCodes.Status200OK)]
    public async Task Create(DiseaseCreateCommand command) => await _mediator.Send(command);

    [HttpGet]
    [SwaggerRequestExample(typeof(PaginationDiseaseQuery), typeof(GetDiseaseQueryExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DiseaseDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<DiseaseDto>> Get(int page = 1, int recordsPerPage = 20)
    {
        return await _mediator.Send(new PaginationDiseaseQuery
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DiseaseDto), StatusCodes.Status200OK)]
    public async Task<DiseaseDto> GetById(Guid id)
    {
        return await _mediator.Send(new DiseaseByIdQuery(id));
    }

    [HttpGet("name/{name}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<DiseaseDto> GetByName(string name)
    {
        return await _mediator.Send(new DiseaseByNameQuery(name));
    }

    [HttpGet("all")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IEnumerable<DiseaseNormalDto>> GetAll()
    {
        return await _mediator.Send(new GetAllDiseaseQuery());
    }


    [HttpPut("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DiseaseDto), StatusCodes.Status200OK)]
    public async Task ChangeState(DiseaseUpdateCommand command, Guid id)
    {
        if (id != command.Id)
        {
            throw new ConflictException(Messages.IdDoNotMatch);
        }

        await _mediator.Send(command);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DiseaseDto), StatusCodes.Status200OK)]
    public async Task Delete(Guid id) => await _mediator.Send(new DiseaseDeleteCommand(id));
}