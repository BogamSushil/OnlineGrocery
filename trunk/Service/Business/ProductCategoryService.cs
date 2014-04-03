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



        public Domain.ProductCategory GetProductCategory(string categoryCode)
        {
            return this.Translate(this.DBContext.GetProductCategory(categoryCode).FirstOrDefault());
        }

        public void SaveUpdateUser(Domain.ProductCategory entity)
        {
            DatabaseAction action = string.IsNullOrWhiteSpace(entity.product_type_code) ? DatabaseAction.Insert : DatabaseAction.Update;
            try
            {
                this.DBContext.ManageProductCategory(entity.product_type_code, entity.parent_product_type_code, entity.product_type_description, entity.productTypeImageName, (int)action);
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
