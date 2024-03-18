namespace Domain.Entities;

public class Treatment : EntityBase<Guid>
{
    public Treatment(string name, List<Medicine> medicines)
    {
        Name = name;
        Medicines = medicines;
    }

    public string Name { get; set; }
    public List<Medicine> Medicines { get; set; }

    public void Update(string name)
    {
        if (Name.Equals(name) is not true) Name = name;
    }

    public void UpdateListMedicines(List<Medicine> newListMedicines)
    {
        if (newListMedicines != null) Medicines = newListMedicines;
    }
}