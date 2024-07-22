using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using MySql.Data.MySqlClient;
using System.Text;

namespace FoodOffer.Repository
{
    public class AdvertisingRepository : IAdvertisingRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;
        private readonly string s3Path = "https://s3.sa-east-1.amazonaws.com/clickfood/";

        public AdvertisingRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public List<Advertising> GetAdvertisings(Filter filter)
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
            query.AppendLine("LEFT JOIN advertising_attributes ON ada_adv_id = adv_id ");
            query.AppendLine("LEFT JOIN advertising_images ON adi_adv_id = adv_id AND adi_item = 1 ");
            query.AppendLine("WHERE ats_day IN (" + days + ") ");
            query.AppendLine("AND adv_ads_cod = 'A' ");
            query.AppendLine("AND adv_delete_data IS NULL ");

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
                query.AppendLine("AND (@hour < ats_end_1 AND ats_nextday_1 = 'Y')  OR (@hour < ats_end_2 AND ats_nextday_2 = 'Y') ");
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
                                adv.Category = new Category(Convert.ToInt16(reader["cat_cod"]), Convert.ToString(reader["cat_desc"]));
                                adv.State = new AdvertisingState(Convert.ToInt16(reader["ads_cod"]), Convert.ToString(reader["ads_desc"]));
                                var img = reader["adi_name"] != DBNull.Value ? true : false;
                                if(img)
                                    adv.Images.Add(new Image(adv.Id, 1, Convert.ToString(reader["adi_name"]), s3Path + Convert.ToString(reader["adi_path"]), null));
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

        public Advertising GetAdvertisingData(int id)
        {

            var Adv = _context.advertisings.FirstOrDefault(adv => adv.adv_id == id);
            _context.Dispose();

            return _mapper.Map<Advertising>(Adv);

        }

        public int SaveAdvertisingData(Advertising advertising)
        {
            advertising.CreationDate = DateTime.Now;
            advertising.UpdateDate = DateTime.Now;
            advertising.DeleteDate = null;
            var data = _mapper.Map<Db_Advertising>(advertising);

            var result = _context.advertisings.Add(data);
            if (_context.SaveChanges() == 1)
                return data.adv_id;
            return 0;
        }

        public bool UpdateAdvertisingData(Advertising advertising)
        {
            advertising.UpdateDate = DateTime.Now;
            advertising.DeleteDate = null;

            var data = _mapper.Map<Db_Advertising>(advertising);

            var result = _context.advertisings.Update(data);

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
