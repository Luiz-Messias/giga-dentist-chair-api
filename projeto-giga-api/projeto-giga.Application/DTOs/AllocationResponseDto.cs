namespace projeto_giga.Application.DTOs;

public class AllocationResponseDto
{
    public int Id { get; set; }
    public int ChairId { get; set; }
    public int ChairNumber { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}