using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Repository.Mapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper() 
        {
            //.ForMember(dest => dest.PropiedadDestino, opt => opt.MapFrom(src => src.PropiedadOrigen))
            CreateMap<User, Db_User>()
            .ForMember(a => a.usr_id, b => b.MapFrom(src => src.Id_User))
            .ForMember(a => a.usr_name, b => b.MapFrom(src => src.Name))
            .ForMember(a => a.usr_mail, b => b.MapFrom(src => src.Email))
            .ForMember(a => a.usr_phone, b => b.MapFrom(src => src.Phone))
            .ForMember(a => a.usr_cell_phone, b => b.MapFrom(src => src.Cell_Phone))
            .ForMember(a => a.usr_ide_cod, b => b.MapFrom(src => src.Id_Type))
            .ForMember(a => a.usr_ide_num, b => b.MapFrom(src => src.Id_Number))
            .ForMember(a => a.usr_ust_cod, b => b.MapFrom(src => src.Type))
            .ReverseMap();

            CreateMap<LoginData, Db_User_Login_Data>()
            .ForMember(a => a.uld_pwd, b => b.MapFrom(src => src.Password))
            .ForMember(a => a.uld_salt, b => b.MapFrom(src => src.Salt))
            .ReverseMap();

            CreateMap<Advertising, Db_Advertising>()
            .ForMember(a => a.adv_id, b => b.MapFrom(src => src.Id))
            .ForMember(a => a.adv_title, b => b.MapFrom(src => src.Title))
            .ForMember(a => a.adv_desc, b => b.MapFrom(src => src.Description))
            .ForMember(a => a.adv_price, b => b.MapFrom(src => src.Price))
            .ForMember(a => a.adv_cat_cod, b => b.MapFrom(src => src.Category.Code))
            .ForMember(a => a.adv_ads_cod, b => b.MapFrom(src => src.State.Code))
            .ForMember(a => a.adv_create_data, b => b.MapFrom(src => src.CreationDate))
            .ForMember(a => a.adv_update_data, b => b.MapFrom(src => src.UpdateDate))
            .ForMember(a => a.adv_delete_data, b => b.MapFrom(src => src.DeleteDate))
            .ReverseMap();
        }
    }
}
