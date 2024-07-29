﻿using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface IImagesRepository
    {
        bool SaveImageData(Image image, char type);
        bool DeleteImageData(int advId, short item);
        Image GetAdvertisingImage(int AdvId);
    }
}
