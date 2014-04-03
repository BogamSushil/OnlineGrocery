using Data.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Business
{
    public class BaseService : IBaseService
    {
        private OnlineGroceryEntities mDbContext;
        public BaseService()
        {
            DBContext.Configuration.ProxyCreationEnabled = false;
        }
        public OnlineGroceryEntities DBContext
        {
            get
            {
                return (null == mDbContext) ? mDbContext = new OnlineGroceryEntities() : mDbContext;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public interface IBaseService
    {
         OnlineGroceryEntities DBContext { get; set; }
    }

    public enum DatabaseAction
    {
        None,
        Insert,
        Update,
        Delete,
        Get,
        GetAll,
        DeleteAll,
        ActiveDeactive
    }
}
