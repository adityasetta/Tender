namespace Tender.Domain.Repositories
{
    using System;

    /// <summary>
    /// Base interface for all repositories
    /// </summary>
    /// <typeparam name="T">Type of repository</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository<T> : IDisposable where T : class
    {
    }
}
