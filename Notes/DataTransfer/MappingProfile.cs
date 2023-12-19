using AutoMapper;
using Notes.DataTransfer.Input.UserDataTransfer;
using Notes.Identity;

namespace Notes.DataTransfer;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<User, UserInputRegister>().ReverseMap();
    }
}
