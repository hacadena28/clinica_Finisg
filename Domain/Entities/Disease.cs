namespace Domain.Entities;

public class Disease : EntityBase<Guid>
{
    public Disease(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public List<MedicalHistoryDisease> HistoryDiseases { get; set; }


    public void Update(string name)
    {
        if (Name.Equals(name) is not true) Name = name;
    }
}