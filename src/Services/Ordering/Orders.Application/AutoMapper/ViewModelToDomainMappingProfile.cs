using AutoMapper;
using Orders.Application.DTO;
using Orders.Domain.Command;
using Orders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateOrderDTO, CreateOrderCommand>();
            CreateMap<OrderDetailsDTO, OrdersDetail>();
            //CreateMap<CreateOrderDTO, OrdersInfo>();
        }
    }
}
