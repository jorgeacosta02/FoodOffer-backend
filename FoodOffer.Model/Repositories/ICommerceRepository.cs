using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface ICommerceRepository
    {
        Commerce GetCommerce(int comId);
        int SaveCommerceData(Commerce commerce);
    }
}
