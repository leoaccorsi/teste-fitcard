using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestePraticoModel.Model;
using TestePraticoModel.ViewModel;

namespace TestePratico.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EstabelecimentoViewModel, EstabelecimentoModel>();
        }
    }
}