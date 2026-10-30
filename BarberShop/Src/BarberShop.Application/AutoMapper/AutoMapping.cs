using BarberShop.Communication.Request.User;
using BarberShop.Communication.Response.Users;

namespace BarberShop.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    public void RequestToEntity()
    {
        CreateMap<RequestFaturasJson, Fatura>()
            .ForMember(destino => destino.Tags, config => config.MapFrom(source => source.Tags.Distinct()));
        CreateMap<RequestUserJson, Usuario>()
            .ForMember(destino => destino.Password, config => config.Ignore());
        CreateMap<Communication.Enums.Tags, Tag>()
            .ForMember(destino => destino.Value, config => config.MapFrom(source => source));
    }

    public void EntityToResponse()
    {
        CreateMap<Fatura, ResponseRegisterFaturaJson>();
        CreateMap<Fatura, ResponseShortFaturaJson>();
        CreateMap<Fatura, ResponseFaturaJson>()
            .ForMember(destino => destino.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));
        CreateMap<Usuario, ResponseUserJson>();
    }

}
