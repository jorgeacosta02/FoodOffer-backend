using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using MySql.Data.MySqlClient;
using System.Text;

namespace FoodOffer.Repository
{
    public class CommerceRepository : ICommerceRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;

        public CommerceRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public List<Commerce> GetCommerces()
        {
            var commerces = _context.commerces.ToList();
            _context.Dispose();

            return _mapper.Map<List<Commerce>>(commerces);

        }

        public Commerce GetCommerce(int comId)
        {
            var commerce = _context.commerces.FirstOrDefault(com => com.com_id == comId);
            _context.Dispose();

            return _mapper.Map<Commerce>(commerce);

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


        public int SaveCommerceData(Commerce commerce)
        {
            try
            {
                commerce.DeleteDate = null;
                
                var data = _mapper.Map<Db_Commerce>(commerce);

                _context.commerces.Add(data);
                
                var id = _context.SaveChanges() == 1 ? data.com_id : 0;

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
            existingAdv.adv_ads_cod = advertising.StateCode;
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
