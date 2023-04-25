namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1 ,Name = "Sam" }
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();

            // The next two lines are required to map the AddCharacterReqDto to a Character object and set the Id property.
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;

            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResDto>();
            var character = characters.FirstOrDefault(character => character.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterResDto>(character);
            return serviceResponse;       
        }
    }
}