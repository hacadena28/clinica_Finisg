using Application.UseCases.Diseases.Commands.DiseaseUpdate;

namespace Api.Examples.DiseaseExamples
{
    public class DiseaseUpdateCommandExample : IMultipleExamplesProvider<DiseaseUpdateCommand>
    {
        public IEnumerable<SwaggerExample<DiseaseUpdateCommand>> GetExamples()
        {
            var diseaseCommand = new DiseaseUpdateCommand(
                new Guid(),
                "Halitosis"
            );
            yield return SwaggerExample.Create("diseaseUpdateCommand", diseaseCommand);
        }
    }
}