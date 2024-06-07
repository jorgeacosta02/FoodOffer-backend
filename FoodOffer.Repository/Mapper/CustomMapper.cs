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

            CreateMap<Image, Db_Advertising_Image>()
            .ForMember(a => a.adi_adv_id, b => b.MapFrom(src => src.ReferenceId))
            .ForMember(a => a.adi_item, b => b.MapFrom(src => src.Item))
            .ForMember(a => a.adi_path, b => b.MapFrom(src => src.Path))
            .ForMember(a => a.adi_name, b => b.MapFrom(src => src.Name))
            .ReverseMap();

            CreateMap<Advertising, Db_Advertising>()
            .ForMember(a => a.adv_id, b => b.MapFrom(src => src.Id))
            .ForMember(a => a.adv_title, b => b.MapFrom(src => src.Title))
            .ForMember(a => a.adv_desc, b => b.MapFrom(src => src.Description))
            .ForMember(a => a.adv_price, b => b.MapFrom(src => src.Price))
            .ForMember(a => a.adv_cat_cod, b => b.MapFrom(src => src.Category.Code))
            .ForMember(a => a.adv_com_id, b => b.MapFrom(src => src.Commerce.Id))
            .ForMember(a => a.adv_ads_cod, b => b.MapFrom(src => src.State.Code))
            .ForMember(a => a.adv_create_date, b => b.MapFrom(src => src.CreationDate))
            .ForMember(a => a.adv_update_date, b => b.MapFrom(src => src.UpdateDate))
            .ForMember(a => a.adv_delete_date, b => b.MapFrom(src => src.DeleteDate))
            .ReverseMap();

            CreateMap<Category, Db_Attributes_Category>()
            .ForMember(a => a.atc_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.atc_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();

            CreateMap<Category, Db_Advertising_Category>()
            .ForMember(a => a.cat_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.cat_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();

            CreateMap<Category, Db_Advertising_State>()
            .ForMember(a => a.ads_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.ads_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();

            CreateMap<Category, Db_Commerce_Type>()
            .ForMember(a => a.cot_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.cot_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();

            CreateMap<Category, Db_Identification_Type>()
            .ForMember(a => a.ide_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.ide_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();

            CreateMap<Category, Db_User_Type>()
            .ForMember(a => a.ust_cod, b => b.MapFrom(src => src.Code))
            .ForMember(a => a.ust_desc, b => b.MapFrom(src => src.Description))
            .ReverseMap();
        }
    }
}
