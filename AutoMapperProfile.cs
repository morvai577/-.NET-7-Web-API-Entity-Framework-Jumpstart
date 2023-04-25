namespace dotnet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResDto>();
            CreateMap<AddCharacterReqDto, Character>();
        }
    }
}