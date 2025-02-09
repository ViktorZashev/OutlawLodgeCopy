namespace DataLayer
{
    public class UserContext : IDb<User, Guid>
    {
        private readonly HotelDbContext hotelDbContext;
        public UserContext(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task CreateAsync(User entity)
        {
            try
            {
                await hotelDbContext.Users.AddAsync(entity);
                await hotelDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> ReadAsync(Guid key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = hotelDbContext.Users;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(e => e.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<User> query = hotelDbContext.Users;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User entity, bool useNavigationalProperties = false)
        {
            try
            {
                User userFromDb = await ReadAsync(entity.Id, false, false);

                if (userFromDb is null)
                {
                    throw new ArgumentException("User with id = " + entity.Id + " does not exist!");
                }

                hotelDbContext.Entry(userFromDb).CurrentValues.SetValues(entity);
                await hotelDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid key)
        {
            try
            {
                User user = await ReadAsync(key, false, false);

                if (user is null)
                {
                    throw new ArgumentException("User with id = " + key + " does not exist!");
                }

                hotelDbContext.Users.Remove(user);
                await hotelDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
