using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadProject.Models.Repository
{
    interface IRepository<T , T2>  : IDisposable
    {
        void Add(T2 item);        
        T GetOne(int id);
        List<T> GetAll();
    }
}
