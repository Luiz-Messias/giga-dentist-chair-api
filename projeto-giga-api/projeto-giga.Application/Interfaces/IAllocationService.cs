using projeto_giga.Application.Common;
using projeto_giga.Application.DTOs;
using projeto_giga.Domain.Entities;

namespace projeto_giga.Application.Interfaces;

public interface IAllocationService
{
    ApiResponse<AllocationResponseDto> AllocateChairsAutomatically(AllocateRequestDto allocateRequestDto);
    ApiResponse<List<Allocation>> GetAllAllocations();
}
