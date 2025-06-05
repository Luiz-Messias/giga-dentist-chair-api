using projeto_giga.Domain.Entities;

namespace projeto_giga.Domain.Interfaces;

public interface IDentistChairRepository
{
    List<DentistChair> GetAll();
    DentistChair? GetById(int id);
    void Create(DentistChair chair);
    void Update(DentistChair chair);
    void Delete(int id);
}
