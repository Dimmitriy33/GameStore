using BLL.Interfaces;
using DAL.EF;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

    
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            this._context.SaveChangesAsync();
        }
    }
}
