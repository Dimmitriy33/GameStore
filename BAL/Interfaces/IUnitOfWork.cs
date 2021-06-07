using System;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void SaveChangesAsync();
    }
}