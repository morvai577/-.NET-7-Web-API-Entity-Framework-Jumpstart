namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterResDto>>> AddCharacter(AddCharacterReqDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(_mapper.Map<Character>(newCharacter)); // 1. The Add method adds the new character to the Characters DbSet. It is not added to the database until the SaveChanges method is called - hence why AddAsync is not used here.
            await _context.SaveChangesAsync(); // 2. The SaveChanges method persists the data to the database. It returns the number of rows affected.

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterResDto>(dbCharacter);
            return serviceResponse;       
        }

        public async Task<ServiceResponse<GetCharacterResDto>> UpdateCharacter(UpdateCharacterReqDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(character => character.Id == updatedCharacter.Id);
                
                if (character is null)
                {
                    throw new Exception($"Character with id {updatedCharacter.Id} not found.");
                }

                _mapper.Map(updatedCharacter, character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterResDto>(character);
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }            
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(character => character.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with id {id} not found.");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResDto>(c)).ToListAsync();
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }            
           
            return serviceResponse;
        }
    }
}