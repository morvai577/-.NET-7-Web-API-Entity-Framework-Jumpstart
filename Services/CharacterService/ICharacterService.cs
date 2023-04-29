
namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResDto>>> GetAllCharacters(int userId);
        Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto newCharacter);
        Task<ServiceResponse<GetCharacterResDto>> UpdateCharacter(UpdateCharacterReqDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterResDto>>> DeleteCharacter(int id);
    }
}