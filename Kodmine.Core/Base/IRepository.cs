using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.Core.Base
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
