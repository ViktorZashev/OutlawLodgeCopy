namespace DataLayer
{
    public class RoomContext : IDb<Room, Guid>
    {
        private readonly HotelDbContext hotelDbContext;
        public RoomContext(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task CreateAsync(Room entity)
        {
            try
            {
                await hotelDbContext.Rooms.AddAsync(entity);
                await hotelDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Room> ReadAsync(Guid key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Room> query = hotelDbContext.Rooms;

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

        public async Task<List<Room>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Room> query = hotelDbContext.Rooms;

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

        public async Task UpdateAsync(Room entity, bool useNavigationalProperties = false)
        {
            try
            {
                Room roomFromDb = await ReadAsync(entity.Id, false, false);

                if (roomFromDb is null)
                {
                    throw new ArgumentException("Room with id = " + entity.Id + " does not exist!");
                }

                hotelDbContext.Entry(roomFromDb).CurrentValues.SetValues(entity);
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
                Room room = await ReadAsync(key, false, false);

                if (room is null)
                {
                    throw new ArgumentException("Room with id = " + key + " does not exist!");
                }

                hotelDbContext.Rooms.Remove(room);
                await hotelDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
