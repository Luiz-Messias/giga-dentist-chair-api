using projeto_giga.Application.Common;
using projeto_giga.Application.DTOs;
using projeto_giga.Application.Interfaces;
using projeto_giga.Domain.Entities;
using projeto_giga.Domain.Interfaces;

namespace projeto_giga.Application.Services;

public class AllocationService : IAllocationService
{
    private readonly IAllocationRepository allocationRepository;
    private readonly IDentistChairRepository dentistChairRepository;

    public AllocationService(IAllocationRepository allocationRepository, IDentistChairRepository dentistChairRepository)
    {
        this.allocationRepository = allocationRepository;
        this.dentistChairRepository = dentistChairRepository;
    }

    public ApiResponse<List<Allocation>> GetAllAllocations()
    {
        var allocations = allocationRepository.GetAllAllocations();
        return ApiResponse<List<Allocation>>.Ok(allocations);
    }

    public ApiResponse<AllocationResponseDto> AllocateChairsAutomatically(AllocateRequestDto dto)
    {
        try
        {
            if (dto.StartTime >= dto.EndTime)
                return ApiResponse<AllocationResponseDto>.Fail("Horário de início deve ser antes do fim.");

            var activeChairs = dentistChairRepository.GetAll();
            var allocationsInPeriod = allocationRepository.GetAllocationsInPeriod(dto.StartTime, dto.EndTime);
            var occupiedChairIds = allocationsInPeriod.Select(a => a.ChairId).ToHashSet();
            var availableChairs = activeChairs.Where(c => !occupiedChairIds.Contains(c.Id)).ToList();

            if (!availableChairs.Any())
                return ApiResponse<AllocationResponseDto>.Fail("Nenhuma cadeira disponível no período.");

            var last7Days = allocationRepository.GetAllocationsInLast7Days();
            var chosenChair = availableChairs.OrderBy(c => last7Days.Count(a => a.ChairId == c.Id)).First();

            var allocation = new Allocation(chosenChair.Id, dto.StartTime, dto.EndTime);
            allocationRepository.Add(allocation);

            var response = new AllocationResponseDto
            {
                ChairId = chosenChair.Id,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            return ApiResponse<AllocationResponseDto>.Ok(response, "Cadeira alocada com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<AllocationResponseDto>.Fail($"Erro ao alocar: {ex.Message}");
        }
    }
}