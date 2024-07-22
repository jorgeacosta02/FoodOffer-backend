using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface IAdvertisingRepository
    {
        List<Advertising> GetAdvertisings(Filter filter);
        Advertising GetAdvertisingData(int id);
        int SaveAdvertisingData(Advertising advertising);
        bool UpdateAdvertisingData(Advertising advertising);

        bool UpdateAdvertisingState(Advertising advertising);
        bool DeleteAdvertisingData(int id);
    }
}
