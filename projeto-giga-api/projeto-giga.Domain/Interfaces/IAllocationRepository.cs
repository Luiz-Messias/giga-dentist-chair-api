
using projeto_giga.Domain.Entities;

namespace projeto_giga.Domain.Interfaces;

public interface IAllocationRepository
{
    void Add(Allocation allocation);
    List<Allocation> GetAllocationsInPeriod(DateTime start, DateTime end);
    List<Allocation> GetAllocationsInLast7Days();
    List<Allocation> GetAllAllocations();
}
