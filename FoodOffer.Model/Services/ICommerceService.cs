

using FoodOffer.Model.Models;
using Microsoft.AspNetCore.Http;

namespace FoodOffer.Model.Services
{
    public interface ICommerceService
    {
        List<Commerce> GetCommerces();
        Commerce GetCommerce(int comID);
        Commerce GetCommerceComplete(int comID);
        Commerce AddCommerce(Commerce commerce);
        void SaveCommerceImage(int commerceId, IFormFile image);
    }
}
