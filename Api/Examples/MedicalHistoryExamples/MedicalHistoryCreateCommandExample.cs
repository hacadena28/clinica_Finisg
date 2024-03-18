using Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;
using Domain.Entities;

namespace Api.Examples.MedicalHistoryExamples
{
    public class MedicalHistoryCreateCommandExample : IMultipleExamplesProvider<MedicalHistoryCreateCommand>
    {
        public IEnumerable<SwaggerExample<MedicalHistoryCreateCommand>> GetExamples()
        {
            var medicalHistoryCommand = new MedicalHistoryCreateCommand(
                DateTime.Now,
                "Dolor de muelas",
                new List<string>
                    { "Caries" }
                ,
                "limpieza",
                new Guid()
            );
            yield return SwaggerExample.Create("medicalHistoryCreateCommand", medicalHistoryCommand);
        }
    }
}