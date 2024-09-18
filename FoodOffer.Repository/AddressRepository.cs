using AutoMapper;
using clasificados.Infraestructure.DbContextConfig.DbModels;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using MySql.Data.MySqlClient;
using System.Text;

namespace FoodOffer.Repository
{
    public class AddressRepository : IAddressRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;

        public AddressRepository(IDbConecction dbConecction, ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public List<Address> GetAddresses(int id, char type)
        {
            List<Address> addresses = new List<Address>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM addresses ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod AND ste_cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod AND cit_cou_cod = add_cou_cod AND cit_ste_cod = add_ste_cod ");
            query.AppendLine("WHERE add_ref_id = @id ");
            query.AppendLine("AND add_ref_type = @type ");

            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@type", type);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var add = new Address();
                                add.Ref_Id = Convert.ToInt16(reader["add_ref_id"]);
                                add.Ref_Type = Convert.ToChar(reader["add_ref_type"]);
                                add.Item = Convert.ToInt16(reader["add_item"]);
                                add.Description = Convert.ToString(reader["add_desc"]);
                                add.Obs = Convert.ToString(reader["add_obs"]);
                                add.City = new City(Convert.ToInt16(reader["cit_cod"]), Convert.ToString(reader["cit_desc"]));
                                add.State = new State(Convert.ToInt16(reader["ste_cod"]), Convert.ToString(reader["ste_desc"]));
                                add.Country = new Country(Convert.ToInt16(reader["cou_cod"]), Convert.ToString(reader["cou_desc"]));
                                addresses.Add(add);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error getting list of address for Id: {id} of type: {type}.", ex);
                    }
                }
            }

            return addresses;
        }

        public Address GetAddress(int id, char type)
        {

            Address address = null;

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM addresses ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod AND ste_cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod AND cit_cou_cod = add_cou_cod AND cit_ste_cod == add_ste_cod ");
            query.AppendLine("WHERE add_ref_id = @id ");
            query.AppendLine("AND add_ref_type = @type ");
            query.AppendLine("AND add_item = 1 ");

            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@type", type);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                address = new Address();
                                address.Ref_Id = Convert.ToInt16(reader["add_ref_id"]);
                                address.Ref_Type = Convert.ToChar(reader["add_ref_type"]);
                                address.Item = Convert.ToInt16(reader["add_item"]);
                                address.Description = Convert.ToString(reader["add_desc"]);
                                address.Obs = Convert.ToString(reader["add_obs"]);
                                address.City = new City(Convert.ToInt16(reader["cit_cod"]), Convert.ToString(reader["cit_desc"]));
                                address.State = new State(Convert.ToInt16(reader["ste_cod"]), Convert.ToString(reader["ste_desc"]));
                                address.Country = new Country(Convert.ToInt16(reader["cou_cod"]), Convert.ToString(reader["cou_desc"]));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error getting address for Id: {id} of type: {type}.", ex);
                    }
                }
            }

            return address;
        }


        public List<Address> GetAdvertisingAddresses(int advId)
        {

            List<Address> addresses = new List<Address>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM advertisings_address ");
            query.AppendLine("INNER JOIN addresses ON add_ref_id = aad_adv_com_id AND add_ref_type = 'C' AND add_item = add_add_item ");
            query.AppendLine("INNER JOIN countries ON cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN states ON ste_cod = add_ste_cod AND ste_cou_cod = add_cou_cod ");
            query.AppendLine("INNER JOIN cities ON cit_cod = add_cit_cod AND cit_cou_cod = add_cou_cod AND cit_ste_cod = add_ste_cod ");
            query.AppendLine("WHERE aad_adv_id = @advId ");

            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@advId", advId);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var address = new Address();
                                address.Ref_Id = Convert.ToInt16(reader["add_ref_id"]);
                                address.Ref_Type = Convert.ToChar(reader["add_ref_type"]);
                                address.Item = Convert.ToInt16(reader["add_item"]);
                                address.Name = Convert.ToString(reader["add_name"]);
                                address.Description = Convert.ToString(reader["add_desc"]);
                                address.Obs = Convert.ToString(reader["add_obs"]);
                                address.City = new City(Convert.ToInt16(reader["cit_cod"]), Convert.ToString(reader["cit_desc"]));
                                address.State = new State(Convert.ToInt16(reader["ste_cod"]), Convert.ToString(reader["ste_desc"]));
                                address.Country = new Country(Convert.ToInt16(reader["cou_cod"]), Convert.ToString(reader["cou_desc"]));
                                addresses.Add(address);
                            
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error getting adverting address - Advertising Id: {advId}.", ex);
                    }
                }
            }

            return addresses;
        }



        public bool SaveAddressData(Address address)
        {

            var data = _mapper.Map<Db_Address>(address);

            var result = _context.addresses.Add(data);
            return _context.SaveChanges() == 1;

        }

        public bool UpdateAddressData(Address address)
        {
            var data = _mapper.Map<Db_Address>(address);

            var result = _context.addresses.Update(data);

            return _context.SaveChanges() == 1;

        }

        public bool DeleteAddressData(Address address)
        {
            var data = _context.addresses.FirstOrDefault(add => add.add_ref_id == address.Ref_Id && add.add_ref_type == address.Ref_Type && add.add_item == address.Item);

            if (data == null)
                throw new Exception("Advertising not found.");

            _context.addresses.Remove(data);

           return _context.SaveChanges() == 1;

        }


    }
}
