using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.Diseases.Commands.DiseaseDelete
{
    public class DiseaseDeleteCommandHandler : IRequestHandler<DiseaseDeleteCommand>
    {
        private readonly DiseaseService _diseaseService;
        private readonly IGenericRepository<Disease> _diseaseRepository;


        public DiseaseDeleteCommandHandler(DiseaseService diseaseService,
            IGenericRepository<Disease> diseaseRepository)
        {
            _diseaseService = diseaseService ?? throw new ArgumentNullException(nameof(diseaseService));
            _diseaseRepository = diseaseRepository ?? throw new ArgumentNullException(nameof(diseaseRepository));
        }

        public async Task<Unit> Handle(DiseaseDeleteCommand request, CancellationToken cancellationToken)
        {
            var disease = await _diseaseRepository.GetByIdAsync(request.Id);
            await _diseaseService.DeleteDisease(disease);
            return Unit.Value;
        }
    }
}