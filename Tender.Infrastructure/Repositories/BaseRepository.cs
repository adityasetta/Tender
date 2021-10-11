namespace Tender.Infrastructure.Repositories
{
    using System;
    using Tender.Domain.Repositories;
    using Tender.Infrastructure.Repositories.DataAccess;

    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The disposed flag.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="UserContext">The product context.</param>
        public BaseRepository(UserContext userContext)
        {
            this.UserContext = userContext;
        }

        /// <summary>
        /// Gets the product context.
        /// </summary>
        /// <value>
        /// The product context.
        /// </value>
        protected UserContext UserContext { get; }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">True if any managed resourced needs to be disposed, false otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.UserContext?.Dispose();
                this.disposed = true;
            }
        }
    }
}
