using Api.Examples.UserExamples;
using Api.Filters;
using Application;
using Application.Common.Exceptions;
using Application.Common.Helpers.Pagination;
using Application.UseCases.Auth.Commands.Authentication;
using Application.UseCases.Auth.Queries;
using Application.UseCases.Users.Commands.UserCreateAdmin;
using Application.UseCases.Users.Commands.UserCreateDoctor;
using Application.UseCases.Users.Commands.UserUpdate;
using Application.UseCases.Users.Commands.UserCreatePatient;
using Application.UseCases.Users.Commands.UserDelete;
using Application.UseCases.Users.Commands.UserRecoveryPassword;
using Application.UseCases.Users.Queries.GetPaginationUser;
using Application.UseCases.Users.Queries.GetUserByDocumentNumber;
using Application.UseCases.Users.Queries.GetUserByID;
using Application.UseCases.Users.Queries.GetUserByRol;
using Application.UseCases.Users.Queries.GetUserPaginationByRol;
using Domain.Enums;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator) => _mediator = mediator;


    [HttpPost("Auth")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<AuthenticationDto> Authentication(AuthenticationCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("patient")]
    [SwaggerRequestExample(typeof(UserCreatePatientCommand), typeof(UserCreatePatientCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserCreateResponseExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task CreateUserPatient(UserCreatePatientCommand command)
    {
        await _mediator.Send(command);
    }

    [HttpPost("admin")]
    [SwaggerRequestExample(typeof(UserCreateAdminCommand), typeof(UserCreateAdminCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserCreateResponseExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task CreateUserAdmin(UserCreateAdminCommand command)
    {
        await _mediator.Send(command);
    }


    [HttpPost("doctor")]
    [SwaggerRequestExample(typeof(UserCreateDoctorCommand), typeof(UserCreateDoctorCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserCreateResponseExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task CreateUserDoctor(UserCreateDoctorCommand command)
    {
        await _mediator.Send(command);
    }

    [HttpGet]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<UserDto>> Get(int page = 1, int recordsPerPage = 20)
    {
        return await _mediator.Send(new PaginationUserQuery
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
    }

    [HttpGet("filter/{role}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<UserDto>> GetPaginationByRol(Role role, int page = 1, int recordsPerPage = 20)
    {
        return await _mediator.Send(new UserPaginatedByRolQuery(role)
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<UserDto> GetById(Guid id)
    {
        return await _mediator.Send(new UserByIdQuery(id));
    }

    [HttpGet("documentNumber/{documentNumber},{role}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<UserDto> GetByDocumentNumber(string documentNumber, Role role)
    {
        return await _mediator.Send(new UserByDocumentNumberQuery(documentNumber, role));
    }

    [HttpPut("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task Update(UserUpdateCommand command, Guid id)
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
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task Delete(Guid id) => await _mediator.Send(new UserDeleteCommand(id)); 
    
    [HttpGet("recovery-password/{documentNumber}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task RecoveryPassword(string documentNumber) => await _mediator.Send(new UserRecoveryPasswordCommand(documentNumber));
}