namespace DataLayer
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [Required]
        public string SocialSecurity { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime StartOfEmployment { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime? EndOfEmployment { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

        public User()
        {

        }
    }
}
