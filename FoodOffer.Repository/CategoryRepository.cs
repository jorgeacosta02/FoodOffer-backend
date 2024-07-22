using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;

        public CategoryRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public short InsertCategory(Category category, short type)
        {

            switch (type)
            {
                case 1:
                    var data1 = _mapper.Map<Db_Attributes_Category>(category);
                    _context.attribute_categories.Add(data1);
                    if (_context.SaveChanges() == 1)
                        return data1.atc_cod;
                    break;

                case 2:
                    var data2 = _mapper.Map<Db_Advertising_Category>(category);
                    _context.advertising_categories.Add(data2);
                    if (_context.SaveChanges() == 1)
                        return data2.cat_cod;
                    break;

                case 3:
                    var data3 = _mapper.Map<Db_Advertising_State>(category);
                    _context.advertising_states.Add(data3);
                    if (_context.SaveChanges() == 1)
                        return data3.ads_cod;
                    break;

                case 4:
                    var data4 = _mapper.Map<Db_Commerce_Type>(category);
                    _context.commerce_types.Add(data4);
                    if (_context.SaveChanges() == 1)
                        return data4.cot_cod;
                    break;
                case 5:
                    var data5 = _mapper.Map<Db_Identification_Type>(category);
                    _context.identification_types.Add(data5);
                    if (_context.SaveChanges() == 1)
                        return data5.ide_cod;
                    break;
                case 6:
                    var data6 = _mapper.Map<Db_User_Type>(category);
                    _context.user_types.Add(data6);
                    if (_context.SaveChanges() == 1)
                        return data6.ust_cod;
                    break;

                default:

                    throw new ArgumentException("Invalid type", nameof(type));

            }


            return 0;
        }

        public bool UpdateCategory(Category category, short type)
        {
            switch (type)
            {
                case 1:
                    var data1 = _mapper.Map<Db_Attributes_Category>(category);
                    _context.attribute_categories.Update(data1);
                    break;

                case 2:
                    var data2 = _mapper.Map<Db_Advertising_Category>(category);
                    _context.advertising_categories.Update(data2);
                    break;

                case 3:
                    var data3 = _mapper.Map<Db_Advertising_State>(category);
                    _context.advertising_states.Update(data3);
                    break;

                case 4:
                    var data4 = _mapper.Map<Db_Commerce_Type>(category);
                    _context.commerce_types.Update(data4);
                    break;
                case 5:
                    var data5 = _mapper.Map<Db_Identification_Type>(category);
                    _context.identification_types.Update(data5);
                    break;
                case 6:
                    var data6 = _mapper.Map<Db_User_Type>(category);
                    _context.user_types.Update(data6);
                    break;

                default:

                    throw new ArgumentException("Invalid type", nameof(type));
            }

            return _context.SaveChanges() == 1;
        }

        public bool UpdateAdvertisingState(Advertising advertising)
        {
            var existingAdv = _context.advertisings.FirstOrDefault(adv => adv.adv_id == advertising.Id);

            if (existingAdv == null)
                throw new Exception($"No fue posible encontrar aviso con Id: {advertising.Id}");

            existingAdv.adv_update_date = DateTime.Now;
            existingAdv.adv_ads_cod = advertising.State.Code;
            return _context.SaveChanges() == 1;

        }

        public bool DeleteAdvertisingData(int id)
        {


            var flag = false;

            var existingAdv = _context.advertisings.FirstOrDefault(adv => adv.adv_id == id);

            if (existingAdv != null)
            {
                existingAdv.adv_update_date = DateTime.Now;
                existingAdv.adv_delete_date = DateTime.Now;

                flag = _context.SaveChanges() == 1;
            }
            else
            {
                throw new Exception("Advertising not found.");
            }

            return flag;

        }


    }
}
