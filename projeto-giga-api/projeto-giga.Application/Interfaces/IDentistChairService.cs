using projeto_giga.Application.Common;
using projeto_giga.Application.DTOs;

namespace projeto_giga.Application.Interfaces;

public interface IDentistChairService
{
    ApiResponse<List<DentistChairDto>> GetAll();
    ApiResponse<DentistChairDto> GetDentistChairById(int id);
    ApiResponse<string> Create(DentistChairCreateDto dto);
    ApiResponse<string> Update(int id, DentistChairUpdateDto dto);
    ApiResponse<string> Delete(int id);
}