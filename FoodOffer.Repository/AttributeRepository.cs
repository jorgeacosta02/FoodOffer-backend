using AutoMapper;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;
using MySql.Data.MySqlClient;
using System.Text;

namespace FoodOffer.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly Func<ApplicationDbContext> _contextFactory;
        private readonly IDbConecction _session;
        private readonly IMapper _mapper;

        public AttributeRepository(IDbConecction dbConecction, Func<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            _session = dbConecction ?? throw new ArgumentNullException(nameof(dbConecction));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public List<AttributeValue> GetAttributesByCategory(short atc)
        {
            List<AttributeValue> attributes = new List<AttributeValue>();

            try
            {
                using(var context = _contextFactory())
                {
                    var data = context.attributes.Where(atr => atr.atr_atc_cod == atc).ToList();
                    attributes = _mapper.Map<List<AttributeValue>>(data);
                }

            }
            catch (Exception ex) 
            {
                var exc = new Exception("Error getting attributes by category", ex);
                exc.Data.Add("Category", atc);
                throw exc;
            }

            return attributes;
        }

        public List<AttributeValue> GetAdvertisingsAttribute(int advId)
        {
            List<AttributeValue> attributes = new List<AttributeValue>();

            var query = new StringBuilder();
            query.AppendLine("SELECT * FROM advertising_attributes ");
            query.AppendLine("INNER JOIN attributes ON atr_cod = ada_atr_cod AND atr_atc_cod = 1 AND ada_value = 'Y' ");
            query.AppendLine("WHERE ada_adv_id = @id ");


            using (var connection = _session.GetConnection())
            {
                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@id", advId);

                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var atr = new AttributeValue();
                                atr.Id = Convert.ToInt16(reader["atr_cod"]);
                                atr.Description = Convert.ToString(reader["atr_desc"]);
                                atr.Value = Convert.ToChar(reader["ada_value"]);
                                attributes.Add(atr);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error getting attributes for advertising Id: {advId}.", ex);
                    }
                }
            }

            return attributes;
        }



    }
}
