using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;

namespace FoodOffer.Repository
{
    public class AdvertisingRepository : IAdvertisingRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;

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
            query.AppendLine("LEFT JOIN advertising_attributes ON ada_adv_id = adv_id ");
            query.AppendLine("WHERE ats_day IN (" + days + ") ");
            query.AppendLine("AND adv_ads_cod = 'A' ");
            query.AppendLine("AND adv_delete_data IS NULL ");

            if (filter.category != null && filter.category != 0)
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


    }
}
