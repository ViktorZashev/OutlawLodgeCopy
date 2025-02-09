namespace DataLayer
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ushort Capacity { get; set; }

        [Required]
        public ushort Type { get; set; }

        [Required]
        public bool IsFree {  get; set; }

        [Required]
        public decimal AdultPrice { get; set; }

        [Required]
        public decimal ChildPrice { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        public Room()
        {

        }
        public Room(ushort capacity, ushort type, bool isFree, decimal adultPrice, decimal childPrice, int roomNumber)
        {
            Capacity = capacity;
            Type = type;
            IsFree = isFree;
            AdultPrice = adultPrice;
            ChildPrice = childPrice;
            RoomNumber = roomNumber;
        }


    }
}
