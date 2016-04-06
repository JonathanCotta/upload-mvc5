using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UploadProject.Models.Repository
{
    public abstract class Repository<T , T2> : IRepository<T , T2>
    {
        #region Constructor and Variables
        protected UploadContext db;
        
        private bool disposed = false;

        public Repository(UploadContext database)
        {
            db = database;
        }

        #endregion

        #region CrudMethods
        public abstract void Add(T2 item);
        protected abstract T Create(T2 item);        
        public abstract T GetOne(int id);
        public abstract List<T> GetAll(); 
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispode(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispode(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    db.Dispose();
        }
        #endregion  
    }
}