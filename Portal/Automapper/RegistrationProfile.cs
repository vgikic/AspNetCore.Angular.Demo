using Angular.Core.Models.BindingModel;
using Angular.Core.Models.Dto;
using AutoMapper;

namespace McD.MailJet.Portal.Automapper
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<ItemBindingModel, ItemDto>().ReverseMap();

        }
    }
}
