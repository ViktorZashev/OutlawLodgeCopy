namespace DataLayer
{
    public class ClientContext : IDb<Client, Guid>
    {
        private readonly HotelDbContext hotelDbContext;
        public ClientContext(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async Task CreateAsync(Client entity)
        {
            try
            {
                hotelDbContext.Clients.Add(entity);
                hotelDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Client> ReadAsync(Guid key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Client> query = hotelDbContext.Clients;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return query.SingleOrDefault(e => e.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Client>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Client> query = hotelDbContext.Clients;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Client entity, bool useNavigationalProperties = false)
        {
            try
            {
                Client clientFromDb = await ReadAsync(entity.Id, false, false);

                if (clientFromDb is null)
                {
                    throw new ArgumentException("Client with id = " + entity.Id + "does not exist!");
                }
                
                hotelDbContext.Entry(clientFromDb).CurrentValues.SetValues(entity);
                hotelDbContext.SaveChanges();
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
                Client client = await ReadAsync(key, false, false);

                if (client is null)
                {
                    throw new ArgumentException("Client with id = " + key + "does not exist!");
                }

                hotelDbContext.Clients.Remove(client);
                hotelDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
