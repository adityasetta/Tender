namespace Tender.Domain.Repositories
{
    using System.Threading.Tasks;

    using Tender.Domain.Entities;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserById(string userId);
    }
}
