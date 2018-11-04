using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;

namespace TestePratico.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<EstabelecimentoModel, EstabelecimentoViewModel>();
        }
    }
}