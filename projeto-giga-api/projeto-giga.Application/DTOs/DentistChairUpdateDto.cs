namespace projeto_giga.Application.DTOs
{
    public class DentistChairUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Number { get; set; }
        public bool IsActive { get; set; }
    }

}
