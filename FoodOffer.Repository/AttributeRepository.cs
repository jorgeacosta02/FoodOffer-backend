using AutoMapper;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Models;
using FoodOffer.Model.Repositories;

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



    }
}
