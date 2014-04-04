using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{
    public class ProductCategoryService : BaseService
    {
        #region "Public methods"

        public List<Domain.ProductCategory> GetAll()
        {
            var data = this.DBContext.GetAllProductCategory().ToList();
            if (null != data)
            {
                var entites = new List<Domain.ProductCategory>();
                foreach (var item in data)
                {
                    entites.Add(Translate(item));
                }
                return entites;
            }
            return null;
        }

        public Domain.ProductCategory GetProductCategory(string categoryCode)
        {
            return this.Translate(this.DBContext.GetProductCategory(categoryCode).FirstOrDefault());
        }

        public void Update(Domain.ProductCategory entity)
        {
            DatabaseAction action = string.IsNullOrWhiteSpace(entity.product_type_code) ? DatabaseAction.Insert : DatabaseAction.Update;
            try
            {
                this.DBContext.ManageProductCategory(entity.product_type_code, entity.parent_product_type_code, entity.product_type_description, entity.productTypeImageName, (int)DatabaseAction.Update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Create(Domain.ProductCategory entity)
        {
            try
            {
                this.DBContext.ManageProductCategory(entity.product_type_code, entity.parent_product_type_code, entity.product_type_description, entity.productTypeImageName, (int)DatabaseAction.Insert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProductCategory(string productTypeCode)
        {
            
                try
                {
                    this.DBContext.ManageProductCategory(productTypeCode, null, null, null, (int)DatabaseAction.Delete);
                   
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            
        }

      
        #endregion

        #region Private / Helper Methods

        private Domain.ProductCategory Translate(Data.EFData.ProductCategory entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.ProductCategory
            {
                parent_product_type_code = entity.parent_product_type_code,
                product_type_code = entity.product_type_code,
                product_type_description = entity.product_type_description,
                productTypeImageName = entity.productTypeImageName

            };
        }

        #endregion
    }
}
