namespace DataLayer
{
    public class ReservationContext : IDb<Reservation, Guid>
    {
        private readonly HotelDbContext _hotelDbContext;
        public ReservationContext(HotelDbContext hotelDbContext)
        {
            this._hotelDbContext = hotelDbContext;
        }
        public async Task CreateAsync(Reservation entity)
        {
            try
            {
                _hotelDbContext.Reservations.Add(entity);
                _hotelDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Reservation> ReadAsync(Guid key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Reservation> query = _hotelDbContext.Reservations;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.BookedUser).Include(e => e.ReservedRoom).Include(e => e.Clients);
                    
                }

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

        public async Task<List<Reservation>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Reservation> query = _hotelDbContext.Reservations;

                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.BookedUser).Include(e => e.ReservedRoom).Include(e => e.Clients);
                }

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

        public async Task UpdateAsync(Reservation entity, bool useNavigationalProperties = false)
        {
            try
            {
                Reservation reservationFromDb = await ReadAsync(entity.Id, useNavigationalProperties, false);
                reservationFromDb.StartingDate = entity.StartingDate;
                reservationFromDb.EndingDate = entity.EndingDate;
                reservationFromDb.IsBreakfastIncluded = entity.IsBreakfastIncluded;
                reservationFromDb.IsAllinclusive =entity.IsAllinclusive;
                reservationFromDb.Price=entity.Price;




                if (useNavigationalProperties)
                {
                    List<Client> clients = new List<Client>();
                    Room roomFromDb = _hotelDbContext.Rooms.Find(entity.ReservedRoom.Id);
                    User userFromDb = _hotelDbContext.Users.Find(entity.BookedUser);

                    foreach (Client client in reservationFromDb.Clients)
                    {
                        Client clientFromDb = _hotelDbContext.Clients.Find(client.Id);
                        if (clientFromDb != null)
                        {
                            clients.Add(clientFromDb);
                        }
                        else
                        {
                            clients.Add(client);
                        }
                    }

                    reservationFromDb.Clients = clients;

                    if (roomFromDb is not null)
                    {
                        reservationFromDb.ReservedRoom = roomFromDb;
                    }
                    else
                    {
                        reservationFromDb.ReservedRoom = entity.ReservedRoom;
                    }

                    if (userFromDb is not null)
                    {
                        reservationFromDb.BookedUser = userFromDb;
                    }
                    else
                    {
                        reservationFromDb.BookedUser = entity.BookedUser;
                    }

                }

                _hotelDbContext.SaveChanges();
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
                Reservation reservation = await ReadAsync(key, false, false);

                if (reservation is null)
                {
                    throw new ArgumentException("Reservation with id = " + key + "does not exist!");
                }

                _hotelDbContext.Reservations.Remove(reservation);
                _hotelDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
