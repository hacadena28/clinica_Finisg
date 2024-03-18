using Application.UseCases.Diseases.Queries.GetDisease;

namespace Api.Examples.DiseaseExamples
{
    public class GetDiseaseQueryExample : IMultipleExamplesProvider<PaginationDiseaseQuery>
    {
        public IEnumerable<SwaggerExample<PaginationDiseaseQuery>> GetExamples()
        {
            var diseaseQuery = new PaginationDiseaseQuery();
            yield return SwaggerExample.Create("diseaseQuery", diseaseQuery);
        }
    }
}