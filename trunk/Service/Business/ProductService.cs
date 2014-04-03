using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{
    public class ProductService : BaseService
    {
        #region "Public methods"

        public Domain.Product GetUser(int productID)
        {
            return this.Translate(this.DBContext.GetProduct(productID).FirstOrDefault());
        }

        public void SaveUpdateProduct(Domain.Product entity)
        {
            DatabaseAction action = entity.product_id == 0 ? DatabaseAction.Insert : DatabaseAction.Update;
            try
            {
                this.DBContext.ManageProduct(entity.product_id, entity.product_type_code, 
                    entity.product_name, entity.product_price, entity.product_color,
                    entity.product_size, entity.productImageName, entity.product_description,
                    entity.other_product_details, (int)action);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteProduct(int productId)
        {

            try
            {

                this.DBContext.ManageProduct(productId, null, null, null, null, null, null, null, null, (int)DatabaseAction.Delete);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

       
     

        #endregion

        #region Private / Helper Methods

        private Domain.Product Translate(Data.EFData.Product entity)
        {
            if (null == entity)
            {
                return null;
            }
            return new Domain.Product
            {
                other_product_details = entity.other_product_details,
                product_color = entity.product_color,
                product_description = entity.product_description,
                product_id = entity.product_id,
                product_name = entity.product_name,
                product_price = entity.product_price,
                product_size = entity.product_size,
                product_type_code = entity.product_type_code,
                productImageName = entity.productImageName
            };
        }

        #endregion
    }
}
