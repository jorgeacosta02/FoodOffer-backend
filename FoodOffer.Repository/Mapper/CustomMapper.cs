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

            CreateMap<AppImage, Db_Advertising_Image>()
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
            .ForMember(a => a.adv_cat_cod, b => b.MapFrom(src => src.CategoryCode))
            .ForMember(a => a.adv_com_id, b => b.MapFrom(src => src.Commerce.Id))
            .ForMember(a => a.adv_ads_cod, b => b.MapFrom(src => src.StateCode))
            .ForMember(a => a.adv_create_date, b => b.MapFrom(src => src.CreationDate))
            .ForMember(a => a.adv_update_date, b => b.MapFrom(src => src.UpdateDate))
            .ForMember(a => a.adv_delete_date, b => b.MapFrom(src => src.DeleteDate))
            .ForMember(a => a.adv_prl_cod, b => b.MapFrom(src => src.PriorityLevel))
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

            CreateMap<AttributeValue, Db_Attribute>()
            .ForMember(a => a.atr_cod, b => b.MapFrom(src => src.Id))
            .ForMember(a => a.atr_desc, b => b.MapFrom(src => src.Description))
            .ForMember(a => a.atr_atc_cod, b => b.MapFrom(src => src.Category))
            .ReverseMap();

            CreateMap<AttributeValue, Db_Advertising_Attribute>()
            .ForMember(a => a.ada_atr_cod, b => b.MapFrom(src => src.Id))
            .ForMember(a => a.ada_value, b => b.MapFrom(src => src.Value))
            .ReverseMap();

            CreateMap<Commerce, Db_Commerce>()
            .ForMember(a => a.com_id, b => b.MapFrom(src => src.Id))
            .ForMember(a => a.com_usr_id, b => b.MapFrom(src => src.OwnerId))
            .ForMember(a => a.com_name, b => b.MapFrom(src => src.Name))
            .ForMember(a => a.com_mail, b => b.MapFrom(src => src.Mail))
            .ForMember(a => a.com_phone, b => b.MapFrom(src => src.Phone))
            .ForMember(a => a.com_cell_phone, b => b.MapFrom(src => src.CellPhone))
            .ForMember(a => a.com_web_url, b => b.MapFrom(src => src.WebUrl))
            .ReverseMap();

            CreateMap<AdvertisingTimeSet, Db_Advertising_Time_Set>()
            .ForMember(a => a.ats_adv_id, b => b.MapFrom(src => src.AdvId))
            .ForMember(a => a.ats_day, b => b.MapFrom(src => src.Day))
            .ForMember(a => a.ats_start_1, b => b.MapFrom(src => src.Start1))
            .ForMember(a => a.ats_end_1, b => b.MapFrom(src => src.End1))
            .ForMember(a => a.ats_nextday_1, b => b.MapFrom(src => src.NextDay1))
            .ForMember(a => a.ats_start_2, b => b.MapFrom(src => src.Start2))
            .ForMember(a => a.ats_end_2, b => b.MapFrom(src => src.End2))
            .ForMember(a => a.ats_nextday_2, b => b.MapFrom(src => src.NextDay2))
            .ReverseMap();  
        }
    }
}
