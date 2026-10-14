using AutoMapper;
using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;
using BarberShop.Domain.Entidades;

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
            .ForMember(destination => destination.forma_Pgmt, options => options.MapFrom(source => source.tipoPagamento))
            .ForMember(destination => destination.nome_Cliente, options => options.MapFrom(source => source.nomeCliente))
            .ForMember(destination => destination.nome_Barbeiro, options => options.MapFrom(source => source.nomeBarbeiro));
    }

    public void EntityToResponse()
    {
        CreateMap<Fatura, ResponseRegisterFaturaJson>();
        CreateMap<Fatura, ResponseShortFaturaJson>();
        CreateMap<Fatura, ResponseFaturaJson>()
            .ForMember(destination => destination.tipoPagamento, options => options.MapFrom(source => source.forma_Pgmt))
            .ForMember(destination => destination.nomeCliente, options => options.MapFrom(source => source.nome_Cliente))
            .ForMember(destination => destination.nomeBarbeiro, options => options.MapFrom(source => source.nome_Barbeiro))
            .ForMember(destination => destination.atualizadoEm, options => options.MapFrom(source => source.atualizado_Em));
    }
}
