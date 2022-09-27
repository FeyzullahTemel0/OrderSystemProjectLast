
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IWareHouseRepository : IEntityRepository<WareHouse>
    {

        Task<bool> ExistWareHouseByProductIdAndAmount(int productId,int amount,int Size); //***
        //Kontrol sonucunda eğer true ise diğer işlemi yapıyor.
        Task<List<WareHouseDto>> GetAllWareHouseDetailsDto();
            // Tüm wareHouseDetails'e ait tüm verileri liste şeklinde bana döndür dedik.
    }
}