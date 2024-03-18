
using Application.UseCases.Diseases.Commands.DiseaseCreate;

namespace Api.Examples.DiseaseExamples
{
    public class DiseaseCreateCommandExample : IMultipleExamplesProvider<DiseaseCreateCommand>
    {
        public IEnumerable<SwaggerExample<DiseaseCreateCommand>> GetExamples()
        {
            var diseaseCommand = new DiseaseCreateCommand(
                "Caries"
            );
            yield return SwaggerExample.Create("diseaseCreateCommand", diseaseCommand);
        }
    }
}