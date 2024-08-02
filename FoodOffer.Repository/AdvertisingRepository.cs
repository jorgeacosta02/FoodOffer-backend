using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;

namespace FoodOffer.Repository
{
    public class AdvertisingRepository : IAdvertisingRepository
    {

        private readonly Func<ApplicationDbContext> _contextFactory;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;
        private readonly string s3Path = "https://s3.sa-east-1.amazonaws.com/clickfood/";

        public AdvertisingRepository(IDbConecction dbConecction, Func<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public List<Advertising> GetAdvertisings(AdvFilter filter)
        {
            DateTime now = DateTime.Now;
            short day = (short)now.DayOfWeek;
            string days = day.ToString();
            bool nextDay = false;
            TimeSpan nowHour = now.TimeOfDay;
            TimeSpan nightHalf = new TimeSpan(0, 0, 0);
            TimeSpan nightLimit = new TimeSpan(6, 0, 0);

            if(nowHour >  nightHalf && nowHour < nightLimit) 
            {
                days += $", {day - 1}";
                nextDay = true;
            }

            List<Advertising> advertisings = new List<Advertising>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM advertising_time_settings ");
            query.AppendLine("INNER JOIN advertisings ON adv_id = ats_adv_id ");
            query.AppendLine("INNER JOIN advertising_categories ON cat_cod = adv_cat_cod ");
            query.AppendLine("INNER JOIN advertising_states ON ads_cod = adv_ads_cod ");
            query.AppendLine("INNER JOIN commerces ON com_id = adv_com_id ");
            query.AppendLine("INNER JOIN addresses ON add_ref_id = com_id AND add_ref_type = 'C' ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("LEFT JOIN advertising_attributes ON ada_adv_id = adv_id ");
            query.AppendLine("LEFT JOIN advertising_images ON adi_adv_id = adv_id AND adi_item = 1 ");
            query.AppendLine("WHERE ats_day IN (" + days + ") ");
            query.AppendLine("AND adv_ads_cod = 1 ");
            query.AppendLine("AND adv_delete_date IS NULL ");

            if (filter.category != 0)
            {
                query.AppendLine($"AND adv_cat_cod = @category ");
            }

            if (filter.attributes != null && filter.attributes.Count() > 0)
            {
                var attributes = String.Join(",", filter.attributes);
                query.AppendLine($"AND ada_atr_cod IN ({attributes}) AND ada_value = 'Y' ");
            }

            if (!nextDay)
            {
                query.AppendLine("AND (@hour BETWEEN ats_start_1 AND ats_end_1) OR (@hour BETWEEN ats_start_2 AND ats_end_2) ");
            }
            else
            {
                query.AppendLine("AND (@hour < ats_end_1)  OR (@hour < ats_end_2) ");
            }


            query.AppendLine("GROUP BY adv_id ");

            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@hour", nowHour);
                    command.Parameters.AddWithValue("@category", filter.category);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var adv = new Advertising();
                                adv.Id = Convert.ToInt16(reader["adv_id"]);
                                adv.Title = Convert.ToString(reader["adv_title"]);
                                adv.Price = Convert.ToDouble(reader["adv_price"]);
                                adv.Description = Convert.ToString(reader["adv_desc"]);
                                adv.CategoryCode = Convert.ToInt16(reader["cat_cod"]);
                                adv.StateCode = Convert.ToInt16(reader["ads_cod"]);
                                adv.PriorityLevel = Convert.ToInt16(reader["adv_prl_cod"]);
                                adv.Commerce = new Commerce();
                                adv.Commerce.Id = Convert.ToInt16(reader["com_id"]);
                                adv.Commerce.Name = Convert.ToString(reader["com_name"]);
                                var address = new Address();
                                address.Name = Convert.ToString(reader["add_name"]);
                                address.Ref_Type = Convert.ToChar(reader["add_ref_type"]);
                                address.Description = Convert.ToString(reader["add_desc"]);
                                address.City.Code = Convert.ToInt16(reader["cit_cod"]);
                                address.City.Description = Convert.ToString(reader["cit_desc"]);
                                address.State.Code = Convert.ToInt16(reader["ste_cod"]);
                                address.State.Description = Convert.ToString(reader["ste_desc"]);
                                address.Country.Code = Convert.ToInt16(reader["cou_cod"]);
                                address.Country.Description = Convert.ToString(reader["cou_desc"]);
                                adv.Commerce.Addresses.Add(address);

                                var img = reader["adi_name"] != DBNull.Value ? true : false;
                                if(img)
                                    adv.Images.Add(new AppImage(adv.Id, 1, Convert.ToString(reader["adi_name"]), s3Path + Convert.ToString(reader["adi_path"]), null));
                                advertisings.Add(adv);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }


            return advertisings;

        }

        public Advertising GetAdvertising(int AdvId)
        {
            Advertising advertising;

            using(var context = _contextFactory())
            {
                var data = context.advertisings.FirstOrDefault(adv => adv.adv_id == AdvId);
                advertising = _mapper.Map<Advertising>(data);

            }

            return advertising;

        }

        public List<Advertising> GetAdvertisingsByCommerce(int comId)
        {
            List<Advertising> advertising;

            using (var context = _contextFactory())
            {
                var data = context.advertisings.Where(adv => adv.adv_com_id == comId && adv.adv_delete_date == null);
                advertising = _mapper.Map<List<Advertising>>(data);

            }

            return advertising;

        }

        public List<Address> GetAdvertisingAddress(Advertising Adv)
        {
            List<Address> addresses = new List<Address>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM advertisings ");
            query.AppendLine("INNER JOIN advertisings_address ON aad_adv_id = adv_id AND aad_adv_com_id = adv_com_id ");
            query.AppendLine("INNER JOIN addresses ON add_ref_id = adv_com_id AND add_ref_type = 'C' AND add_item = aad_add_item ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("WHERE adv_id = ? ");
            query.AppendLine("AND adv_com_id = ? ");

            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@adv_id", Adv.Id);
                    command.Parameters.AddWithValue("@adv_com_id", Adv.Commerce.Id);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new Address();
                                address.Ref_Id = Convert.ToInt32(reader["adv_com_id"]);
                                address.Name = Convert.ToString(reader["add_name"]);
                                address.Ref_Type = Convert.ToChar(reader["add_ref_type"]);
                                address.Description = Convert.ToString(reader["add_desc"]);
                                address.City.Code = Convert.ToInt16(reader["cit_cod"]);
                                address.City.Description = Convert.ToString(reader["cit_desc"]);
                                address.State.Code = Convert.ToInt16(reader["ste_cod"]);
                                address.State.Description = Convert.ToString(reader["ste_desc"]);
                                address.Country.Code = Convert.ToInt16(reader["cou_cod"]);
                                address.Country.Description = Convert.ToString(reader["cou_desc"]);
                                address.Obs = Convert.ToString(reader["add_obs"]);
                                addresses.Add(address);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return addresses;

        }


        public int SaveAdvertisingData(Advertising advertising)
        {
            try
            {
                advertising.CreationDate = DateTime.Now;
                advertising.UpdateDate = DateTime.Now;
                advertising.DeleteDate = null;
                
                var data = _mapper.Map<Db_Advertising>(advertising);
                int id = 0;

                using(var context = _contextFactory())
                {
                    context.advertisings.Add(data);
                    id = context.SaveChanges() == 1 ? data.adv_id : 0;
                }

                return id;

            }
            catch(Exception ex)
            {
                throw new Exception("Error saving adversiting data", ex);
            }

        }

        

        public bool UpdateAdvertisingData(Advertising advertising)
        {
            advertising.UpdateDate = DateTime.Now;
            advertising.DeleteDate = null;
            bool flag = true; 

            var data = _mapper.Map<Db_Advertising>(advertising);

            using(var context = _contextFactory())
            {
                context.advertisings.Update(data);
                flag = context.SaveChanges() == 1;
            }

            return flag;

        }

        public bool UpdateAdvertisingState(Advertising advertising)
        {
            bool flag = false;

            using(var context = _contextFactory())
            {

                var existingAdv = context.advertisings.FirstOrDefault(adv => adv.adv_id == advertising.Id);

                if (existingAdv == null)
                    throw new Exception($"No fue posible encontrar aviso con Id: {advertising.Id}");

                existingAdv.adv_update_date = DateTime.Now;
                existingAdv.adv_ads_cod = advertising.StateCode;
                flag = context.SaveChanges() == 1;

            }

            return flag;

        }

        public bool DeleteAdvertisingData(int id)
        {
            var flag = false;

            using(var context = _contextFactory())
            {
                var existingAdv = context.advertisings.FirstOrDefault(adv => adv.adv_id == id);

                if (existingAdv != null)
                {
                    existingAdv.adv_update_date = DateTime.Now;
                    existingAdv.adv_delete_date = DateTime.Now;

                    flag = context.SaveChanges() == 1;
                }
                else
                {
                    throw new Exception("Advertising not found.");
                }
            }

            return flag;

        }

        #region TimeSets

        public bool SaveAdvertisingTimeSet(List<AdvertisingTimeSet> sets)
        {
            bool flag = false;

            try
            {
                using (var context = _contextFactory())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var set in sets)
                            {
                                var data = _mapper.Map<Db_Advertising_Time_Set>(set);
                                context.advertising_time_settings.Add(data);
                            };

                            int affectedRows = context.SaveChanges();

                            if (affectedRows == sets.Count)
                            {
                                transaction.Commit();
                                flag = true;
                            }
                            else
                            {
                                transaction.Rollback();
                                flag = false;
                            }

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                }

                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving advertising timeset data", ex);
            }
        }

        public void DeleteAdvertisingTimeSet(int advId)
        {

            try
            {
                using (var context = _contextFactory())
                {
                    var timeSets = context.advertising_time_settings.Where(ats => ats.ats_adv_id == advId);

                    if (timeSets.Any())
                    {
                        context.advertising_time_settings.RemoveRange(timeSets);
                        context.SaveChanges();
                    }

                }

            }
            catch(Exception ex)
            {
                throw new Exception("Error deleting advertising time set", ex);
            }

        }

        #endregion

    }

}
