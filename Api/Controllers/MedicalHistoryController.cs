using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Examples.MedicalHistoryExamples;
using Api.Filters;
using Application.Common.Helpers.Pagination;
using Application.UseCases.Appointments.Queries.GetAppointmentByPatientId;
using Application.UseCases.Appointments.Queries.GetAppointments;
using Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistory;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByDocumentNumber;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByID;
using Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByPatientId;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalHistory:ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalHistory(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [SwaggerRequestExample(typeof(MedicalHistoryCreateCommand), typeof(MedicalHistoryCreateCommandExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MedicalHistoryCreateResponseExample))]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MedicalHistoryDto), StatusCodes.Status200OK)]
    public async Task Create(MedicalHistoryCreateCommand command) => await _mediator.Send(command);

    [HttpGet]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MedicalHistoryDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<MedicalHistoryDto>> Get(int page = 1, int recordsPerPage = 20)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        var result = await _mediator.Send(new PaginationMedicalHistoryQuery
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
        var jsonString = JsonSerializer.Serialize(result, options);
        return result;
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MedicalHistoryDto), StatusCodes.Status200OK)]
    public async Task<MedicalHistoryDto> GetById(Guid id)
    {
        return await _mediator.Send(new MedicalHistoryByIdQuery(id));
    }

    [HttpGet("user/{id:guid}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MedicalHistoryDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<MedicalHistoryDto>> GetMedicalHistoryByPatinetId(Guid id, int page = 1,
        int recordsPerPage = 20)
    {
        return await _mediator.Send(new MedicalHistoryByPatientIdQuery(id)
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
    }

    [HttpGet("user/documentnumber/{documentNumber}")]
    [SwaggerResponseExample(400, typeof(ErrorResponse))]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MedicalHistoryByDocumentNumberDto), StatusCodes.Status200OK)]
    public async Task<ResponsePagination<MedicalHistoryByDocumentNumberDto>> GetMedicalHistoryByDocumentNumber(
        string documentNumber, int page = 1,
        int recordsPerPage = 20)
    {
        return await _mediator.Send(new MedicalHistoryByDocumentNumberQuery(documentNumber)
        {
            Page = page,
            RecordsPerPage = recordsPerPage
        });
    }
}