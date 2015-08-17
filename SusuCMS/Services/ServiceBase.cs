using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SusuCMS.Services
{
    public class ServiceBase<TDataContext> where TDataContext : DbContext
    {
        protected TDataContext DataContext;

        public ServiceBase(TDataContext dataContext)
        {
            DataContext = dataContext;
        }

        protected void SaveChanges(bool submit = true)
        {
            if (submit)
            {
                DataContext.SaveChanges();
            }
        }
    }
}
