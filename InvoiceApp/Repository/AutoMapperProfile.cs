using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.DTOS;

//AutoMapper profile
namespace InvoiceApp.Repository
{
    public class AutoMapperProfile : Profile
    {
        //The main rules of mapping between objects.
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>()
                .ForMember(destination => destination.Id, option => option.Ignore());
            CreateMap<InvoiceTerms, InvoiceTermsDTO>();
            CreateMap<InvoiceTermsDTO, InvoiceTerms>()
                .ForMember(destination => destination.Id, option => option.Ignore());
            CreateMap<InvoiceLineItem, InvoiceLineItemDTO>();
            CreateMap<InvoiceLineItemDTO, InvoiceLineItem>()
                .ForMember(destination => destination.Id, option => option.Ignore());
            CreateMap<Invoice, InvoiceDTO>()
               .ForMember(destination => destination.CustomerName, option => option.MapFrom(src => src.Customer!.Name))
               .ForMember(destination => destination.InvoiceTermsName, option => option.MapFrom(src => src.InvoiceTerms!.Name));
            CreateMap<InvoiceDTO, Invoice>()
                .ForMember(destination => destination.Id, option => option.Ignore());
        }
    }
}
