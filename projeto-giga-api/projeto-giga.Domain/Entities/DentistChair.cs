namespace projeto_giga.Domain.Entities;

public sealed class DentistChair : Entity
{
    public int Number { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public DentistChair(int id, int number, string description, bool isActive = true)
    {
        Id = id;
        Number = number;
        Description = description;
        IsActive = isActive;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetNumber(int number)
    {
        Number = number;
    }
}