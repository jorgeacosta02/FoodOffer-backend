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
        List<Advertising> GetAdvertisings(AdvFilter filter);
        Advertising GetAdvertising(int AdvId);
        List<Advertising> GetAdvertisingsByCommerce(int comId);
        int SaveAdvertisingData(Advertising advertising);
        bool UpdateAdvertisingData(Advertising advertising);
        List<Address> GetAdvertisingAddress(Advertising Adv);
        bool UpdateAdvertisingState(Advertising advertising);
        bool DeleteAdvertisingData(int id);
        bool SaveAdvertisingTimeSet(List<AdvertisingTimeSet> sets);
        void DeleteAdvertisingTimeSet(int advId);

    }
}
