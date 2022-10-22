using AutoMapper;
using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.Mappings
{
    public class DTOtoCommandMappingProfile : Profile
    {
        public DTOtoCommandMappingProfile()
        {
            CreateMap<ProductDTO, ProductCreateCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
