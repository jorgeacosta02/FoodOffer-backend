using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface IImagesRepository
    {
        bool SaveImageData(AppImage image, char type);
        bool DeleteImageData(int advId, short item);
        AppImage GetAdvertisingImage(int AdvId);
        AppImage GetCommerceImage(int ComId);
    }
}
