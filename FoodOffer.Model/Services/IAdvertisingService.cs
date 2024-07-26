using FoodOffer.Model.Models;

namespace FoodOffer.Model.Services
{
    public interface IAdvertisingService
    {
        Advertising GetAdvertising(int Id);
        List<Advertising> GetAdvertisings(AdvFilter filter);
        Advertising CreateAdvertising(Advertising advertising);
        Advertising UpdateAdvertising(Advertising advertising);
        bool UpdateAdvertisingState(Advertising advertising);
        bool DeleteAdvertising(int id);
    }
}
