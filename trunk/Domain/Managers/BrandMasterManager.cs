using System;
using System.Collections.Generic;
using Domain.Entites;
using Domain.Interfaces;

namespace Domain.Managers
{
    public class BrandMasterManager
    {
        static readonly IDataBaseRepository repository = IocContainer.Resolve<IDataBaseRepository>();

        public static BrandMaster GetBrandMaster(long brandId)
        {
            try
            {
                return repository.GetBrand(brandId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static List<BrandMaster> GetAllBrandMaster()
        {
            try
            {
                return repository.GetAllBrands();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void AddBrandMaster(BrandMaster entity)
        {
            try
            {
                repository.AddBrand(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void UpdateBrand(BrandMaster entity)
        {
            try
            {
                repository.UpdateBrand(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DeleteBrand(long id)
        {
            try
            {
                repository.RemoveBrand(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void ActiveDeactiveCategory(long id, bool status)
        {
            try
            {
                repository.ActiveDeactiveCategory(id, status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
