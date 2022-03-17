using BenivoNetwork.DAL.Interfaces;
using System;

namespace BenivoNetwork.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BenivoNetworkEntities _context;

        public IUserRepository UserRepository { get; set; }
        public IPostRepository PostRepository { get; set; }
        public IMessageRepository MessageRepository { get; set; }

        public UnitOfWork()
        {
            _context = new BenivoNetworkEntities();

            UserRepository = new UserRepository(_context);
            PostRepository = new PostRepository(_context);
            MessageRepository = new MessageRepository(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
