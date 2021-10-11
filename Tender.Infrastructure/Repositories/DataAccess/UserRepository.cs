namespace Tender.Infrastructure.Repositories.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;

    using Tender.Domain.Entities;
    using Tender.Domain.Repositories;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext) : base(userContext)
        {
            this.userContext = userContext;
        }

        public async Task<User> GetUserById(string userId) => await userContext.User.Where(x=>x.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}
