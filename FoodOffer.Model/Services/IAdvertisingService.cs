using FoodOffer.Model.Models;

namespace FoodOffer.Model.Services
{
    public interface IAdvertisingService
    {
        Advertising GetAdvertisingDetail(int Id);
        Advertising GetAdvertisingSimple(int Id);
        List<Advertising> GetAdvertisings(AdvFilter filter);
        List<Advertising> GetAdvertisingsByCommerce(int Id);
        Advertising CreateAdvertising(Advertising advertising);
        Advertising UpdateAdvertisingData(Advertising advertising);
        Advertising UpdateAdvertisingImages(Advertising advertising);
        bool UpdateAdvertisingState(Advertising advertising);
        bool DeleteAdvertising(int id);
        Advertising GetAdvertisingSetting(int Id);
        bool SetAdvertisingTimeSet(List<AdvertisingTimeSet> timeSets, int advId);
    }
}
