namespace Domain.Entities;

public class Medicine : EntityBase<Guid>
{
    public Medicine(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public void Update(string name)
    {
        if (Name.Equals(name) is not true) Name = name;
    }
}