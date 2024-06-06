using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Services
{
    public interface IAdvertisingService
    {
        Advertising GetAdvertising(int Id);
        List<Advertising> GetAdvertisings(Filter filter);
        Advertising CreateAdvertising(Advertising advertising);
        Advertising UpdateAdvertising(Advertising advertising, bool stateOnly);
        bool DeleteAdvertising(int id);
    }
}
