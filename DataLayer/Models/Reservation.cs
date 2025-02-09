namespace DataLayer
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("RoomId")]
        public Guid RoomId { get; set; }
        public Room ReservedRoom { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User BookedUser { get; set; }

        [Required]
        public List<Client> Clients { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

        [Required]
        public bool IsBreakfastIncluded { get; set; }

        [Required]
        public bool IsAllinclusive {  get; set; }

        [Required]
        public decimal Price { get; set; }

        public Reservation()
        {

        }

        public Reservation(Room reservedRoom, User bookedUser, List<Client> clients, DateTime startingDate,
            DateTime endingDate, bool isBreakfastIncluded, bool isAllinclusive, decimal price)
        {
            ReservedRoom = reservedRoom;
            BookedUser = bookedUser;
            Clients = clients;
            StartingDate = startingDate;
            EndingDate = endingDate;
            IsBreakfastIncluded = isBreakfastIncluded;
            IsAllinclusive = isAllinclusive;
            Price = price;
        }
    }
}
