
namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto newCharacter);
    }
}