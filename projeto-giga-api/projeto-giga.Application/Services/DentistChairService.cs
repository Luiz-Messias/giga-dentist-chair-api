using projeto_giga.Application.Common;
using projeto_giga.Application.DTOs;
using projeto_giga.Application.Interfaces;
using projeto_giga.Domain.Entities;
using projeto_giga.Domain.Interfaces;

namespace projeto_giga.Application.Services;

public class DentistChairService : IDentistChairService
{
    private readonly IDentistChairRepository dentistChairRepository;

    public DentistChairService(IDentistChairRepository dentistChairRepository)
    {
        this.dentistChairRepository = dentistChairRepository;
    }

    public ApiResponse<List<DentistChairDto>> GetAll()
    {
        var chairs = dentistChairRepository.GetAll();

        var dtos = chairs.Select(c => new DentistChairDto
        {
            Id = c.Id,
            Number = c.Number,
            Description = c.Description,
            IsActive = c.IsActive
        }).ToList();

        return ApiResponse<List<DentistChairDto>>.Ok(dtos);
    }

    public ApiResponse<string> Create(DentistChairCreateDto dto)
    {
        try
        {
            var chair = new DentistChair(0, dto.Number, dto.Description, dto.IsActive);
            dentistChairRepository.Create(chair);
            return ApiResponse<string>.Ok("Cadeira criada com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Fail($"Erro ao criar cadeira: {ex.Message}");
        }
    }

    public ApiResponse<DentistChairDto> GetDentistChairById(int id)
    {
        var chair = dentistChairRepository.GetById(id);
        if (chair == null)
            return ApiResponse<DentistChairDto>.Fail($"Cadeira com ID {id} não encontrada.");

        var dto = new DentistChairDto
        {
            Id = chair.Id,
            Number = chair.Number,
            Description = chair.Description,
            IsActive = chair.IsActive
        };

        return ApiResponse<DentistChairDto>.Ok(dto);
    }

    public ApiResponse<string> Update(int id, DentistChairUpdateDto dto)
    {
        try
        {
            var existing = dentistChairRepository.GetById(id);
            if (existing == null)
                return ApiResponse<string>.Fail($"Cadeira com ID {id} não encontrada.");

            if (string.IsNullOrWhiteSpace(dto.Description))
                return ApiResponse<string>.Fail("Descrição não pode ser vazia.");

            if (Convert.ToInt32(dto.Number) <= 0)
                return ApiResponse<string>.Fail("Número da cadeira deve ser maior que zero.");

            existing.SetActive(dto.IsActive);
            existing.SetDescription(dto.Description);
            existing.SetNumber(dto.Number);

            dentistChairRepository.Update(existing);
            return ApiResponse<string>.Ok("Cadeira atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Fail($"Erro ao atualizar: {ex.Message}");
        }
    }

    public ApiResponse<string> Delete(int id)
    {
        var existing = dentistChairRepository.GetById(id);
        if (existing == null)
            return ApiResponse<string>.Fail($"Cadeira com ID {id} não encontrada.");

        dentistChairRepository.Delete(id);
        return ApiResponse<string>.Ok("Cadeira removida com sucesso.");
    }
}