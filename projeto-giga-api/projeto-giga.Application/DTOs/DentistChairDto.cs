namespace projeto_giga.Application.DTOs;

public class DentistChairDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
